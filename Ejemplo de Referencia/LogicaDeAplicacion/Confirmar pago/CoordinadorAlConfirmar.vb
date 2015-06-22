Imports LogicaDeNegocio
Imports System.Diagnostics.Contracts

Public Class CoordinadorAlConfirmar

    Private _laBitacora As IBitacoraAlConfirmar
    Private _elRepositorio As IRepositorioAlConfirmar
    Private _elInvocadorDeEntidad As IInvocadorAlConfirmar
    Private _elCalendarizador As ICalendarizadorAlConfirmar

    Public Sub New(laBitacora As IBitacoraAlConfirmar,
                   elRepositorio As IRepositorioAlConfirmar,
                  elInvocadorDeEntidad As IInvocadorAlConfirmar,
                  elCalendarizador As ICalendarizadorAlConfirmar
                  )
        _laBitacora = laBitacora
        _elRepositorio = elRepositorio
        _elInvocadorDeEntidad = elInvocadorDeEntidad
        _elCalendarizador = elCalendarizador
    End Sub

    Public Sub Confirmar(solicitud As SolicitudAlConfirmar)

        Dim miTransaccionDTO As PagoPorConfirmar = _elRepositorio.ObtenerPagoPorConfirmar(solicitud.CodigoDeReferencia)

        ' Valida reglas de negocio
        Dim miTransaccionBE As New PagoBE(miTransaccionDTO)
        Dim sePuedeConfirmar As Boolean = miTransaccionBE.SePuedeConfirmar()

        ' Datos para tomar decisiones
        Dim misParametrosDTO As ParametrosAlConfirmar = Nothing
        Dim misParametrosBE As ParametrosAlConfirmarBE = Nothing
        Dim sePuedeInvocarAEntidad As Boolean = False

        If sePuedeConfirmar Then
            ' Obtiene y valida parametros
            misParametrosDTO = _elRepositorio.ObtenerParametros(miTransaccionDTO)
            misParametrosBE = New ParametrosAlConfirmarBE(misParametrosDTO)
            sePuedeInvocarAEntidad = misParametrosBE.SePuedeInvocarAEntidad()
        Else
            Dim mensaje As New ErrorPagoDebeEstarAutorizadoYNoNotificado(miTransaccionDTO)
            _laBitacora.EscribirErrorAlConfirmar(mensaje)
        End If

        Dim comunicacionFueExitosa As Boolean = False
        Dim miTransaccionDTOConfirmada As PagoPorConfirmar = Nothing

        If sePuedeInvocarAEntidad Then
            miTransaccionDTOConfirmada = miTransaccionBE.MarcarPagoComoConfirmado(solicitud.FechaYHoraActual)
            Dim laInstruccionDeConfirmacion = New InstruccionDeConfirmacion(miTransaccionDTOConfirmada, misParametrosDTO, solicitud.FechaYHoraActual)
            Dim respuestaAlInvocarEntidad = _elInvocadorDeEntidad.InvocarParaConfirmar(laInstruccionDeConfirmacion)
            comunicacionFueExitosa = respuestaAlInvocarEntidad.ComunicacionFueExitosa
        End If

        If sePuedeConfirmar And Not sePuedeInvocarAEntidad Then
            Dim mensaje As New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(miTransaccionDTO)
            _laBitacora.EscribirErrorNoSePuedeInvocarAEntidad(mensaje)
        End If

        If comunicacionFueExitosa Then
            _elRepositorio.ActualizarPagoConfirmado(miTransaccionDTOConfirmada)
            Dim mensaje = New MensajePagoFueConfirmado(miTransaccionDTOConfirmada)
            _laBitacora.EscribirPagoFueConfirmado(mensaje)
        End If

        Dim seNecesitaReintentar = Not comunicacionFueExitosa And sePuedeConfirmar And sePuedeInvocarAEntidad

        If seNecesitaReintentar Then
            ReintentarConfirmacion(solicitud)
        End If
    End Sub

    Public Sub ReintentarConfirmacion(solicitud As SolicitudAlConfirmar)
        Dim parametros = _elRepositorio.ObtenerParametrosParaReintentar()

        Dim misReintentosBE As New ReintentosBE(parametros, solicitud)
        Dim sePuedeReintentar = misReintentosBE.SePuedeReintentar()

        If sePuedeReintentar Then
            Dim instruccion = misReintentosBE.ObtenerInstruccionParaReintentar()
            _elCalendarizador.Reintentar(instruccion)

            Dim mensaje As New MensajeConfirmacionDePagoReintentado(instruccion)
            _laBitacora.EscribirConfirmacionDePagoReintentado(mensaje)
        End If

    End Sub

End Class

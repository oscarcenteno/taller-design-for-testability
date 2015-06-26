Imports LogicaDeNegocio
Imports System.Diagnostics.Contracts

Public Class CoordinadorAlConfirmar

    Private _laBitacora As IBitacora
    Private _elRepositorio As IRepositorioAlConfirmar
    Private _elInvocadorDeEntidad As IInvocador
    Private _elCalendarizador As ICalendarizador

    Public Sub New(laBitacora As IBitacora,
                   elRepositorio As IRepositorioAlConfirmar,
                  elInvocadorDeEntidad As IInvocador,
                  elCalendarizador As ICalendarizador
                  )
        _laBitacora = laBitacora
        _elRepositorio = elRepositorio
        _elInvocadorDeEntidad = elInvocadorDeEntidad
        _elCalendarizador = elCalendarizador
    End Sub

    Public Sub Confirmar(solicitud As SolicitudAlConfirmar)

        Dim miPagoPorConfirmar As PagoPorConfirmar = _elRepositorio.ObtenerPagoPorConfirmar(solicitud.CodigoDeReferencia)

        ' Valida reglas de negocio
        Dim miPagoBE As New PagoBE(miPagoPorConfirmar)
        Dim sePuedeConfirmar As Boolean = miPagoBE.SePuedeConfirmar()

        ' Datos para tomar decisiones
        Dim misParametrosDTO As ParametrosAlConfirmar = Nothing
        Dim misParametrosBE As ParametrosAlConfirmarBE = Nothing
        Dim sePuedeInvocarAEntidad As Boolean = False

        If sePuedeConfirmar Then
            ' Obtiene y valida parametros
            misParametrosDTO = _elRepositorio.ObtenerParametrosParInvocarEntidad(miPagoPorConfirmar.CodigoDeEntidadDestino)
            misParametrosBE = New ParametrosAlConfirmarBE(misParametrosDTO)
            sePuedeInvocarAEntidad = misParametrosBE.SePuedeInvocarAEntidad()
        Else
            Dim mensaje = MensajesABitacora.ObtenerErrorPagoDebeEstarAutorizadoYNoNotificado(miPagoPorConfirmar)
            _laBitacora.EscribirError(mensaje)
        End If

        Dim comunicacionFueExitosa As Boolean = False

        If sePuedeInvocarAEntidad Then
            Dim laInstruccionDeConfirmacion = New InstruccionDeConfirmacion(miPagoPorConfirmar, misParametrosDTO, solicitud.FechaYHoraActual)
            Dim respuestaAlInvocarEntidad = _elInvocadorDeEntidad.InvocarParaConfirmar(laInstruccionDeConfirmacion)
            comunicacionFueExitosa = respuestaAlInvocarEntidad.ComunicacionFueExitosa
        End If

        If sePuedeConfirmar And Not sePuedeInvocarAEntidad Then
            Dim mensaje = MensajesABitacora.ObtenerErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(miPagoPorConfirmar)
            _laBitacora.EscribirError(mensaje)
        End If

        If comunicacionFueExitosa Then
            Dim miPagoConfirmado As PagoConfirmado = Nothing
            miPagoConfirmado = miPagoBE.ObtenerDatosParaPagoConfirmado(solicitud.FechaYHoraActual)
            _elRepositorio.ActualizarPagoConfirmado(miPagoConfirmado)
            Dim mensaje = MensajesABitacora.ObtenerMensajePagoFueConfirmado(miPagoPorConfirmar)
            _laBitacora.EscribirMensajeInformativo(mensaje)
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

            Dim mensaje = MensajesABitacora.ObtenerMensajeConfirmacionDePagoReintentado(instruccion)
            _laBitacora.EscribirMensajeInformativo(mensaje)
        End If

    End Sub

End Class

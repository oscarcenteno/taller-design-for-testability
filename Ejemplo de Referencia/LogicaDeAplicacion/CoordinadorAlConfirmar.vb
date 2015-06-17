Imports LogicaDeNegocio

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


    ' HACK: el codigo original no recibe la fecha, lo que no lo hace testable por el Date.Now que genera internamente
    Public Sub Confirmar(codReferencia As String, intentosYaRealizados As Integer, fechaYHoraActual As Date)
        Dim datosDeTransaccion As TransaccionDTO = _elRepositorio.ObtenerTransaccion(codReferencia)
        Dim respuesta = Confirmar(datosDeTransaccion, fechaYHoraActual)
        If respuesta.SeNecesitaReintentar Then
            ReintentarConfirmacion(fechaYHoraActual, datosDeTransaccion, intentosYaRealizados)
        End If
    End Sub

    Public Function Confirmar(miTransaccionDTO As TransaccionDTO, fechaYHoraActual As Date) As RespuestaAlConfirmar
        Dim respuesta As New RespuestaAlConfirmar

        ' Valida reglas de negocio
        Dim miTransaccionBE As New TransaccionBE(miTransaccionDTO)
        Dim sePuedeConfirmar As Boolean = miTransaccionBE.SePuedeConfirmar()

        ' Datos para tomar decisiones
        Dim comunicacionFueExitosa As Boolean = False
        Dim miTransaccionDTOConfirmada As TransaccionDTO = Nothing
        Dim misParametrosDTO As ParametrosAlConfirmarDTO = Nothing
        Dim misParametrosBE As ParametrosAlConfirmarBE = Nothing
        Dim sePuedeInvocarAEntidad As Boolean = False

        If sePuedeConfirmar Then
            ' Obtiene y valida parametros
            misParametrosDTO = _elRepositorio.ObtenerParametros(miTransaccionDTO)
            misParametrosBE = New ParametrosAlConfirmarBE(miTransaccionDTO, misParametrosDTO)
            sePuedeInvocarAEntidad = misParametrosBE.SePuedeInvocarAEntidad()
        Else
            _laBitacora.EscribirErrorAlConfirmar(miTransaccionBE.ErrorDeValidacion)
        End If

        If sePuedeInvocarAEntidad Then
            miTransaccionBE.Confirmar(fechaYHoraActual)
            miTransaccionDTOConfirmada = miTransaccionBE.ObtenerDatosDeTransaccion

            Dim laInstruccionDeConfirmacion = New InstruccionDeConfirmacion(miTransaccionDTOConfirmada, misParametrosDTO, fechaYHoraActual)
            Dim respuestaAlInvocarEntidad = _elInvocadorDeEntidad.Confirmar(laInstruccionDeConfirmacion)
            comunicacionFueExitosa = respuestaAlInvocarEntidad.ComunicacionFueExitosa
        End If

        If sePuedeConfirmar And Not sePuedeInvocarAEntidad Then
            _laBitacora.EscribirErrorNoSePuedeInvocarAEntidad(misParametrosBE.ErrorDeValidacion)
        End If


        If comunicacionFueExitosa Then
            _elRepositorio.ActualizarTransaccionConfirmada(miTransaccionDTOConfirmada)
            Dim mensajeDeTransaccionFueConfirmada = New MensajeTransaccionFueConfirmada(miTransaccionDTOConfirmada)
            _laBitacora.EscribirTransaccionConfirmada(mensajeDeTransaccionFueConfirmada)
        End If

        respuesta.SeNecesitaReintentar = Not comunicacionFueExitosa And sePuedeConfirmar And sePuedeInvocarAEntidad

        Return respuesta

    End Function


    Public Sub ReintentarConfirmacion(fechaYHoraActual As Date, datosDeTransaccion As TransaccionDTO, intentosYaRealizados As Integer)
        Dim parametrosParaRecalendarizar = _elRepositorio.ObtenerParametrosParaRecalendarizar()

        Dim misReintentosBE As New ReintentosBE(parametrosParaRecalendarizar, datosDeTransaccion, intentosYaRealizados, fechaYHoraActual)
        Dim sePuedeReintentar = misReintentosBE.SePuedeReintentar()

        If sePuedeReintentar Then
            Dim instruccion = misReintentosBE.ObtenerInstruccionParaReintentar()
            _elCalendarizador.Reintentar(instruccion)

            Dim mensaje = misReintentosBE.ObtenerMensajeTransaccionFueCalendarizada()
            _laBitacora.EscribirTransaccionFueRecalendarizada(mensaje)
        End If

    End Sub

End Class

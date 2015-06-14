Imports LogicaDeNegocio

Public Class App

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
    Public Sub Confirmar(codReferencia As String, intentos As Integer, fecha As Date)
        Dim datosDeTransaccion As TransaccionDTO = _elRepositorio.ObtenerTransaccion(codReferencia)
        Dim respuesta = Confirmar(datosDeTransaccion, fecha)
        If respuesta.AlInvocarEntidad.ComunicacionFueFallida Then
            IntentarCalendarizar(Date.Now, datosDeTransaccion, intentos)
        End If
    End Sub

    Public Function Confirmar(datosDeTransaccion As TransaccionDTO, fechaYHoraActual As Date) As RespuestaAlConfirmar
        Dim respuesta As New RespuestaAlConfirmar
        Dim parametros = _elRepositorio.ObtenerParametrosParaConfirmar(datosDeTransaccion)

        ' Reglas del negocio
        Dim logica As New LogicaDeNegocio.ProcesoAlConfirmar()
        Dim respuestaValidar = logica.ValidarProceso(parametros, datosDeTransaccion, fechaYHoraActual)

        ' Infraestructura
        Dim transaccionConfirmada = respuestaValidar.DatosDeTransaccionConfirmada
        Dim mensajeDeConfirmacion = respuestaValidar.MensajeDeTransaccionFueConfirmada
        Dim sePuedeConfirmar = respuestaValidar.SePuedeConfirmar
        Dim erroresEncontrados = respuestaValidar.Errores

        If sePuedeConfirmar Then
            respuesta.AlInvocarEntidad = _elInvocadorDeEntidad.Confirmar(respuestaValidar.LaInstruccionDeConfirmacion)
            _elRepositorio.ActualizarTransaccionConfirmada(transaccionConfirmada)
            _laBitacora.EscribirTransaccionConfirmada(mensajeDeConfirmacion)
        Else
            ' HACK Se comunica errores por medio de objetos
            _laBitacora.EscribirErrores(erroresEncontrados)
        End If

        respuesta.AlValidarConfirmacion = respuestaValidar

        Return respuesta

    End Function


    Public Sub IntentarCalendarizar(fechaYHoraActual As Date, datosDeTransaccion As TransaccionDTO, intentosYaRealizados As Integer)
        Dim logica As New LogicaDeNegocio.ProcesoAlConfirmar()

        Dim parametrosParaRecalendarizar = _elRepositorio.ObtenerParametrosParaRecalendarizar()
        Dim laRespuesta = logica.ValidarRecalendarizacion(fechaYHoraActual, datosDeTransaccion, parametrosParaRecalendarizar, intentosYaRealizados)

        Dim mensaje = laRespuesta.MensajeABitacoraTransaccionFueRecalendarizada
        Dim instruccion = laRespuesta.LaInstruccionParaRecalendarizar

        If laRespuesta.SePuedeReintentar Then
            _elCalendarizador.Recalendarizar(instruccion)
            _laBitacora.EscribirTransaccionFueRecalendarizada(mensaje)
        End If

    End Sub

End Class

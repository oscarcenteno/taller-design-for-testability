Imports LogicaDeNegocio

Public Class App

    Private _laBitacora As IBitacora
    Private _repParametros As IRepositorioDeParametros
    Private _repTransacciones As IRepositorioDeTransacciones
    Private _elInvocadorDeEntidad As IInvocadorAlConfirmar
    Private _elCalendarizador As ICalendarizador

    Public Sub New(laBitacora As IBitacora, repParametros As IRepositorioDeParametros,
                  repTransacciones As IRepositorioDeTransacciones,
                  elInvocadorDeEntidad As IInvocadorAlConfirmar,
                  elCalendarizador As ICalendarizador
                  )
        _laBitacora = laBitacora
        _repParametros = repParametros
        _repTransacciones = repTransacciones
        _elInvocadorDeEntidad = elInvocadorDeEntidad
        _elCalendarizador = elCalendarizador
    End Sub

    Public Sub Confirmar(codReferencia As String, intentos As Integer)
        Dim datosDeTransaccion As TransaccionDTO = _repTransacciones.Obtener(codReferencia)
        Dim respuesta = Confirmar(datosDeTransaccion, datosDeTransaccion.FecValor)
        If respuesta.AlInvocarEntidad.ComunicacionFueFallida Then
            IntentarCalendarizar(Date.Now, datosDeTransaccion, intentos)
        End If
    End Sub

    Public Function Confirmar(datosDeTransaccion As TransaccionDTO, fecha As Date) As RespuestaAlConfirmar
        Dim respuesta As New RespuestaAlConfirmar
        Dim parametros = _repParametros.ObtenerParametrosParaConfirmar()

        ' Reglas del negocio
        Dim logica As New LogicaDeNegocio.ProcesoAlConfirmar()
        Dim respuestaAlValidarConfirmacion = logica.ValidarConfirmacion(parametros, datosDeTransaccion, fecha)

        ' Infraestructura
        Dim laRespuestaAlInvocarEntidad = New RespuestaAlInvocarEntidad
        If respuestaAlValidarConfirmacion.ProcesoPuedeEjecutarse Then
            laRespuestaAlInvocarEntidad = _elInvocadorDeEntidad.Confirmar(respuestaAlValidarConfirmacion.DatosTransaccionConfirmada, parametros, fecha)
            _repTransacciones.Actualizar(respuestaAlValidarConfirmacion.DatosTransaccionConfirmada)
            _laBitacora.Escribir(respuestaAlValidarConfirmacion.MensajeTransaccionConfirmada)
        Else
            _laBitacora.Escribir(respuestaAlValidarConfirmacion.Errores)
        End If

        respuesta.AlValidarConfirmacion = respuestaAlValidarConfirmacion
        respuesta.AlInvocarEntidad = laRespuestaAlInvocarEntidad
        Return respuesta

    End Function


    Public Sub IntentarCalendarizar(fecha As Date, datosDeTransaccion As TransaccionDTO, intentos As Integer)
        Dim logica As New LogicaDeNegocio.ProcesoAlConfirmar()

        Dim parametrosParaRecalendarizar As ParametrosAlRecalendarizar = _repParametros.ObtenerParametrosParaRecalendarizar()
        Dim laRespuestaAlValidarRecalendarizacion = logica.ValidarRecalendarizacion(fecha, datosDeTransaccion, parametrosParaRecalendarizar, intentos)

        If laRespuestaAlValidarRecalendarizacion.SePuedeReintentarLaConfirmacion Then
            _elCalendarizador.ReCalendarizarConfirmacion(laRespuestaAlValidarRecalendarizacion.LaInstruccionParaRecalendarizar)
            _laBitacora.Escribir(laRespuestaAlValidarRecalendarizacion.MensajeABitacoraTransaccionFueRecalendarizada)
        End If

    End Sub

End Class

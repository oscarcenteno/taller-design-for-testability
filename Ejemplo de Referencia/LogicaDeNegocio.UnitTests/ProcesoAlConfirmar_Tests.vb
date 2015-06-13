Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class ProcesoAlConfirmar_Tests

    Private sut As New ProcesoAlConfirmar

    'datos de pruebas
    Private fecha As New Date(2015, 6, 1)
    Private datosDeTransaccionSePuedeConfirmar As New TransaccionDTO With {.CodReferencia = "CodReferenciaPrueba", .Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = False}
    Private datosDeTransaccionConfirmada As New TransaccionDTO With {.CodReferencia = "CodReferenciaPrueba", .Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = True, .FechaDeConfirmacion = fecha}
    Private datosDeTransaccionNoSePuedeConfirmar As New TransaccionDTO With {.CodReferencia = "CodReferenciaPrueba", .Estado = EstadoTransaccion.EnProceso, .SeHaNotificado = False}
    Private parametrosValidos As New ParametrosAlConfirmar(datosDeTransaccionSePuedeConfirmar, "http://destino", 300, "CN")
    Private parametrosInvalidos As New ParametrosAlConfirmar(datosDeTransaccionSePuedeConfirmar, "http://destino", 300, String.Empty)

    <TestMethod()> Public Sub ValidarProceso_NoHayErrores_RespuestaCompletaParaProcesar()
        'expectativas
        Dim respuestaEsperada As New RespuestaAlValidarProceso
        respuestaEsperada.ProcesoPuedeEjecutarse = True
        respuestaEsperada.MensajeTransaccionConfirmada = _
            New InformativoTransaccionFueConfirmada(datosDeTransaccionSePuedeConfirmar)
        respuestaEsperada.laInstruccionDeConfirmacion = _
            New InstruccionDeConfirmacion(datosDeTransaccionSePuedeConfirmar, parametrosValidos, fecha)
        respuestaEsperada.DatosTransaccionConfirmada = datosDeTransaccionConfirmada

        'sut
        Dim respuestaObtenida = sut.ValidarConfirmacion(parametrosValidos, datosDeTransaccionSePuedeConfirmar, fecha)

        'verificacion
        Assert.AreEqual(respuestaEsperada, respuestaObtenida)
    End Sub

    <TestMethod()> Public Sub ValidarProceso_ErroresDeNegocio_RespuestaNegativaConErrores()
        'expectativas
        Dim respuestaEsperada As New RespuestaAlValidarProceso
        respuestaEsperada.ProcesoPuedeEjecutarse = False
        respuestaEsperada.Errores.Add(New ErrorTransaccionNoEstaAutorizada(datosDeTransaccionNoSePuedeConfirmar))

        'sut
        Dim respuestaObtenida = sut.ValidarConfirmacion(parametrosValidos, datosDeTransaccionNoSePuedeConfirmar, fecha)

        'verificacion
        Assert.AreEqual(respuestaEsperada, respuestaObtenida)
    End Sub

    <TestMethod()> Public Sub ValidarProceso_ErroresDeParametros_RespuestaNegativaConErrores()
        'expectativas
        Dim respuestaEsperada As New RespuestaAlValidarProceso
        respuestaEsperada.ProcesoPuedeEjecutarse = False
        respuestaEsperada.Errores.Add( _
            New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(datosDeTransaccionSePuedeConfirmar.CodReferencia, _
                                                                    datosDeTransaccionSePuedeConfirmar.CodEntidadPadronMovilDestino))
        'sut
        Dim respuestaObtenida = sut.ValidarConfirmacion(parametrosInValidos, datosDeTransaccionNoSePuedeConfirmar, fecha)

        'verificacion
        Assert.AreEqual(respuestaEsperada, respuestaObtenida)
    End Sub

    <TestMethod()> Public Sub ValidarRecalendarizacion_MaximoDeIntentosAlcanzado_NoSePuedeCalendarizar()
        Dim intentosRealizados = 3
        Dim parametrosParaRecalendarizar As New ParametrosAlRecalendarizar
        parametrosParaRecalendarizar.CantidadMaximaDeIntentos = 3

        ' expectativas
        Dim esperado = False

        'sut
        Dim respuestaEsperada = sut.ValidarRecalendarizacion(fecha, datosDeTransaccionSePuedeConfirmar, parametrosParaRecalendarizar, intentosRealizados)

        'verificacion
        Assert.AreEqual(esperado, respuestaEsperada.SePuedeReintentarLaConfirmacion)

    End Sub

    <TestMethod()> Public Sub ValidarRecalendarizacion_MaximoDeIntentosNoAlcanzado_SePuedeCalendarizar()
        Dim intentosRealizados = 2
        Dim parametrosParaRecalendarizar As New ParametrosAlRecalendarizar
        parametrosParaRecalendarizar.CantidadMaximaDeIntentos = 3
        parametrosParaRecalendarizar.IntervaloDeNotificacion = New TimeSpan(0, 5, 0)
        Dim fecha As New Date(2015, 6, 1, 12, 0, 0)

        ' expectativas
        Dim fechaInicioEsperada As New Date(2015, 6, 1, 12, 5, 0)
        Dim intentosEsperadosParaCalendarizador = 3
        Dim respuestaEsperada As New RespuestaAlValidarRecalendarizacion
        respuestaEsperada.SePuedeReintentarLaConfirmacion = True
        respuestaEsperada.LaInstruccionParaRecalendarizar = New InstruccionParaRecalendarizar _
            With {.FechaInicio = fechaInicioEsperada, _
                  .Intentos = intentosEsperadosParaCalendarizador, _
                  .CodReferencia = "CodReferenciaPrueba"}
        respuestaEsperada.MensajeABitacoraTransaccionFueRecalendarizada = _
            New InformativoTransaccionFueRecalendarizada(datosDeTransaccionSePuedeConfirmar, _
                                                          intentosEsperadosParaCalendarizador, _
                                                          fechaInicioEsperada)

        'sut
        Dim respuestaObtenida As RespuestaAlValidarRecalendarizacion = sut.ValidarRecalendarizacion(fecha, _
                                                             datosDeTransaccionSePuedeConfirmar, _
                                                             parametrosParaRecalendarizar, _
                                                             intentosRealizados)

        'verificacion
        Assert.AreEqual(respuestaEsperada, respuestaObtenida)
    End Sub

End Class
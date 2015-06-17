Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class ReintentosBE_Tests

    ' datos de la prueba
    Dim parametros As New ParametrosAlRecalendarizarDTO With {.CantidadMaximaDeIntentos = 3, .IntervaloDeNotificacion = "00:10:00"}
    Dim codigoDeReferencia = "Ref5"
    Dim intentos = 2
    Dim fecha = New Date(2015, 6, 15, 12, 30, 0)
    Dim transaccion As New TransaccionDTO With {.CodigoDeReferencia = codigoDeReferencia}

    ' esperados
    Dim intentosEsperados = 3
    Dim fechaEsperada As New Date(2015, 6, 15, 12, 40, 0)
    Dim sut = New ReintentosBE(parametros, transaccion, intentos, fecha)

    <TestMethod()> Public Sub ObtenerInstruccionParaReintentar_ParametrosValidas_SonIguales()
        Dim esperado = New InstruccionParaRecalendarizar With {.CodigoDeReferencia = codigoDeReferencia,
                                                               .FechaDeInicio = fechaEsperada,
                                                               .NumeroDeReintento = intentosEsperados}
        Dim obtenido = sut.ObtenerInstruccionParaReintentar()
        Assert.AreEqual(esperado, obtenido)
    End Sub

    <TestMethod()> Public Sub ObtenerMensajeTransaccionFueCalendarizada_ParametrosValidas_SonIguales()
        Dim esperado = New MensajeTransaccionFueRecalendarizada(codigoDeReferencia, intentosEsperados, fechaEsperada)
        Dim obtenido = sut.ObtenerMensajeTransaccionFueCalendarizada()
        Assert.AreEqual(esperado, obtenido)
    End Sub


End Class
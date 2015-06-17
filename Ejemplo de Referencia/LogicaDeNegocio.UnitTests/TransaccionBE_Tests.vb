Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TransaccionBE_Tests

    Private miTransaccion As TransaccionBE
    Private transaccionNoAutorizada As New TransaccionDTO With {.Estado = EstadoTransaccion.EnProceso, .SeHaNotificado = False}
    Private transaccionYaNotificada As New TransaccionDTO With {.Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = True}
    Private transaccionAutorizadaYNoNotificada As New TransaccionDTO With {.Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = False}

    <TestMethod()> Public Sub ErrorDeValidacion_SePuedeConfirmar_SinErrores()
        miTransaccion = New TransaccionBE(transaccionAutorizadaYNoNotificada)
        ' expectativa
        Dim esperado = Nothing
        ' sut
        Dim obtenido = miTransaccion.ErrorDeValidacion()
        ' verificacion
        Assert.AreEqual(esperado, obtenido)
    End Sub

    <TestMethod()> Public Sub ErrorDeValidacion_NoSeHaAutorizado_Error()
        miTransaccion = New TransaccionBE(transaccionNoAutorizada)
        ' expectativa
        Dim esperado As New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(transaccionNoAutorizada)
        ' sut
        Dim obtenido = miTransaccion.ErrorDeValidacion()
        ' verificacion
        Assert.AreEqual(esperado, obtenido)
    End Sub

    <TestMethod()> Public Sub ErrorDeValidacion_YaSeHaNotificado_Error()
        miTransaccion = New TransaccionBE(transaccionYaNotificada)
        ' expectativa
        Dim esperado As New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(transaccionYaNotificada)
        ' sut
        Dim obtenido = miTransaccion.ErrorDeValidacion()
        ' verificacion
        Assert.AreEqual(esperado, obtenido)
    End Sub

    <TestMethod()> Public Sub ObtenerDatosDeTransaccion_Comparacion_SonIguales()
        ' sut
        miTransaccion = New TransaccionBE(transaccionAutorizadaYNoNotificada)
        Dim obtenido = miTransaccion.ObtenerDatosDeTransaccion()
        ' verificacion
        Assert.AreEqual(transaccionAutorizadaYNoNotificada, obtenido)
    End Sub

End Class
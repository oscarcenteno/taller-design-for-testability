Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class MensajeTransaccionFueConfirmada_Tests

    Private transaccion As TransaccionDTO
    Private transaccion2 As TransaccionDTO
    Private transaccion3 As TransaccionDTO
    Private transaccion4 As TransaccionDTO
    Private transaccion5 As TransaccionDTO
    Private transaccion6 As TransaccionDTO
    Private uno As MensajeTransaccionFueConfirmada

    Public Sub New()
        transaccion = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501", .CodigoDeMonedaDestino = "1", .CodigoDeEntidadOrigen = "401", .NumeroDeTelefonoDestino = "60607070"}
        transaccion2 = New TransaccionDTO With {.CodigoDeReferencia = "Ref2", .CodigoDeEntidadDestino = "501", .CodigoDeMonedaDestino = "1", .CodigoDeEntidadOrigen = "401", .NumeroDeTelefonoDestino = "60607070"}
        transaccion3 = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "503", .CodigoDeMonedaDestino = "1", .CodigoDeEntidadOrigen = "401", .NumeroDeTelefonoDestino = "60607070"}
        transaccion4 = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501", .CodigoDeMonedaDestino = "2", .CodigoDeEntidadOrigen = "401", .NumeroDeTelefonoDestino = "60607070"}
        transaccion5 = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501", .CodigoDeMonedaDestino = "1", .CodigoDeEntidadOrigen = "402", .NumeroDeTelefonoDestino = "60607070"}
        transaccion6 = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501", .CodigoDeMonedaDestino = "1", .CodigoDeEntidadOrigen = "401", .NumeroDeTelefonoDestino = "60607071"}
        uno = New MensajeTransaccionFueConfirmada(transaccion)
    End Sub

    <TestMethod()>
    Public Sub Equals_SonIguales_True()
        Dim dos As New MensajeTransaccionFueConfirmada(transaccion)
        Assert.AreEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeReferenciaNoSonIguales_False()
        Dim dos As New MensajeTransaccionFueConfirmada(transaccion2)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeEntidadDestinoNoSonIguales_False()
        Dim dos As New MensajeTransaccionFueConfirmada(transaccion3)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeMonedaDestinoNoSonIguales_False()
        Dim dos As New MensajeTransaccionFueConfirmada(transaccion4)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeEntidadOrigenNoSonIguales_False()
        Dim dos As New MensajeTransaccionFueConfirmada(transaccion5)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_NumeroDeTelefonoDestinoNoSonIguales_False()
        Dim dos As New MensajeTransaccionFueConfirmada(transaccion6)
        Assert.AreNotEqual(uno, dos)
    End Sub


    <TestMethod()>
    Public Sub ToString_CasoUnico_StringEsperado()
        Dim esperado = "Se confirmó la operación con referencia [Ref1] de la entidad origen [401], en moneda [1] hacia el teléfono [60607070] asociado a la entidad destino [501]"
        Assert.AreEqual(esperado, uno.ToString)
    End Sub


End Class

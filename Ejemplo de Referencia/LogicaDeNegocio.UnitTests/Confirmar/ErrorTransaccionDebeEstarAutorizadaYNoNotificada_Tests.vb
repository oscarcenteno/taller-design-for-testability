Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ErrorTransaccionDebeEstarAutorizadaYNoNotificada_Tests

    Private transaccion As TransaccionDTO
    Private transaccion2 As TransaccionDTO
    Private uno As ErrorTransaccionDebeEstarAutorizadaYNoNotificada

    Public Sub New()
        transaccion = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501"}
        transaccion2 = New TransaccionDTO With {.CodigoDeReferencia = "Ref2", .CodigoDeEntidadDestino = "501"}
        uno = New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(transaccion)
    End Sub

    <TestMethod()>
    Public Sub Equals_SonIguales_True()
        Dim dos As New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(transaccion)
        Assert.AreEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeReferenciaNoSonIguales_False()
        Dim dos As New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(transaccion2)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub ToString_CasoUnico_StringEsperado()
        Dim esperado = "No se confirmó la transacción con Cod. Referencia [Ref1] pues debe estar autorizada y no notificada."
        Assert.AreEqual(esperado, uno.ToString)
    End Sub

End Class

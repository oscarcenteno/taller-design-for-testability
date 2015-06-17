Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos_Tests

    Dim transaccion As TransaccionDTO
    Dim transaccion2 As TransaccionDTO
    Dim transaccion3 As TransaccionDTO
    Dim uno As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos

    Public Sub New()
        transaccion = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501"}
        transaccion2 = New TransaccionDTO With {.CodigoDeReferencia = "Ref2", .CodigoDeEntidadDestino = "501"}
        transaccion3 = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "502"}
        uno = New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(transaccion)
    End Sub

    <TestMethod()>
    Public Sub Equals_SonIguales_True()
        Dim dos As New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(transaccion)
        Assert.AreEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeReferenciaNoSonIguales_False()
        Dim dos As New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(transaccion2)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeEntidadDestinoNoSonIguales_False()
        Dim dos As New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(transaccion3)
        Assert.AreNotEqual(uno, dos)
    End Sub


    <TestMethod()>
    Public Sub ToString_CasoUnico_StringEsperado()
        Dim esperado = "No se confirmó la transacción Ref1 pues los parametros requeridos para invocar a la entidad 501 no están configurados"
        Assert.AreEqual(esperado, uno.ToString)
    End Sub


End Class

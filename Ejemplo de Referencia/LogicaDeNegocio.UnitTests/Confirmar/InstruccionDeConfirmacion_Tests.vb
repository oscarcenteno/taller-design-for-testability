Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class InstruccionDeConfirmacion_Tests

    Private transaccion As TransaccionDTO
    Private transaccion2 As TransaccionDTO
    Private transaccion3 As TransaccionDTO
    Private parametros As ParametrosAlConfirmarDTO
    Private parametros2 As ParametrosAlConfirmarDTO
    Private parametros3 As ParametrosAlConfirmarDTO
    Private parametros4 As ParametrosAlConfirmarDTO
    Private fecha1 As Date
    Private fecha2 As Date
    Private uno As InstruccionDeConfirmacion

    Public Sub New()
        transaccion = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "501"}
        transaccion2 = New TransaccionDTO With {.CodigoDeReferencia = "Ref2", .CodigoDeEntidadDestino = "501"}
        transaccion3 = New TransaccionDTO With {.CodigoDeReferencia = "Ref1", .CodigoDeEntidadDestino = "502"}

        parametros = New ParametrosAlConfirmarDTO With {.Cn = "CN", .TimeOut = "2", .Url = "http://1"}
        parametros2 = New ParametrosAlConfirmarDTO With {.Cn = "CN1", .TimeOut = "2", .Url = "http://1"}
        parametros3 = New ParametrosAlConfirmarDTO With {.Cn = "CN", .TimeOut = "3", .Url = "http://1"}
        parametros4 = New ParametrosAlConfirmarDTO With {.Cn = "CN", .TimeOut = "2", .Url = "http://2"}

        fecha1 = New Date(2015, 1, 2)
        fecha2 = New Date(2015, 1, 3)

        uno = New InstruccionDeConfirmacion(transaccion, parametros, fecha1)

    End Sub

    <TestMethod()>
    Public Sub Equals_SonIguales_True()
        Dim dos As New InstruccionDeConfirmacion(transaccion, parametros, fecha1)
        Assert.AreEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeReferenciaNoSonIguales_False()
        Dim dos As New InstruccionDeConfirmacion(transaccion2, parametros, fecha1)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeEntidadDestinoNoSonIguales_False()
        Dim dos As New InstruccionDeConfirmacion(transaccion3, parametros, fecha1)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_UrlNoSonIguales_False()
        Dim dos As New InstruccionDeConfirmacion(transaccion, parametros2, fecha1)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_TimeOutNoSonIguales_False()
        Dim dos As New InstruccionDeConfirmacion(transaccion, parametros3, fecha1)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_CnNoSonIguales_False()
        Dim dos As New InstruccionDeConfirmacion(transaccion, parametros4, fecha1)
        Assert.AreNotEqual(uno, dos)
    End Sub

    <TestMethod()>
    Public Sub Equals_FechasNoSonIguales_False()
        Dim dos As New InstruccionDeConfirmacion(transaccion, parametros, fecha2)
        Assert.AreNotEqual(uno, dos)
    End Sub

End Class

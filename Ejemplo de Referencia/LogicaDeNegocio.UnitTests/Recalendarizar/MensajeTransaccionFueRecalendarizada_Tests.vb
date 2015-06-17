Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class MensajeTransaccionFueRecalendarizada_Tests

    Dim codigo = "Ref1"
    Dim fecha = New Date(2015, 6, 7)
    Dim reintento = 3
    Dim miInstruccion As New MensajeTransaccionFueRecalendarizada(codigo, reintento, fecha)

    <TestMethod()>
    Public Sub ToString_ParametrosValidos_MensajeEsElEsperado()
        Dim mensajeEsperado = "Se ha calendarizado el reintento para Confirmar #3 para la transacción con referencia Ref1 en la fecha/hora 07/06/2015."
        Assert.AreEqual(mensajeEsperado, miInstruccion.ToString)
    End Sub

    <TestMethod()>
    Public Sub Equals_ParametrosIguales_SonIguales()
        Dim miInstruccion2 As New MensajeTransaccionFueRecalendarizada(codigo, reintento, fecha)
        Assert.AreEqual(miInstruccion, miInstruccion2)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoNoSonIguales_SonIguales()
        Dim miInstruccion2 As New MensajeTransaccionFueRecalendarizada("Ref2", reintento, fecha)
        Assert.AreNotEqual(miInstruccion, miInstruccion2)
    End Sub

    <TestMethod()>
    Public Sub Equals_FechaNoSonIguales_SonIguales()
        Dim miInstruccion2 As New MensajeTransaccionFueRecalendarizada(codigo, reintento, New Date(2015, 1, 1))
        Assert.AreNotEqual(miInstruccion, miInstruccion2)
    End Sub

    <TestMethod()>
    Public Sub Equals_ReintentoNoSonIguales_SonIguales()
        Dim miInstruccion2 As New MensajeTransaccionFueRecalendarizada(codigo, 2, fecha)
        Assert.AreNotEqual(miInstruccion, miInstruccion2)
    End Sub

End Class

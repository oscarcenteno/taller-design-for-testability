Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class InstruccionParaRecalendarizar_Tests

    Private instruccion1 As InstruccionParaRecalendarizar
    Private instruccion2 As InstruccionParaRecalendarizar
    Private instruccion3 As InstruccionParaRecalendarizar
    Private instruccion4 As InstruccionParaRecalendarizar

    Public Sub New()
        instruccion1 = New InstruccionParaRecalendarizar With {.CodigoDeReferencia = "Ref1", .FechaDeInicio = New Date(2015, 6, 23), .NumeroDeReintento = 3}
        instruccion2 = New InstruccionParaRecalendarizar With {.CodigoDeReferencia = "Ref2", .FechaDeInicio = New Date(2015, 6, 23), .NumeroDeReintento = 3}
        instruccion3 = New InstruccionParaRecalendarizar With {.CodigoDeReferencia = "Ref1", .FechaDeInicio = New Date(2015, 6, 24), .NumeroDeReintento = 3}
        instruccion4 = New InstruccionParaRecalendarizar With {.CodigoDeReferencia = "Ref1", .FechaDeInicio = New Date(2015, 6, 23), .NumeroDeReintento = 5}
    End Sub

    <TestMethod()>
    Public Sub Constructor_ParametrosValidos_SeAsignanCorrectamente()
        Dim codigo = "codigo"
        Dim fecha = New Date(2015, 6, 7)
        Dim reintento = 3

        Dim miInstruccion As New InstruccionParaRecalendarizar(codigo, fecha, reintento)

        Assert.AreEqual(codigo, miInstruccion.CodigoDeReferencia)
        Assert.AreEqual(fecha, miInstruccion.FechaDeInicio)
        Assert.AreEqual(reintento, miInstruccion.NumeroDeReintento)
    End Sub

    <TestMethod()>
    Public Sub Equals_SonIguales_True()
        Assert.AreEqual(instruccion1, instruccion1)
    End Sub

    <TestMethod()>
    Public Sub Equals_CodigoDeReferenciaNoSonIguales_False()
        Assert.AreNotEqual(instruccion1, instruccion2)
    End Sub

    <TestMethod()>
    Public Sub Equals_FechaDeInicioNoSonIguales_False()
        Assert.AreNotEqual(instruccion1, instruccion3)
    End Sub

    <TestMethod()>
    Public Sub Equals_NumeroDeReintentoNoSonIguales_False()
        Assert.AreNotEqual(instruccion1, instruccion4)
    End Sub

End Class

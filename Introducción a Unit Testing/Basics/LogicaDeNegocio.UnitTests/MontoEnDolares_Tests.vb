Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports LogicaDeNegocio

<TestClass()> Public Class MontoEnDolares_Tests

    <TestMethod()> Public Sub Constructor_Basico_ValorEsIgual()
        Dim valorEsperado As Double = 25
        Dim sut As New MontoEnDolares(valorEsperado)
        Dim valorObtenido As Double = sut.Valor
        Assert.AreEqual(Of Double)(valorEsperado, valorObtenido)

    End Sub

    <TestMethod()> _
    Public Sub Parse_TextoValido_MontoEsEsperado()
        Dim esperado = New MontoEnDolares(25)
        Dim obtenido = MontoEnDolares.Parse("$25")
        Assert.AreEqual(esperado, obtenido)
    End Sub

    <TestMethod(), ExpectedException(GetType(ArgumentException))> _
    Public Sub Parse_TextoEsInvalido_Excepcion()
        Dim valorObtenido = MontoEnDolares.Parse("NA25")
    End Sub

    <TestMethod()> _
    Public Sub Equals_SoyYoMismo_ValorEsIgual()
        Dim sut As New MontoEnDolares(25)
        Assert.IsTrue(sut.Equals(sut))
    End Sub

    <TestMethod()> _
    Public Sub Equals_Nothing_ValorEsDiferente()
        Dim sut As New MontoEnDolares(25)
        Assert.IsFalse(sut.Equals(Nothing))
    End Sub

    <TestMethod()> _
    Public Sub Equals_OtroTipo_ValorEsDiferente()
        Dim sut As New MontoEnDolares(25)
        Assert.IsFalse(sut.Equals(New ArrayList))
    End Sub

End Class
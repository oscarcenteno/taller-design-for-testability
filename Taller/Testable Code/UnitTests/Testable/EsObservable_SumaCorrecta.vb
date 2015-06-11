Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Logica

<TestClass()> Public Class EsObservable_SumaCorrecta

    <TestMethod()> Public Sub Sume_SumaEnteros_RetornaEntero()

        Dim esperado = 4
        Dim sut As New EsObservable
        Dim obtenido = sut.Sume(2, 2)
        Assert.AreEqual(esperado, obtenido)

    End Sub

    <TestMethod()> Public Sub Sume_SumaEnteros_RetornaEnteroCorrecto()

        Dim esperado = 4
        Dim sut As New PeroEsteSiEsAislable
        Dim obtenido = sut.Sume(2, 2)
        Assert.AreEqual(esperado, obtenido)

    End Sub

End Class
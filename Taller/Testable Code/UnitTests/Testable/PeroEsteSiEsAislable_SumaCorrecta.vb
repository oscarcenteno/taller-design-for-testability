Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Logica

<TestClass()> Public Class PeroEsteSiEsAislable_SumaCorrecta

    <TestMethod()> Public Sub Add_SumaEnteros_RetornaEntero()

        Dim esperado = 4
        Dim sut As New EsObservable
        Dim obtenido = sut.Sume(2, 2)
        Assert.AreEqual(esperado, obtenido)

    End Sub

    <TestMethod()> Public Sub Add_SumaEnteros_RetornaEntero2()

        Dim esperado = 4
        Dim sut As New PeroEsteSiEsAislable
        Dim obtenido = sut.Sume(2, 2)
        Assert.AreEqual(esperado, obtenido)

    End Sub

End Class
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Logica
Imports System.IO

<TestClass()> Public Class NoEsObservable_FallaComparacion

    <TestMethod()> Public Sub Sume_SumaEnteros_RetornaEntero()

        Dim esperado = 4
        Dim sut As New NoEsObservable
        sut.Sume(2, 2)
        Dim obtenido = File.ReadAllText("results.txt")

        Assert.AreEqual(esperado, obtenido)

    End Sub

End Class
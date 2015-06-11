Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Logica
Imports System.IO

<TestClass()> Public Class AddTests_NoEsAislable

    <TestMethod()> Public Sub Add_SumaEnteros_RetornaEntero()

        Dim esperado = 4
        Dim sut As New NoEsAislable
        Dim obtenido = sut.Add(2, 2)

        Assert.AreEqual(esperado, obtenido)

    End Sub

End Class
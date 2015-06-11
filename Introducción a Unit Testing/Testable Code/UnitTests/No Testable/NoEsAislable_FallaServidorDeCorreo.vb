Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Logica
Imports System.IO

<TestClass()> Public Class NoEsAislable_FallaServidorDeCorreo

    <TestMethod()> Public Sub Sume_SumaEnteros_RetornaEntero()

        Dim esperado = 4
        Dim sut As New NoEsAislable
        Dim obtenido = sut.Sume(2, 2)

        Assert.AreEqual(esperado, obtenido)

    End Sub

End Class
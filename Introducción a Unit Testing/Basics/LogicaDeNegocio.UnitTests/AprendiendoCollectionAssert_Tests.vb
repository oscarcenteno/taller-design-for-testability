Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class AprendiendoCollectionAssert_Tests

    <TestMethod()> Public Sub CollectionAssert_ListasSonIguales_True()

        Dim lista1 As New List(Of MontoEnDolares)
        lista1.Add(New MontoEnDolares(25))
        lista1.Add(New MontoEnDolares(50))

        Dim lista2 As New List(Of MontoEnDolares)
        lista2.Add(New MontoEnDolares(25))
        lista2.Add(New MontoEnDolares(50))

        CollectionAssert.AreEqual(lista1, lista2)

    End Sub

    <TestMethod()> Public Sub CollectionAssert_ListasNoSonIguales_False()

        Dim lista1 As New List(Of MontoEnDolares)
        lista1.Add(New MontoEnDolares(25))
        lista1.Add(New MontoEnDolares(50))

        Dim lista2 As New List(Of MontoEnDolares)
        lista2.Add(New MontoEnDolares(25))
        lista2.Add(New MontoEnDolares(50))

        CollectionAssert.AreEqual(lista1, lista2)

    End Sub

End Class
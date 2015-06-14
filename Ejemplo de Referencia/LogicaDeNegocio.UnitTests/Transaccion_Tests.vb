Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class Transaccion_Tests

    Private miTransaccion As TransaccionBE

    <TestMethod()> Public Sub ErroresAlConfirmar_SePuedeConfirmar_SinErrores()
        miTransaccion = New TransaccionBE(New TransaccionDTO With {.Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = False})
        ' expectativa
        Dim cantidadEsperada = 0
        ' sut
        Dim cantidadObtenida = miTransaccion.ObtenerErroresAlConfirmar().Count
        ' verificacion
        Assert.AreEqual(cantidadEsperada, cantidadObtenida, "No debe haber errores al confirmar")
    End Sub

    <TestMethod()> Public Sub ErroresAlConfirmar_NoSeHaAutorizado_Error()
        miTransaccion = New TransaccionBE(New TransaccionDTO With {.Estado = EstadoTransaccion.EnProceso, .SeHaNotificado = False})
        ' expectativa
        Dim errorEsperado = GetType(ErrorTransaccionNoEstaAutorizada)
        ' sut
        Dim erroresObtenidos = miTransaccion.ObtenerErroresAlConfirmar()
        ' verificacion
        CollectionAssert.AllItemsAreInstancesOfType(erroresObtenidos, errorEsperado)
    End Sub

    <TestMethod()> Public Sub ErroresAlConfirmar_YaSeHaNotificado_Error()
        miTransaccion = New TransaccionBE(New TransaccionDTO With {.Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = True})
        ' expectativa
        Dim errorEsperado = GetType(ErrorTransaccionYaFueNotificada)
        ' sut
        Dim erroresObtenidos = miTransaccion.ObtenerErroresAlConfirmar()
        ' verificacion
        CollectionAssert.AllItemsAreInstancesOfType(erroresObtenidos, errorEsperado)
    End Sub

    <TestMethod()> Public Sub ErroresAlConfirmar_YaSeHaNotificadoYNoSeHaAutorizado_DosErrores()
        Dim miTransaccionDTO As New TransaccionDTO With {.CodReferencia = "CodReferenciaPrueba", .Estado = EstadoTransaccion.Rechazada, .SeHaNotificado = True}
        miTransaccion = New TransaccionBE(miTransaccionDTO)
        ' expectativa
        Dim erroresEsperados As New List(Of Object)
        erroresEsperados.Add(New ErrorTransaccionNoEstaAutorizada(miTransaccionDTO))
        erroresEsperados.Add(New ErrorTransaccionYaFueNotificada(miTransaccionDTO))
        ' sut
        Dim erroresObtenidos = miTransaccion.ObtenerErroresAlConfirmar()
        ' verificacion
        CollectionAssert.AreEqual(erroresObtenidos, erroresEsperados)

    End Sub

End Class
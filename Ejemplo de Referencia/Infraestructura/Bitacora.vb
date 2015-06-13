Imports LogicaDeAplicacion

Public Class Bitacora
    Implements IBitacora

    Public Sub Escribir(mensaje As Object) Implements IBitacora.Escribir
        Console.WriteLine("Bitacora: " + mensaje.ToString)
    End Sub

    Public Sub Escribir(listaDeMensajes As List(Of Object)) Implements IBitacora.Escribir
        For Each mensaje In listaDeMensajes
            Escribir(mensaje)
        Next
    End Sub
End Class

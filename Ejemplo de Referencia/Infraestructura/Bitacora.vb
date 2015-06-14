
Public Class Bitacora

    Public Sub Escribir(mensaje As Object)
        Console.WriteLine("Bitacora: " + mensaje.ToString)
    End Sub

    Public Sub Escribir(listaDeMensajes As List(Of Object))
        For Each mensaje In listaDeMensajes
            Escribir(mensaje)
        Next
    End Sub
End Class

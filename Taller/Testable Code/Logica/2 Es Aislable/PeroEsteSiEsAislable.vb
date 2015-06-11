Imports System.Net.Mail

Public Class PeroEsteSiEsAislable

    ' Para escribir software Testable, 
    ' debemos siempre buscar el principio de la separacion de responsabilidades
    ' El metodo del calculo tarda milisegundos en ejecutar.
    ' Las pruebas unitarias siempre deben ser rapidas para dar feedback pronto.
    ' Deben ser repetibles y nunca fallar por componentes externos.
    Public Function Sume(x As Integer, y As Integer) As Integer
        Return x + y
    End Function

    ' Si escribimos codigo de esta manera, 
    ' sera mas sencillo escribir las pruebas, 
    ' se vera sus resultados mas rapido,
    ' y se confiara en ellas para mejorar el software.

    Public Sub EnviarEmailExitoso(mensaje As String)
        Dim message As New MailMessage
        message.Subject = "Exito! " + mensaje
        Dim client = New SmtpClient()
        client.Send(message)
    End Sub

End Class

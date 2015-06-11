Imports System.IO
Imports System.Net.Mail

Public Class NoEsAislable

    ' Es observable pero no se puede aislar del servidor SMTP  
    ' Si la prueba unitaria quiere verificar la logica del calculo, 
    ' podria fallar porque el servidor de email esta fuera de linea.
    ' El servidor SMTP no esta relacionado con el comportamiento por verificar.
    Public Function Sume(x As Integer, y As Integer) As Integer
        Dim suma As Integer = x + y
        Dim resultado As String = String.Format("La respuesta es {0}", suma)

        Dim message As New MailMessage
        message.Subject = "Exito! " + resultado
        Dim client = New SmtpClient()
        client.Send(message)

        Return suma
    End Function

End Class

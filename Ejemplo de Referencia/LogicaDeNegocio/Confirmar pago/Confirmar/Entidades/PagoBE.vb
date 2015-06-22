Public Class PagoBE

#Region "Confirmacion"

    Private _estadoInterno As PagoPorConfirmar

    Public Sub New(estadoInterno As PagoPorConfirmar)
        _estadoInterno = estadoInterno
    End Sub

    Public Function SePuedeConfirmar() As Boolean
        Dim respuesta As Boolean = False
        If SeHaAutorizado() And Not SeHaNotificado() Then
            respuesta = True
        End If
        Return respuesta
    End Function

    Private Function SeHaAutorizado() As Boolean
        Return _estadoInterno.Estado = EstadoPago.Autorizado
    End Function

    Function SeHaNotificado() As Boolean
        Return _estadoInterno.SeHaNotificado
    End Function

#End Region

    Function MarcarPagoComoConfirmado(fechaDeConfirmacion As Date) As PagoPorConfirmar
        _estadoInterno.FechaDeConfirmacion = fechaDeConfirmacion
        _estadoInterno.SeHaNotificado = True
        Return _estadoInterno
    End Function

End Class

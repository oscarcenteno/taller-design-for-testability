Public Class TransaccionBE

#Region "Confirmacion"

    Private _estadoInterno As TransaccionDTO

    Public Sub New(estadoInterno As TransaccionDTO)
        _estadoInterno = estadoInterno
    End Sub

    Function ObtenerDatosDeTransaccion() As TransaccionDTO
        Return _estadoInterno
    End Function

    Public ReadOnly Property FechaDeConfirmacion As Date
        Get
            Return _estadoInterno.FechaDeConfirmacion
        End Get
    End Property
    Public Function SePuedeConfirmar() As Boolean
        Dim respuesta As Boolean = False
        If SeHaAutorizado() And Not SeHaNotificado() Then
            respuesta = True
        End If
        Return respuesta
    End Function

    Public Function ErrorDeValidacion() As ErrorTransaccionDebeEstarAutorizadaYNoNotificada
        Dim respuesta As ErrorTransaccionDebeEstarAutorizadaYNoNotificada = Nothing
        If Not SePuedeConfirmar() Then
            respuesta = New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(Me._estadoInterno)
        End If
        Return respuesta
    End Function

    Private Function SeHaAutorizado() As Boolean
        Return _estadoInterno.Estado = EstadoTransaccion.Autorizada
    End Function

    Function SeHaNotificado() As Boolean
        Return _estadoInterno.SeHaNotificado
    End Function

    Sub Confirmar(fechaDeConfirmacion As Date)
        _estadoInterno.FechaDeConfirmacion = fechaDeConfirmacion
        _estadoInterno.SeHaNotificado = True
    End Sub

#End Region

End Class

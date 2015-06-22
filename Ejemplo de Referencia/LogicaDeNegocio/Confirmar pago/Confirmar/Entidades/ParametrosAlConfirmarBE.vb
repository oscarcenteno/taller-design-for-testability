Public Class ParametrosAlConfirmarBE

    Private _estadoInterno As ParametrosAlConfirmar

    Public Sub New(estadoInterno As ParametrosAlConfirmar)
        _estadoInterno = estadoInterno
    End Sub

    Public Function SePuedeInvocarAEntidad() As Boolean
        Dim respuesta As Boolean = False

        respuesta = Not String.IsNullOrEmpty(_estadoInterno.Url)
        respuesta = respuesta And Not String.IsNullOrEmpty(_estadoInterno.TimeOut)
        respuesta = respuesta And Not String.IsNullOrEmpty(_estadoInterno.Cn)

        Return respuesta
    End Function

End Class

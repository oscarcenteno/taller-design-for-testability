Public Class ParametrosAlConfirmarBE
    Private _url As String
    Private _timeOut As Integer
    Private _cn As String
    Private _miTransaccion As TransaccionDTO

    Public Sub New(miTransaccion As TransaccionDTO, estadoInterno As ParametrosAlConfirmarDTO)
        _url = estadoInterno.Url
        _timeOut = estadoInterno.TimeOut
        _cn = estadoInterno.Cn
        _miTransaccion = miTransaccion
    End Sub

    Public Function SePuedeInvocarAEntidad() As Boolean
        Dim respuesta As Boolean = False

        respuesta = Not String.IsNullOrEmpty(_url)
        respuesta = respuesta And Not String.IsNullOrEmpty(_timeOut)
        respuesta = respuesta And Not String.IsNullOrEmpty(_cn)

        Return respuesta
    End Function

    Public Function ErrorDeValidacion() As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos
        Dim respuesta As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos = Nothing
        If Not SePuedeInvocarAEntidad() Then
            respuesta = New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(_miTransaccion)
        End If
        Return respuesta
    End Function

End Class

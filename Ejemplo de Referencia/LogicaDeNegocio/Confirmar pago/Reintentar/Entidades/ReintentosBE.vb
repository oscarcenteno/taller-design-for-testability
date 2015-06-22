Public Class ReintentosBE

    Private _parametros As ParametrosAlReintentar
    Private _solicitud As SolicitudAlConfirmar

    Sub New(parametros As ParametrosAlReintentar, solicitud As SolicitudAlConfirmar)
        _parametros = parametros
        _solicitud = solicitud
    End Sub

    Public Function SePuedeReintentar() As Boolean
        Dim resultado = False
        If NoHaSuperadoLosIntentosMaximos() Then
            resultado = True
        End If
        Return resultado
    End Function

    Private Function NoHaSuperadoLosIntentosMaximos() As Boolean
        Return _solicitud.IntentosYaRealizados < _parametros.CantidadMaximaDeIntentos
    End Function

    Public Function ObtenerInstruccionParaReintentar() As InstruccionParaReintentar
        ' Datos calculados
        Dim numeroDeEsteIntento As Integer = _solicitud.IntentosYaRealizados + 1
        Dim fechaYHoraCalendarizada As Date = Nothing
        Dim ts As TimeSpan
        TimeSpan.TryParse(_parametros.IntervaloDeNotificacion, ts)
        fechaYHoraCalendarizada = _solicitud.FechaYHoraActual.Add(ts)

        Dim respuesta As New InstruccionParaReintentar(_solicitud.CodigoDeReferencia, fechaYHoraCalendarizada, numeroDeEsteIntento)
        Return respuesta
    End Function

End Class

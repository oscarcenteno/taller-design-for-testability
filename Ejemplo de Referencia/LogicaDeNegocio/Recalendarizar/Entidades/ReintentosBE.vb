Public Class ReintentosBE

    ' Datos del constructor
    Private _parametros As ParametrosAlRecalendarizarDTO
    Private _codReferencia As String
    Private _intentosYaRealizados As Integer
    Private _fechaYHoraActual As Date

    ' Datos calculados
    Private _numeroDeEsteIntento As Integer
    Private _fechaYHoraCalendarizada As Date

    Public Sub New(parametros As ParametrosAlRecalendarizarDTO, laTransaccion As TransaccionDTO, intentosYaRealizados As Integer, fechaYHoraActual As Date)
        _parametros = parametros
        _codReferencia = laTransaccion.CodigoDeReferencia
        _intentosYaRealizados = intentosYaRealizados
        _fechaYHoraActual = fechaYHoraActual
        _numeroDeEsteIntento = _intentosYaRealizados + 1

        Dim ts As TimeSpan
        TimeSpan.TryParse(_parametros.IntervaloDeNotificacion, ts)
        _fechaYHoraCalendarizada = _fechaYHoraActual.Add(ts)
    End Sub

    Public Function SePuedeReintentar() As Boolean
        Dim resultado = False
        If NoHaSuperadoLosIntentosMaximos(_intentosYaRealizados) Then
            resultado = True
        End If
        Return resultado
    End Function

    Private Function NoHaSuperadoLosIntentosMaximos(intentosYaRealizados As Integer) As Boolean
        Return intentosYaRealizados < _parametros.CantidadMaximaDeIntentos
    End Function

    Public Function ObtenerInstruccionParaReintentar() As InstruccionParaRecalendarizar
        Dim respuesta As New InstruccionParaRecalendarizar(_codReferencia, _fechaYHoraCalendarizada, _numeroDeEsteIntento)
        Return respuesta
    End Function

    Public Function ObtenerMensajeTransaccionFueCalendarizada() As MensajeTransaccionFueRecalendarizada
        Dim respuesta As New MensajeTransaccionFueRecalendarizada(_codReferencia, _numeroDeEsteIntento, _fechaYHoraCalendarizada)
        Return respuesta
    End Function

End Class

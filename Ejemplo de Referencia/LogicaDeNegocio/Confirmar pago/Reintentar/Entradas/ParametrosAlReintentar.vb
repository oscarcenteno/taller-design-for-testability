
Public Structure ParametrosAlReintentar

    Public Property CantidadMaximaDeIntentos As Integer
    Public Property IntervaloDeNotificacion As String

    Public Sub New(CantidadMaximaDeIntentos As Integer, IntervaloDeNotificacion As String)
        Me.CantidadMaximaDeIntentos = CantidadMaximaDeIntentos
        Me.IntervaloDeNotificacion = IntervaloDeNotificacion
    End Sub

End Structure

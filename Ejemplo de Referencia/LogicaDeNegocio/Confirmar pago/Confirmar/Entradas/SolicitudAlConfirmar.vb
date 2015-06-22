Public Structure SolicitudAlConfirmar
    Public Property CodigoDeReferencia As String
    Public Property IntentosYaRealizados As Integer
    Public Property FechaYHoraActual As Date

    Public Sub New(CodigoDeReferencia As String, IntentosYaRealizados As Integer, FechaYHoraActual As Date)
        Me.CodigoDeReferencia = CodigoDeReferencia
        Me.IntentosYaRealizados = IntentosYaRealizados
        Me.FechaYHoraActual = FechaYHoraActual
    End Sub

End Structure

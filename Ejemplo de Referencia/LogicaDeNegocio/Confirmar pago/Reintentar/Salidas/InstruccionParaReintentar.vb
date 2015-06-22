Public Structure InstruccionParaReintentar

    Public Property CodigoDeReferencia As String
    Public Property FechaDeInicio As Date
    Public Property NumeroDeReintento As Integer

    Public Sub New(CodigoDeReferencia As String, FechaDeInicio As Date, NumeroDeReintento As Integer)
        Me.CodigoDeReferencia = CodigoDeReferencia
        Me.FechaDeInicio = FechaDeInicio
        Me.NumeroDeReintento = NumeroDeReintento
    End Sub

End Structure

Public Class InstruccionParaReintentar
    Implements IEquatable(Of InstruccionParaReintentar)

    Public Property CodigoDeReferencia As String
    Public Property FechaDeInicio As Date
    Public Property NumeroDeReintento As Integer

    Public Sub New(CodigoDeReferencia As String, FechaDeInicio As Date, NumeroDeReintento As Integer)
        Me.CodigoDeReferencia = CodigoDeReferencia
        Me.FechaDeInicio = FechaDeInicio
        Me.NumeroDeReintento = NumeroDeReintento
    End Sub

    Public Overloads Function Equals(other As InstruccionParaReintentar) _
            As Boolean Implements IEquatable(Of InstruccionParaReintentar).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqCodReferencia = CodigoDeReferencia.Equals(other.CodigoDeReferencia)
            Dim eqFechaDeInicio = FechaDeInicio.Equals(other.FechaDeInicio)
            Dim eqNumeroDeReintento = NumeroDeReintento.Equals(other.NumeroDeReintento)
            respuesta = eqCodReferencia And eqFechaDeInicio And eqNumeroDeReintento
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, InstruccionParaReintentar))
    End Function




End Class

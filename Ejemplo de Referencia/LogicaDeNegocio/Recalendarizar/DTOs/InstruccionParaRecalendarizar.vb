
Public Class InstruccionParaRecalendarizar
    Implements IEquatable(Of InstruccionParaRecalendarizar)

    Public Property CodigoDeReferencia As String
    Public Property FechaDeInicio As Date
    Public Property NumeroDeReintento As Integer

    Public Sub New(elCodigoDeReferencia As String, laFechaDeInicio As Date, elNumeroDeReintento As Integer)
        CodigoDeReferencia = elCodigoDeReferencia
        FechaDeInicio = laFechaDeInicio
        NumeroDeReintento = elNumeroDeReintento
    End Sub

    Public Sub New()
        CodigoDeReferencia = String.Empty
        FechaDeInicio = New Date
        NumeroDeReintento = 0
    End Sub

    Public Overloads Function Equals(other As InstruccionParaRecalendarizar) _
    As Boolean Implements IEquatable(Of InstruccionParaRecalendarizar).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqFechaInicio = FechaDeInicio.Equals(other.FechaDeInicio)
            Dim eqIntentos = NumeroDeReintento.Equals(other.NumeroDeReintento)
            Dim eqCodReferencia = CodigoDeReferencia.Equals(other.CodigoDeReferencia)
            respuesta = eqFechaInicio And eqIntentos And eqCodReferencia
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, InstruccionParaRecalendarizar))
    End Function

End Class

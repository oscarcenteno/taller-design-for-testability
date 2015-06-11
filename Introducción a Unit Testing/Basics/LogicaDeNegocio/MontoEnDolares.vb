Public Class MontoEnDolares
    Implements System.IEquatable(Of MontoEnDolares)

    Private _monto As Double

    Public Sub New(nuevoMonto As Double)
        _monto = nuevoMonto
    End Sub

    Public ReadOnly Property Valor As Double
        Get
            Return _monto
        End Get
    End Property

    ' $25
    Public Shared Function Parse(monto As String) As MontoEnDolares
        monto = monto.Trim
        Dim montoSinCurrency As String = monto.Replace("$", String.Empty)
        Dim montoNumerico As Double = 0
        Dim resultado As MontoEnDolares = Nothing

        If monto.StartsWith("$") And Double.TryParse(montoSinCurrency, montoNumerico) Then
            resultado = New MontoEnDolares(montoNumerico)
        Else
            Throw New ArgumentException(String.Format("'{0}' no es un monto en dólares", monto))
        End If

        Return resultado

    End Function

    ' $25
    Public Overrides Function ToString() As String
        Return String.Format("${0}", _monto)
    End Function

    Public Overloads Function Equals(ByVal other As MontoEnDolares) As Boolean _
        Implements System.IEquatable(Of MontoEnDolares).Equals

        Dim result As Boolean = False

        If other Is Nothing Then
            result = False
        ElseIf other Is Me Then
            result = True
        Else
            result = Me._monto.Equals(other.Valor)
        End If

        Return result
    End Function

    Public Overrides Function Equals(other As Object) As Boolean

        Dim temp = TryCast(other, MontoEnDolares)
        Dim result As Boolean = False

        If temp IsNot Nothing Then
            result = Me.Equals(temp)
        Else
            result = False
        End If

        Return result

    End Function

End Class
Public Class ErrorTransaccionNoEstaAutorizada
    Implements IEquatable(Of ErrorTransaccionNoEstaAutorizada)

    Private _codReferencia As String

    Public Sub New(miTransaccion As TransaccionDTO)
        _codReferencia = miTransaccion.CodReferencia
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("No se confirmó la transacción con Cod. Referencia [{0}] pues no está autorizada", _codReferencia)
    End Function

    Public Overloads Function Equals(other As ErrorTransaccionNoEstaAutorizada) As Boolean Implements IEquatable(Of ErrorTransaccionNoEstaAutorizada).Equals
        Return Me._codReferencia.Equals(other._codReferencia)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, ErrorTransaccionNoEstaAutorizada))
    End Function

End Class

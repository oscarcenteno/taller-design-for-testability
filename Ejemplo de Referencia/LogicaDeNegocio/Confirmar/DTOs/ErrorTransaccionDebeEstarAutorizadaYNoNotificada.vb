Public Class ErrorTransaccionDebeEstarAutorizadaYNoNotificada
    Implements IEquatable(Of ErrorTransaccionDebeEstarAutorizadaYNoNotificada)

    Private _codReferencia As String

    Public Sub New(miTransaccion As TransaccionDTO)
        _codReferencia = miTransaccion.CodigoDeReferencia
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("No se confirmó la transacción con Cod. Referencia [{0}] pues debe estar autorizada y no notificada.", _codReferencia)
    End Function

    Public Overloads Function Equals(other As ErrorTransaccionDebeEstarAutorizadaYNoNotificada) As Boolean Implements IEquatable(Of ErrorTransaccionDebeEstarAutorizadaYNoNotificada).Equals
        Return Me._codReferencia.Equals(other._codReferencia)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, ErrorTransaccionDebeEstarAutorizadaYNoNotificada))
    End Function

End Class

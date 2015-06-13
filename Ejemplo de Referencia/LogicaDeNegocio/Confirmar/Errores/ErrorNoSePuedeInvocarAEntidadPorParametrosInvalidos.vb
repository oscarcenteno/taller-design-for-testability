Public Class ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos
    Implements IEquatable(Of ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos)

    Private _codReferencia As String
    Private _codEntidad As String

    Public Sub New(codReferencia As String, codEntidad As String)
        _codReferencia = codReferencia
        _codEntidad = codEntidad
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("No se confirmó la transacción {0} pues los parametros requeridos para invocar a la entidad {1} no están configurados", _codEntidad, _codReferencia)
    End Function

    Public Overloads Function Equals(other As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos) As Boolean _
        Implements IEquatable(Of ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos).Equals
        Return Me._codReferencia.Equals(other._codReferencia) And _
        Me._codEntidad.Equals(other._codEntidad)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos))
    End Function
End Class

Public Class InformativoTransaccionFueConfirmada
    Implements IEquatable(Of InformativoTransaccionFueConfirmada)

    Private _codReferencia As String
    Private _codEntidadOrigen As String
    Private _codMoneda As String
    Private _numTelefono As String
    Private _codEntidadPadronMovilDestino As String

    Public Sub New(miTransaccion As TransaccionDTO)
        _codReferencia = miTransaccion.CodReferencia
        _codEntidadOrigen = miTransaccion.CodEntidadOrigen
        _codMoneda = miTransaccion.CodMonedaPadronMovilDestino
        _numTelefono = miTransaccion.NumTelefonoPadronMovilDestino
        _codEntidadPadronMovilDestino = miTransaccion.CodEntidadPadronMovilDestino
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Se confirmó la operación con referencia [{0}] de la entidad origen [{1}], en moneda [{2}] hacia el teléfono [{3}] asociado a la entidad destino [{4}]", _
                                                         _codReferencia, _codEntidadOrigen, _codMoneda, _
                                                         _numTelefono, _codEntidadPadronMovilDestino)
    End Function

    Public Overloads Function Equals(other As InformativoTransaccionFueConfirmada) As Boolean Implements IEquatable(Of InformativoTransaccionFueConfirmada).Equals
        Return Me.ToString.Equals(other.ToString)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        Dim respuesta As Boolean = False

        If obj IsNot Nothing Then
            respuesta = Me.Equals(CType(obj, InformativoTransaccionFueConfirmada))
        End If

        Return respuesta
    End Function

End Class
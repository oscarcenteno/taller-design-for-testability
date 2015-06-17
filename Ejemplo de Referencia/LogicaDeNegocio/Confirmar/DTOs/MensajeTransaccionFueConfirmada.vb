Public Class MensajeTransaccionFueConfirmada
    Implements IEquatable(Of MensajeTransaccionFueConfirmada)

    Private _codReferencia As String
    Private _codEntidadOrigen As String
    Private _codMoneda As String
    Private _numTelefono As String
    Private _codEntidadDestino As String

    Public Sub New(miTransaccion As TransaccionDTO)
        _codReferencia = miTransaccion.CodigoDeReferencia
        _codEntidadOrigen = miTransaccion.CodigoDeEntidadOrigen
        _codMoneda = miTransaccion.CodigoDeMonedaDestino
        _numTelefono = miTransaccion.NumeroDeTelefonoDestino
        _codEntidadDestino = miTransaccion.CodigoDeEntidadDestino
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Se confirmó la operación con referencia [{0}] de la entidad origen [{1}], en moneda [{2}] hacia el teléfono [{3}] asociado a la entidad destino [{4}]", _
                                                         _codReferencia, _codEntidadOrigen, _codMoneda, _
                                                         _numTelefono, _codEntidadDestino)
    End Function

    Public Overloads Function Equals(other As MensajeTransaccionFueConfirmada) As Boolean Implements IEquatable(Of MensajeTransaccionFueConfirmada).Equals
        Return Me.ToString.Equals(other.ToString)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        Dim respuesta As Boolean = False

        If obj IsNot Nothing Then
            respuesta = Me.Equals(CType(obj, MensajeTransaccionFueConfirmada))
        End If

        Return respuesta
    End Function

End Class
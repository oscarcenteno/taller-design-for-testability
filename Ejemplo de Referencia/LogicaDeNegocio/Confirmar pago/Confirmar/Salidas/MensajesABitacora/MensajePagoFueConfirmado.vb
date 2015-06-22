Imports LogicaDeNegocio

Public Structure MensajePagoFueConfirmado

    Public CodigoDeReferencia As String
    Public CodigoDeEntidadOrigen As String
    Public CodigoDeMoneda As String
    Public NumeroDeTelefono As String
    Public CodigoDeEntidadDestino As String

    Public Sub New(miTransaccion As PagoPorConfirmar)
        CodigoDeReferencia = miTransaccion.CodigoDeReferencia
        CodigoDeEntidadOrigen = miTransaccion.CodigoDeEntidadOrigen
        CodigoDeMoneda = miTransaccion.CodigoDeMonedaDestino
        NumeroDeTelefono = miTransaccion.NumeroDeTelefonoDestino
        CodigoDeEntidadDestino = miTransaccion.CodigoDeEntidadDestino
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Se confirmó el pago con referencia [{0}] de la entidad origen [{1}], en moneda [{2}] hacia el teléfono [{3}] asociado a la entidad destino [{4}]", _
                                                         CodigoDeReferencia, CodigoDeEntidadOrigen, CodigoDeMoneda, _
                                                         NumeroDeTelefono, CodigoDeEntidadDestino)
    End Function

End Structure
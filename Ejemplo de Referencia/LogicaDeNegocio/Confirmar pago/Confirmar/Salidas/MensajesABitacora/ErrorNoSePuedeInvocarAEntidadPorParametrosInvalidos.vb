Imports LogicaDeNegocio

Public Structure ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos

    Private CodigoDeReferencia As String
    Private CodigoDeEntidadDestino As String

    Public Sub New(transaccion As PagoPorConfirmar)
        CodigoDeReferencia = transaccion.CodigoDeReferencia
        CodigoDeEntidadDestino = transaccion.CodigoDeEntidadDestino
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("No se confirmó el pago {0} pues los parametros requeridos para invocar a la entidad {1} no están configurados", CodigoDeReferencia, CodigoDeEntidadDestino)
    End Function

End Structure

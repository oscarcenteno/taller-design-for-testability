Imports LogicaDeNegocio

Public Interface IRepositorioAlConfirmar

    Function ObtenerPagoPorConfirmar(codReferencia As String) As PagoPorConfirmar

    Function ObtenerParametrosParInvocarEntidad(codEntidadDestino As Integer) As ParametrosAlConfirmar

    Function ObtenerParametrosParaReintentar() As ParametrosAlReintentar

    Sub ActualizarPagoConfirmado(miPago As PagoConfirmado)

End Interface

Imports LogicaDeNegocio

Public Interface IRepositorioAlConfirmar

    Function ObtenerPagoPorConfirmar(codReferencia As String) As PagoPorConfirmar

    Function ObtenerParametros(datosDeTransaccion As PagoPorConfirmar) As ParametrosAlConfirmar

    Function ObtenerParametrosParaReintentar() As ParametrosAlReintentar
    Sub ActualizarPagoConfirmado(datosDeTransaccion As PagoPorConfirmar)

End Interface

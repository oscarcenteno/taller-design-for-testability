Public Interface IInvocadorAlConfirmar

    Function Confirmar(datosDeTransaccion As LogicaDeNegocio.TransaccionDTO, parametros As LogicaDeNegocio.ParametrosAlConfirmar, fecha As Date) As RespuestaAlInvocarEntidad

End Interface

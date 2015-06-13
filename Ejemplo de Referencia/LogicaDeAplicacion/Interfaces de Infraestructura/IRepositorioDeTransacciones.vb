Public Interface IRepositorioDeTransacciones

    Sub Actualizar(datosDeTransaccion As LogicaDeNegocio.TransaccionDTO)

    Function Obtener(codReferencia As String) As LogicaDeNegocio.TransaccionDTO

End Interface

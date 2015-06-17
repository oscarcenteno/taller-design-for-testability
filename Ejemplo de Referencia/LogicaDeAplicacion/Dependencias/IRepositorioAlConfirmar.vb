Imports LogicaDeNegocio

Public Interface IRepositorioAlConfirmar

    Sub ActualizarTransaccionConfirmada(datosDeTransaccion As TransaccionDTO)

    Function ObtenerTransaccion(codReferencia As String) As TransaccionDTO

    Function ObtenerParametros(datosDeTransaccion As TransaccionDTO) As ParametrosAlConfirmarDTO

    Function ObtenerParametrosParaRecalendarizar() As ParametrosAlRecalendarizarDTO

End Interface

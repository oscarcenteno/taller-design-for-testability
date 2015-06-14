Imports LogicaDeNegocio

Public Interface IRepositorioAlConfirmar

    Sub ActualizarTransaccionConfirmada(datosDeTransaccion As TransaccionDTO)

    Function ObtenerTransaccion(codReferencia As String) As TransaccionDTO

    Function ObtenerParametrosParaConfirmar(datosDeTransaccion As TransaccionDTO) As Object

    Function ObtenerParametrosParaRecalendarizar() As ParametrosAlRecalendarizar

End Interface

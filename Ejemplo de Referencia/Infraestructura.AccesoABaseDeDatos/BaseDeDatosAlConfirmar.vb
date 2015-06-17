Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class BaseDeDatosAlConfirmar
    Implements IRepositorioAlConfirmar


    Public Sub ActualizarTransaccionConfirmada(datosDeTransaccion As TransaccionDTO) Implements IRepositorioAlConfirmar.ActualizarTransaccionConfirmada

    End Sub

    Public Function ObtenerParametros(datosDeTransaccion As TransaccionDTO) As LogicaDeNegocio.ParametrosAlConfirmarDTO Implements IRepositorioAlConfirmar.ObtenerParametros

    End Function

    Public Function ObtenerParametrosParaRecalendarizar() As ParametrosAlRecalendarizarDTO Implements IRepositorioAlConfirmar.ObtenerParametrosParaRecalendarizar

    End Function

    Public Function ObtenerTransaccion(codReferencia As String) As LogicaDeNegocio.TransaccionDTO Implements IRepositorioAlConfirmar.ObtenerTransaccion

    End Function

End Class

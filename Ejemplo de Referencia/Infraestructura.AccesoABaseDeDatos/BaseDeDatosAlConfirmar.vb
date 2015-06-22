Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class BaseDeDatosAlConfirmar
    Implements IRepositorioAlConfirmar

    Public Sub ActualizarPagoConfirmado(datosDeTransaccion As PagoPorConfirmar) Implements IRepositorioAlConfirmar.ActualizarPagoConfirmado
        Throw New NotImplementedException
    End Sub

    Public Function ObtenerParametros(datosDeTransaccion As PagoPorConfirmar) As LogicaDeNegocio.ParametrosAlConfirmar Implements IRepositorioAlConfirmar.ObtenerParametros
        Throw New NotImplementedException
    End Function

    Public Function ObtenerParametrosParaRecalendarizar() As ParametrosAlReintentar Implements IRepositorioAlConfirmar.ObtenerParametrosParaReintentar
        Throw New NotImplementedException
    End Function

    Public Function ObtenerPagoPorConfirmar(codReferencia As String) As LogicaDeNegocio.PagoPorConfirmar Implements IRepositorioAlConfirmar.ObtenerPagoPorConfirmar
        Throw New NotImplementedException
    End Function

End Class

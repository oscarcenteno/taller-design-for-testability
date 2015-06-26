Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class BaseDeDatosAlConfirmar
    Implements IRepositorioAlConfirmar

    Public Sub ActualizarPagoConfirmado(elPago As PagoConfirmado) Implements IRepositorioAlConfirmar.ActualizarPagoConfirmado
        Throw New NotImplementedException
    End Sub

    Public Function ObtenerParametrosParInvocarEntidad(codEntidadDestino As Integer) As LogicaDeNegocio.ParametrosAlConfirmar Implements IRepositorioAlConfirmar.ObtenerParametrosParInvocarEntidad
        Throw New NotImplementedException
    End Function

    Public Function ObtenerParametrosParaRecalendarizar() As ParametrosAlReintentar Implements IRepositorioAlConfirmar.ObtenerParametrosParaReintentar
        Throw New NotImplementedException
    End Function

    Public Function ObtenerPagoPorConfirmar(codReferencia As String) As LogicaDeNegocio.PagoPorConfirmar Implements IRepositorioAlConfirmar.ObtenerPagoPorConfirmar
        Throw New NotImplementedException
    End Function

End Class

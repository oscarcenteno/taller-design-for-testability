Imports LogicaDeNegocio

Public Structure ErrorPagoDebeEstarAutorizadoYNoNotificado

    Public CodigoDeReferencia As String

    Public Sub New(miTransaccion As PagoPorConfirmar)
        CodigoDeReferencia = miTransaccion.CodigoDeReferencia
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("No se confirmó el pago con Cod. Referencia [{0}] pues debe estar autorizado y no notificado.", CodigoDeReferencia)
    End Function

End Structure

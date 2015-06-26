Public Module MensajesABitacora
    Public Function ObtenerMensajePagoFueConfirmado(miPago As PagoPorConfirmar) As String
        Return String.Format("Se confirmó el pago con referencia [{0}] de la entidad origen [{1}], en moneda [{2}] hacia el teléfono [{3}] asociado a la entidad destino [{4}]", _
                                                                 miPago.CodigoDeReferencia, miPago.CodigoDeEntidadOrigen, miPago.CodigoDeMonedaDestino, _
                                                                 miPago.NumeroDeTelefonoDestino, miPago.CodigoDeEntidadDestino)
    End Function

    Public Function ObtenerErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(miPago As PagoPorConfirmar) As String
        Return String.Format("No se confirmó el pago {0} pues los parametros requeridos para invocar a la entidad {1} no están configurados", miPago.CodigoDeReferencia, miPago.CodigoDeEntidadDestino)
    End Function

    Public Function ObtenerErrorPagoDebeEstarAutorizadoYNoNotificado(elPago As PagoPorConfirmar) As String
        Return String.Format("No se confirmó el pago con Cod. Referencia [{0}] pues debe estar autorizado y no notificado.", elPago.CodigoDeReferencia)

    End Function

    Function ObtenerMensajeConfirmacionDePagoReintentado(instruccion As InstruccionParaReintentar) As Object
        Return String.Format("Se ha calendarizado el reintento para confirmar #{0} para el pago con referencia {1} en la fecha/hora {2}.", _
                              instruccion.NumeroDeReintento,
                              instruccion.CodigoDeReferencia,
                              instruccion.FechaDeInicio)
    End Function

End Module

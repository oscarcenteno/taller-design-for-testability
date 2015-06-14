Public Class ProcesoAlConfirmar

    Public Function ValidarProceso(parametros As ParametrosAlConfirmar, _
                                   datosDeTransaccion As TransaccionDTO, _
                                   fechaYHoraActual As Date) _
                               As RespuestaAlValidarProceso

        Dim respuesta As New RespuestaAlValidarProceso

        ' Validaciones de negocio y de parametros
        Dim laTransaccion As New TransaccionBE(datosDeTransaccion)
        respuesta.SePuedeConfirmar = laTransaccion.SePuedeConfirmar And parametros.SePuedeInvocarAEntidad

        If respuesta.SePuedeConfirmar Then
            ' Logica de negocio
            laTransaccion.Confirmar(fechaYHoraActual)
            respuesta.DatosDeTransaccionConfirmada = laTransaccion.ObtenerDatosDeTransaccion
            'Bitacora e instrucciones
            respuesta.LaInstruccionDeConfirmacion = New InstruccionDeConfirmacion(datosDeTransaccion, parametros, fechaYHoraActual)
            respuesta.MensajeDeTransaccionFueConfirmada = New InformativoTransaccionFueConfirmada(datosDeTransaccion)
        Else
            'Reporte de errores por los que el proceso no puede ejecutarse
            respuesta.Errores.AddRange(laTransaccion.ObtenerErroresAlConfirmar)
            respuesta.Errores.AddRange(parametros.ObtenerErroresDeParametros)
        End If

        Return respuesta
    End Function

    Public Function ValidarRecalendarizacion(fechaYHoraActual As Date, datosDeTransaccion As TransaccionDTO, _
                                             parametrosParaRecalendarizar As ParametrosAlRecalendarizar, _
                                             intentosYaRealizados As Integer) As RespuestaAlValidarRecalendarizacion

        Dim respuesta As New RespuestaAlValidarRecalendarizacion

        ' Validaciones de negocio y de parametros
        Dim laTransaccion As New TransaccionBE(datosDeTransaccion)

        respuesta.SePuedeReintentar = laTransaccion.SePuedeReintentarLaConfirmacion(intentosYaRealizados)
        Dim fechaYHoraCalendarizada = fechaYHoraActual.Add(parametrosParaRecalendarizar.IntervaloDeNotificacion)

        Dim numeroDeEsteIntento = intentosYaRealizados + 1

        If respuesta.SePuedeReintentar Then
            respuesta.LaInstruccionParaRecalendarizar = New InstruccionParaRecalendarizar() _
                With {.FechaInicio = fechaYHoraCalendarizada, _
                      .Intentos = numeroDeEsteIntento, _
                      .CodReferencia = datosDeTransaccion.CodReferencia _
                     }

            respuesta.MensajeABitacoraTransaccionFueRecalendarizada = _
                New InformativoTransaccionFueRecalendarizada(datosDeTransaccion, numeroDeEsteIntento, fechaYHoraCalendarizada)
        End If

        Return respuesta

    End Function

End Class

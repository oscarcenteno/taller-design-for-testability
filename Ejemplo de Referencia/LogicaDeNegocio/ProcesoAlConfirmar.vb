Public Class ProcesoAlConfirmar

    Public Function ValidarConfirmacion(parametros As ParametrosAlConfirmar, _
                                   datosDeTransaccion As TransaccionDTO, _
                                   fecha As Date) _
                               As RespuestaAlValidarProceso

        Dim respuesta As New RespuestaAlValidarProceso

        ' Validaciones de negocio y de parametros
        Dim miTransaccionBE As New TransaccionBE(datosDeTransaccion)
        respuesta.ProcesoPuedeEjecutarse = miTransaccionBE.SePuedeConfirmar And parametros.SePuedeInvocarAEntidad

        If respuesta.ProcesoPuedeEjecutarse Then
            ' Logica de negocio
            miTransaccionBE.Confirmar(fecha)
            respuesta.DatosTransaccionConfirmada = miTransaccionBE.ObtenerDatosDeTransaccion
            'Bitacora e instrucciones
            respuesta.laInstruccionDeConfirmacion = New InstruccionDeConfirmacion(datosDeTransaccion, parametros, fecha)
            respuesta.MensajeTransaccionConfirmada = New InformativoTransaccionFueConfirmada(datosDeTransaccion)
        Else
            'Reporte errores
            respuesta.Errores.AddRange(miTransaccionBE.ErroresAlConfirmar)
            respuesta.Errores.AddRange(parametros.ErroresDeParametros)
        End If

        Return respuesta
    End Function

    Public Function ValidarRecalendarizacion(fecha As Date, datosDeTransaccion As TransaccionDTO, _
                                             parametrosParaRecalendarizar As ParametrosAlRecalendarizar, _
                                             intentos As Integer) As RespuestaAlValidarRecalendarizacion

        Dim respuesta As New RespuestaAlValidarRecalendarizacion

        ' Validaciones de negocio y de parametros
        Dim miTransaccionBE As New TransaccionBE(datosDeTransaccion)

        respuesta.SePuedeReintentarLaConfirmacion = miTransaccionBE.SePuedeReintentarLaConfirmacion(intentos)
        Dim fechaInicio = fecha.Add(parametrosParaRecalendarizar.IntervaloDeNotificacion)

        Dim numeroDeEsteIntento = intentos + 1

        If respuesta.SePuedeReintentarLaConfirmacion Then
            respuesta.LaInstruccionParaRecalendarizar = New InstruccionParaRecalendarizar() _
                With {.FechaInicio = fechaInicio, .Intentos = numeroDeEsteIntento, _
                      .CodReferencia = datosDeTransaccion.CodReferencia}

            respuesta.MensajeABitacoraTransaccionFueRecalendarizada = _
                New InformativoTransaccionFueRecalendarizada(datosDeTransaccion, numeroDeEsteIntento, fechaInicio)
        End If

        Return respuesta


    End Function

End Class


Public Class RespuestaAlValidarRecalendarizacion
    Implements IEquatable(Of RespuestaAlValidarRecalendarizacion)

    Property SePuedeReintentarLaConfirmacion As Boolean
    Property LaInstruccionParaRecalendarizar As InstruccionParaRecalendarizar
    Property MensajeABitacoraTransaccionFueRecalendarizada As InformativoTransaccionFueRecalendarizada

    Public Overloads Function Equals(other As RespuestaAlValidarRecalendarizacion) _
As Boolean Implements IEquatable(Of RespuestaAlValidarRecalendarizacion).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqSePuedeReintentarLaConfirmacion = SePuedeReintentarLaConfirmacion.Equals(other.SePuedeReintentarLaConfirmacion)
            Dim eqInstruccionParaRecalendarizar = InstruccionParaRecalendarizar.Equals(LaInstruccionParaRecalendarizar, other.LaInstruccionParaRecalendarizar)
            Dim eqMensajeTransaccionConfirmada = InformativoTransaccionFueRecalendarizada.Equals(MensajeABitacoraTransaccionFueRecalendarizada, other.MensajeABitacoraTransaccionFueRecalendarizada)
            respuesta = eqSePuedeReintentarLaConfirmacion And eqInstruccionParaRecalendarizar And eqMensajeTransaccionConfirmada
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, RespuestaAlValidarRecalendarizacion))
    End Function

End Class

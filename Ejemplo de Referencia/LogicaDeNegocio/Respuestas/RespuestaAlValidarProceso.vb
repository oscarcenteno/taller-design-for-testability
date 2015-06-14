
Public Class RespuestaAlValidarProceso
    Implements IEquatable(Of RespuestaAlValidarProceso)

    Property SePuedeConfirmar As Boolean
    Property MensajeDeTransaccionFueConfirmada As InformativoTransaccionFueConfirmada
    Property LaInstruccionDeConfirmacion As InstruccionDeConfirmacion
    Property Errores() As List(Of Object)
    Property DatosDeTransaccionConfirmada As TransaccionDTO

    Public Sub New()
        SePuedeConfirmar = False
        Errores = New List(Of Object)
        DatosDeTransaccionConfirmada = New TransaccionDTO()
    End Sub

    Public Overloads Function Equals(other As RespuestaAlValidarProceso) _
    As Boolean Implements IEquatable(Of RespuestaAlValidarProceso).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqDatos = DatosDeTransaccionConfirmada.Equals(other.DatosDeTransaccionConfirmada)
            Dim eqProcesoPuedeEjecutarse = SePuedeConfirmar.Equals(other.SePuedeConfirmar)
            Dim eqMensajeTransaccionConfirmada = InformativoTransaccionFueConfirmada.Equals(MensajeDeTransaccionFueConfirmada, other.MensajeDeTransaccionFueConfirmada)
            Dim eqInstruccionDeConfirmacion = InstruccionDeConfirmacion.Equals(LaInstruccionDeConfirmacion, other.LaInstruccionDeConfirmacion)
            respuesta = eqDatos And eqProcesoPuedeEjecutarse And eqMensajeTransaccionConfirmada And eqInstruccionDeConfirmacion
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, RespuestaAlValidarProceso))
    End Function

End Class

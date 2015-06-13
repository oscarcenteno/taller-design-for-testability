
Public Class RespuestaAlValidarProceso
    Implements IEquatable(Of RespuestaAlValidarProceso)

    Property ProcesoPuedeEjecutarse As Boolean
    Property MensajeTransaccionConfirmada As InformativoTransaccionFueConfirmada
    Property laInstruccionDeConfirmacion As InstruccionDeConfirmacion
    Property Errores() As List(Of Object)
    Property DatosTransaccionConfirmada As TransaccionDTO

    Public Sub New()
        ProcesoPuedeEjecutarse = False
        Errores = New List(Of Object)
        DatosTransaccionConfirmada = New TransaccionDTO()
    End Sub

    Public Overloads Function Equals(other As RespuestaAlValidarProceso) _
    As Boolean Implements IEquatable(Of RespuestaAlValidarProceso).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqDatos = DatosTransaccionConfirmada.Equals(other.DatosTransaccionConfirmada)
            Dim eqProcesoPuedeEjecutarse = ProcesoPuedeEjecutarse.Equals(other.ProcesoPuedeEjecutarse)
            Dim eqMensajeTransaccionConfirmada = InformativoTransaccionFueConfirmada.Equals(MensajeTransaccionConfirmada, other.MensajeTransaccionConfirmada)
            Dim eqInstruccionDeConfirmacion = InstruccionDeConfirmacion.Equals(laInstruccionDeConfirmacion, other.laInstruccionDeConfirmacion)
            respuesta = eqDatos And eqProcesoPuedeEjecutarse And eqMensajeTransaccionConfirmada And eqInstruccionDeConfirmacion
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, RespuestaAlValidarProceso))
    End Function

End Class

Public Class TransaccionDTO
    Implements IEquatable(Of TransaccionDTO)

    Property CodReferencia As String
    Property Estado As EstadoTransaccion
    Property FecValor As Date

    Property CodEntidadOrigen As String

    Property CodEntidadDestino As String
    Property NumTelefonoDestino As String
    Property CodMonedaDestino As String

    Property FecConfirmacion As Date
    Property SeHaNotificado As Boolean


    Public Sub New()
        Estado = EstadoTransaccion.Invalido
        SeHaNotificado = False
        FecConfirmacion = Nothing
        CodReferencia = String.Empty
        CodEntidadOrigen = String.Empty
        NumTelefonoDestino = String.Empty
        CodMonedaDestino = String.Empty
        CodEntidadDestino = String.Empty
        FecValor = New Date
    End Sub


    Public Overloads Function Equals(other As TransaccionDTO) As Boolean _
        Implements IEquatable(Of TransaccionDTO).Equals
        Dim respuesta As Boolean = False
        respuesta = Me.Estado.Equals(other.Estado) And _
        Me.SeHaNotificado.Equals(other.SeHaNotificado) And _
        Me.FecConfirmacion.Equals(other.FecConfirmacion) And _
        Me.CodReferencia.Equals(other.CodReferencia) And _
        Me.CodEntidadOrigen.Equals(other.CodEntidadOrigen) And _
        Me.NumTelefonoDestino.Equals(other.NumTelefonoDestino) And _
        Me.CodMonedaDestino.Equals(other.CodMonedaDestino) And _
        Me.FecValor.Equals(other.FecValor) And _
        Me.CodEntidadDestino.Equals(other.CodEntidadDestino)

        Console.WriteLine("HolamUnod")
        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, TransaccionDTO))
    End Function

End Class

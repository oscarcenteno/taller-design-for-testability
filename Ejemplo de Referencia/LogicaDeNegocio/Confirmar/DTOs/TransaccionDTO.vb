Public Class TransaccionDTO
    Implements IEquatable(Of TransaccionDTO)

    Property CodigoDeReferencia As String
    Property CodigoDeEntidadDestino As String
    Property CodigoDeMonedaDestino As String
    Property CodigoDeEntidadOrigen As String
    Property NumeroDeTelefonoDestino As String
    Property Estado As EstadoTransaccion
    Property FechaValor As Date
    Property FechaDeConfirmacion As Date
    Property SeHaNotificado As Boolean


    Public Sub New()
        Estado = EstadoTransaccion.Invalido
        SeHaNotificado = False
        FechaDeConfirmacion = Nothing
        CodigoDeReferencia = String.Empty
        CodigoDeEntidadOrigen = String.Empty
        NumeroDeTelefonoDestino = String.Empty
        CodigoDeMonedaDestino = String.Empty
        CodigoDeEntidadDestino = String.Empty
        FechaValor = New Date
    End Sub


    Public Overloads Function Equals(other As TransaccionDTO) As Boolean _
        Implements IEquatable(Of TransaccionDTO).Equals
        Dim respuesta As Boolean = False
        respuesta = Me.Estado.Equals(other.Estado) And _
        Me.SeHaNotificado.Equals(other.SeHaNotificado) And _
        Me.FechaDeConfirmacion.Equals(other.FechaDeConfirmacion) And _
        Me.CodigoDeReferencia.Equals(other.CodigoDeReferencia) And _
        Me.CodigoDeEntidadOrigen.Equals(other.CodigoDeEntidadOrigen) And _
        Me.NumeroDeTelefonoDestino.Equals(other.NumeroDeTelefonoDestino) And _
        Me.CodigoDeMonedaDestino.Equals(other.CodigoDeMonedaDestino) And _
        Me.FechaValor.Equals(other.FechaValor) And _
        Me.CodigoDeEntidadDestino.Equals(other.CodigoDeEntidadDestino)

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, TransaccionDTO))
    End Function

End Class

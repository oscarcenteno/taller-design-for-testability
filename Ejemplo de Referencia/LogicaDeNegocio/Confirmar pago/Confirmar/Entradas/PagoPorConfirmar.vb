Public Class PagoPorConfirmar

    Public Property CodigoDeReferencia As String
    Public Property CodigoDeEntidadDestino As String
    Public Property CodigoDeMonedaDestino As String
    Public Property CodigoDeEntidadOrigen As String
    Public Property NumeroDeTelefonoDestino As String
    Public Property Estado As EstadoPago
    Public Property FechaValor As Date
    Public Property FechaDeConfirmacion As Date
    Public Property SeHaNotificado As Boolean

    Public Sub New(CodigoDeReferencia As String, CodigoDeEntidadDestino As String, _
    CodigoDeMonedaDestino As String, CodigoDeEntidadOrigen As String, _
    NumeroDeTelefonoDestino As String, Estado As EstadoPago, _
    FechaValor As Date, FechaDeConfirmacion As Date, SeHaNotificado As Boolean)

        Me.CodigoDeReferencia = CodigoDeReferencia
        Me.CodigoDeEntidadDestino = CodigoDeEntidadDestino
        Me.CodigoDeMonedaDestino = CodigoDeMonedaDestino
        Me.CodigoDeEntidadOrigen = CodigoDeEntidadOrigen
        Me.NumeroDeTelefonoDestino = NumeroDeTelefonoDestino
        Me.Estado = Estado
        Me.FechaValor = FechaValor
        Me.FechaDeConfirmacion = FechaDeConfirmacion
        Me.SeHaNotificado = SeHaNotificado

    End Sub

End Class

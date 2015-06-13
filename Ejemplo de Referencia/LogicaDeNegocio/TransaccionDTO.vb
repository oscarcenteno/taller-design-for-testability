Public Class TransaccionDTO
    Implements IEquatable(Of TransaccionDTO)

    Property Estado As EstadoTransaccion

    Property SeHaNotificado As Boolean

    Property FechaDeConfirmacion As Date

    Property CodReferencia As String

    Property CodEntidadOrigen As String

    Property NumTelefonoPadronMovilDestino As String

    Property CodMonedaPadronMovilDestino As String

    Property CodEntidadPadronMovilDestino As String

    Property FecValor As Date

    Public Sub New()
        Estado = EstadoTransaccion.Invalido
        SeHaNotificado = False
        FechaDeConfirmacion = New Date
        CodReferencia = String.Empty
        CodEntidadOrigen = String.Empty
        NumTelefonoPadronMovilDestino = String.Empty
        CodMonedaPadronMovilDestino = String.Empty
        CodEntidadPadronMovilDestino = String.Empty
        FecValor = New Date
    End Sub


    Public Overloads Function Equals(other As TransaccionDTO) As Boolean _
        Implements IEquatable(Of TransaccionDTO).Equals
        Return Me.Estado.Equals(other.Estado) And _
        Me.SeHaNotificado.Equals(other.SeHaNotificado) And _
        Me.FechaDeConfirmacion.Equals(other.FechaDeConfirmacion) And _
        Me.CodReferencia.Equals(other.CodReferencia) And _
        Me.CodEntidadOrigen.Equals(other.CodEntidadOrigen) And _
        Me.NumTelefonoPadronMovilDestino.Equals(other.NumTelefonoPadronMovilDestino) And _
        Me.CodMonedaPadronMovilDestino.Equals(other.CodMonedaPadronMovilDestino) And _
        Me.FecValor.Equals(other.FecValor) And _
        Me.CodEntidadPadronMovilDestino.Equals(other.CodEntidadPadronMovilDestino)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, TransaccionDTO))
    End Function

End Class

Imports LogicaDeNegocio

Public Structure InstruccionDeConfirmacion

    Public Property CodigoDeReferencia As String
    Public Property CodigoDeEntidadDestino As String
    Public Property FechaDeConfirmacion As Date
    Public Property Url As String
    Public Property TimeOut As Integer
    Public Property Cn As String

    Public Sub New(CodigoDeReferencia As String, CodigoDeEntidadDestino As String,
                   FechaDeConfirmacion As Date, Url As String, TimeOut As Integer, Cn As String)
        Me.CodigoDeReferencia = CodigoDeReferencia
        Me.CodigoDeEntidadDestino = CodigoDeEntidadDestino
        Me.FechaDeConfirmacion = FechaDeConfirmacion
        Me.Url = Url
        Me.TimeOut = TimeOut
        Me.Cn = Cn
    End Sub

    Public Sub New(dto As PagoPorConfirmar, parametros As ParametrosAlConfirmar, laFechaDeConfirmacion As Date)
        CodigoDeReferencia = dto.CodigoDeReferencia
        CodigoDeEntidadDestino = dto.CodigoDeEntidadDestino
        Url = parametros.Url
        TimeOut = parametros.TimeOut
        Cn = parametros.Cn
        FechaDeConfirmacion = laFechaDeConfirmacion
    End Sub

End Structure

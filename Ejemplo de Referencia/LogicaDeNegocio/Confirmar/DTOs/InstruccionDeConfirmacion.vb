Public Class InstruccionDeConfirmacion
    Implements IEquatable(Of InstruccionDeConfirmacion)

    Public Property CodigoDeReferencia As String
    Public Property CodigoDeEntidadDestino As String
    Public Property FechaDeConfirmacion As Date
    Public Property Url As String
    Public Property TimeOut As Integer
    Public Property Cn As String

    Public Sub New()
        MyBase.New()
    End Sub

    Sub New(dto As TransaccionDTO, parametros As ParametrosAlConfirmarDTO, laFechaDeConfirmacion As Date)
        CodigoDeReferencia = dto.CodigoDeReferencia
        CodigoDeEntidadDestino = dto.CodigoDeEntidadDestino
        Url = parametros.Url
        TimeOut = parametros.TimeOut
        Cn = parametros.Cn
        FechaDeConfirmacion = laFechaDeConfirmacion
    End Sub

    Public Overloads Function Equals(other As InstruccionDeConfirmacion) _
        As Boolean Implements IEquatable(Of InstruccionDeConfirmacion).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqCodReferencia = CodigoDeReferencia.Equals(other.CodigoDeReferencia)
            Dim eqCodEntidadDestino = CodigoDeEntidadDestino.Equals(other.CodigoDeEntidadDestino)
            Dim eqUrl = Url.Equals(other.Url)
            Dim eqTimeOut = TimeOut.Equals(other.TimeOut)
            Dim eqCn = Cn.Equals(other.Cn)
            Dim eqFecha = FechaDeConfirmacion.Equals(other.FechaDeConfirmacion)
            respuesta = eqCodReferencia And eqCodEntidadDestino And _
                eqUrl And eqTimeOut And eqCn And eqFecha
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, InstruccionDeConfirmacion))
    End Function

End Class

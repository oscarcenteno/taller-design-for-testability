Public Class InstruccionDeConfirmacion
    Implements IEquatable(Of InstruccionDeConfirmacion)

    Public Property CodReferencia As String
    Public Property CodEntidadDestino As String
    Public Property Url As String
    Public Property TimeOut As Integer
    Public Property Cn As String
    Public Property FecConfirmacion As Date

    Public Sub New()
        MyBase.New()
    End Sub

    Sub New(dto As TransaccionDTO, parametros As ParametrosAlConfirmar, fechaDeConfirmacion As Date)
        CodReferencia = dto.CodReferencia
        CodEntidadDestino = dto.CodEntidadDestino
        Url = parametros.Url
        TimeOut = parametros.TimeOut
        Cn = parametros.Cn
        FecConfirmacion = fechaDeConfirmacion
    End Sub

    Public Overloads Function Equals(other As InstruccionDeConfirmacion) _
        As Boolean Implements IEquatable(Of InstruccionDeConfirmacion).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqCodReferencia = CodReferencia.Equals(other.CodReferencia)
            Dim eqCodEntidadDestino = CodEntidadDestino.Equals(other.CodEntidadDestino)
            Dim eqUrl = Url.Equals(other.Url)
            Dim eqTimeOut = TimeOut.Equals(other.TimeOut)
            Dim eqCn = Cn.Equals(other.Cn)
            Dim eqFecha = FecConfirmacion.Equals(other.FecConfirmacion)
            respuesta = eqCodReferencia And eqCodEntidadDestino And _
                eqUrl And eqTimeOut And eqCn And eqFecha
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, InstruccionDeConfirmacion))
    End Function

End Class

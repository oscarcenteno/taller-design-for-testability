Public Class InstruccionDeConfirmacion
    Implements IEquatable(Of InstruccionDeConfirmacion)

    Public Property CodReferencia As String
    Public Property ParametrosDeComunicacion As ParametrosAlConfirmar
    Public Property Fecha As Date

    Sub New(dto As TransaccionDTO, parametros As ParametrosAlConfirmar, fechaDeProceso As Date)
        CodReferencia = dto.CodReferencia
        ParametrosDeComunicacion = parametros
        Fecha = fechaDeProceso
    End Sub

    Public Overloads Function Equals(other As InstruccionDeConfirmacion) _
        As Boolean Implements IEquatable(Of InstruccionDeConfirmacion).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqCodReferencia = CodReferencia.Equals(other.CodReferencia)
            Dim eqFecha = Fecha.Equals(other.Fecha)
            Dim eqParametrosDeComunicacion = ParametrosDeComunicacion.Equals(other.ParametrosDeComunicacion)
            respuesta = eqCodReferencia And eqFecha And eqParametrosDeComunicacion
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, InstruccionDeConfirmacion))
    End Function

End Class

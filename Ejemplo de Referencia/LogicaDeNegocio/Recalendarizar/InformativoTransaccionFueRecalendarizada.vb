Public Class InformativoTransaccionFueRecalendarizada
    Implements IEquatable(Of InformativoTransaccionFueRecalendarizada)

    Dim _numeroDeEsteIntento As String
    Dim _codReferencia As String
    Dim _fecCalendarizacion As String

    Public Sub New(miTransaccion As TransaccionDTO, numeroDeEsteIntento As Integer, fecCalendarizacion As Date)
        _codReferencia = miTransaccion.CodReferencia
        _numeroDeEsteIntento = numeroDeEsteIntento
        _fecCalendarizacion = fecCalendarizacion
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Se ha calendarizado el reintento para Confirmar #{0} para la transacción con referencia {1} en la fecha/hora {2}.", _
                             _numeroDeEsteIntento,
                             _codReferencia,
                             _fecCalendarizacion)
    End Function

    Public Overloads Function Equals(other As InformativoTransaccionFueRecalendarizada) As Boolean Implements IEquatable(Of InformativoTransaccionFueRecalendarizada).Equals
        Return Me.ToString.Equals(other.ToString)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        Dim respuesta As Boolean = False

        If obj IsNot Nothing Then
            respuesta = Me.Equals(CType(obj, InformativoTransaccionFueRecalendarizada))
        End If

        Return respuesta
    End Function

End Class

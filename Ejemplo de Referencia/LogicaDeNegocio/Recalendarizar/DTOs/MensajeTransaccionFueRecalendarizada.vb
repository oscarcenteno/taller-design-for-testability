Public Class MensajeTransaccionFueRecalendarizada
    Implements IEquatable(Of MensajeTransaccionFueRecalendarizada)

    Private _numeroDeEsteIntento As String
    Private _codReferencia As String
    Private _fecCalendarizacion As String

    Sub New(codigoDeReferencia As String, numeroDeEsteIntento As Integer, fecCalendarizacion As Date)
        _codReferencia = codigoDeReferencia
        _numeroDeEsteIntento = numeroDeEsteIntento
        _fecCalendarizacion = fecCalendarizacion
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Se ha calendarizado el reintento para Confirmar #{0} para la transacción con referencia {1} en la fecha/hora {2}.", _
                             _numeroDeEsteIntento,
                             _codReferencia,
                             _fecCalendarizacion)
    End Function

    Public Overloads Function Equals(other As MensajeTransaccionFueRecalendarizada) As Boolean Implements IEquatable(Of MensajeTransaccionFueRecalendarizada).Equals
        Return Me.ToString.Equals(other.ToString)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        Dim respuesta As Boolean = False

        If obj IsNot Nothing Then
            respuesta = Me.Equals(CType(obj, MensajeTransaccionFueRecalendarizada))
        End If

        Return respuesta
    End Function

End Class

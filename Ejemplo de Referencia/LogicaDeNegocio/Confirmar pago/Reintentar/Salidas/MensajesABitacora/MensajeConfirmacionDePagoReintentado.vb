Public Structure MensajeConfirmacionDePagoReintentado

    Public CodigoDeReferencia As String
    Public NumeroDeEsteIntento As String
    Public FechaDeCalendarizacion As String

    Sub New(instruccion As InstruccionParaReintentar)
        CodigoDeReferencia = instruccion.CodigoDeReferencia
        NumeroDeEsteIntento = instruccion.NumeroDeReintento
        FechaDeCalendarizacion = instruccion.FechaDeInicio
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Se ha calendarizado el reintento para confirmar #{0} para el pago con referencia {1} en la fecha/hora {2}.", _
                             NumeroDeEsteIntento,
                             CodigoDeReferencia,
                             FechaDeCalendarizacion)
    End Function

End Structure

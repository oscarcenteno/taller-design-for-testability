Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class Calendarizador
    Implements ICalendarizadorAlConfirmar

    Public Sub ReCalendarizarConfirmacion(instruccionParaRecalendarizar As InstruccionParaReintentar) Implements ICalendarizadorAlConfirmar.Reintentar
        Console.WriteLine("ReCalendarizarConfirmacion: ")
        Console.WriteLine("| CodigoDeReferencia {0}", instruccionParaRecalendarizar.CodigoDeReferencia)
        Console.WriteLine("| FechaDeInicio: {0}", instruccionParaRecalendarizar.FechaDeInicio)
        Console.WriteLine("| NumeroDeReintento: {0}" & instruccionParaRecalendarizar.NumeroDeReintento)
        Console.WriteLine()
    End Sub

End Class

Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class ServiciosAlConfirmar
    Implements IServiciosAlConfirmar

    Public Sub EscribirErrorAlConfirmar(mensaje As ErrorPagoDebeEstarAutorizadoYNoNotificado) Implements IServiciosAlConfirmar.EscribirErrorAlConfirmar
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub EscribirPagoFueConfirmado(mensaje As MensajePagoFueConfirmado) Implements IServiciosAlConfirmar.EscribirPagoFueConfirmado
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)

    End Sub

    Public Sub EscribirConfirmacionDePagoReintentado(mensaje As MensajeConfirmacionDePagoReintentado) Implements IServiciosAlConfirmar.EscribirConfirmacionDePagoReintentado
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub EscribirErrorNoSePuedeInvocarAEntidad(mensaje As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos) Implements IServiciosAlConfirmar.EscribirErrorNoSePuedeInvocarAEntidad
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub ReCalendarizarConfirmacion(instruccionParaRecalendarizar As InstruccionParaReintentar) Implements ICalendarizadorAlConfirmar.Reintentar
        Console.WriteLine("ReCalendarizarConfirmacion: ")
        Console.WriteLine("| CodigoDeReferencia {0}", instruccionParaRecalendarizar.CodigoDeReferencia)
        Console.WriteLine("| FechaDeInicio: {0}", instruccionParaRecalendarizar.FechaDeInicio)
        Console.WriteLine("| NumeroDeReintento: {0}" & instruccionParaRecalendarizar.NumeroDeReintento)
        Console.WriteLine()
    End Sub

End Class

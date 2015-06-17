Imports LogicaDeAplicacion

Public Class BitacoraAlConfirmar
    Implements IBitacoraAlConfirmar

    Public Sub EscribirErrorAlConfirmar(mensaje As LogicaDeNegocio.ErrorTransaccionDebeEstarAutorizadaYNoNotificada) Implements IBitacoraAlConfirmar.EscribirErrorAlConfirmar
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub EscribirTransaccionConfirmada(mensaje As LogicaDeNegocio.MensajeTransaccionFueConfirmada) Implements IBitacoraAlConfirmar.EscribirTransaccionConfirmada
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)

    End Sub

    Public Sub EscribirTransaccionFueRecalendarizada(mensaje As LogicaDeNegocio.MensajeTransaccionFueRecalendarizada) Implements IBitacoraAlConfirmar.EscribirTransaccionFueRecalendarizada
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub EscribirErrorNoSePuedeInvocarAEntidad(mensaje As LogicaDeNegocio.ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos) Implements IBitacoraAlConfirmar.EscribirErrorNoSePuedeInvocarAEntidad
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub
End Class

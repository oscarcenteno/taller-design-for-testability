Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class BitacoraAlConfirmar
    Implements IBitacoraAlConfirmar

    Public Sub EscribirErrorAlConfirmar(mensaje As ErrorPagoDebeEstarAutorizadoYNoNotificado) Implements IBitacoraAlConfirmar.EscribirErrorAlConfirmar
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub EscribirPagoFueConfirmado(mensaje As MensajePagoFueConfirmado) Implements IBitacoraAlConfirmar.EscribirPagoFueConfirmado
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)

    End Sub

    Public Sub EscribirConfirmacionDePagoReintentado(mensaje As MensajeConfirmacionDePagoReintentado) Implements IBitacoraAlConfirmar.EscribirConfirmacionDePagoReintentado
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub

    Public Sub EscribirErrorNoSePuedeInvocarAEntidad(mensaje As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos) Implements IBitacoraAlConfirmar.EscribirErrorNoSePuedeInvocarAEntidad
        Console.WriteLine("Bitácora: {0}", mensaje.ToString)
    End Sub
End Class

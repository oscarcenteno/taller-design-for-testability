Imports LogicaDeNegocio

Public Interface IBitacoraAlConfirmar
    Sub EscribirTransaccionConfirmada(mensaje As MensajeTransaccionFueConfirmada)
    Sub EscribirErrorAlConfirmar(mensaje As ErrorTransaccionDebeEstarAutorizadaYNoNotificada)
    Sub EscribirTransaccionFueRecalendarizada(mensaje As MensajeTransaccionFueRecalendarizada)
    Sub EscribirErrorNoSePuedeInvocarAEntidad(mensaje As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos)

End Interface

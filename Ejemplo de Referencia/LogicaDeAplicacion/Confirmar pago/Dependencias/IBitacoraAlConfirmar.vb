Imports LogicaDeNegocio

Public Interface IBitacoraAlConfirmar
    Sub EscribirPagoFueConfirmado(mensaje As MensajePagoFueConfirmado)
    Sub EscribirErrorAlConfirmar(mensaje As ErrorPagoDebeEstarAutorizadoYNoNotificado)
    Sub EscribirConfirmacionDePagoReintentado(mensaje As MensajeConfirmacionDePagoReintentado)
    Sub EscribirErrorNoSePuedeInvocarAEntidad(mensaje As ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos)

End Interface

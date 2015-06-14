Imports LogicaDeNegocio

Public Interface IBitacoraAlConfirmar

    Sub EscribirTransaccionConfirmada(mensaje As InformativoTransaccionFueConfirmada)
    Sub EscribirErrores(errores As List(Of Object))
    Sub EscribirTransaccionFueRecalendarizada(mensaje As InformativoTransaccionFueRecalendarizada)

End Interface

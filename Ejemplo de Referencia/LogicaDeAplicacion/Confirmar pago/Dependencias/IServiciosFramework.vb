Imports LogicaDeNegocio

Public Interface IServiciosFramework
    Sub EscribirMensajeABitacora(mensaje As String)
    Sub EscribirErroresABitacora(errores As IEnumerable(Of ErrorDeValidacion))

End Interface

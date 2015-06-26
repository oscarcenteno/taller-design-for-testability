Imports LogicaDeAplicacion
Imports LogicaDeNegocio

Public Class Bitacora
    Implements IBitacora

    Public Sub EscribirMensajeInformativo(mensaje As String) Implements IBitacora.EscribirMensajeInformativo
        Throw New NotImplementedException
    End Sub

    Public Sub EscribirError(mensaje As String) Implements IBitacora.EscribirError
        Throw New NotImplementedException
    End Sub
End Class

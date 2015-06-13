Public Class TransaccionBE

#Region "Confirmacion"

    Private _estadoInterno As TransaccionDTO
    Private Const _maximoDeReintentosPermitidos As Integer = 3

    Function ObtenerDatosDeTransaccion() As LogicaDeNegocio.TransaccionDTO
        Return _estadoInterno
    End Function

    Public Sub New(estadoInterno As TransaccionDTO)
        _estadoInterno = estadoInterno
    End Sub

    Public ReadOnly Property FechaDeConfirmacion As Date
        Get
            Return _estadoInterno.FechaDeConfirmacion
        End Get
    End Property


    Public Function SePuedeConfirmar() As Boolean
        Dim respuesta As Boolean = False
        If SeHaAutorizado() And Not SeHaNotificado() Then
            respuesta = True
        End If
        Return respuesta
    End Function

    Public Function ErroresAlConfirmar() As IEnumerable(Of Object)
        Dim respuesta As New List(Of Object)

        If Not SePuedeConfirmar() Then
            If Not SeHaAutorizado() Then
                respuesta.Add(New ErrorTransaccionNoEstaAutorizada(Me._estadoInterno))
            End If
            If SeHaNotificado() Then
                respuesta.Add(New ErrorTransaccionYaFueNotificada(Me._estadoInterno))
            End If
        End If

        Return respuesta
    End Function

    Private Function SeHaAutorizado() As Boolean
        Return _estadoInterno.Estado = EstadoTransaccion.Autorizada
    End Function

    Function SeHaNotificado() As Boolean
        Return _estadoInterno.SeHaNotificado
    End Function

    Sub Confirmar(fechaDeConfirmacion As Date)
        _estadoInterno.FechaDeConfirmacion = fechaDeConfirmacion
        _estadoInterno.SeHaNotificado = True
    End Sub

#End Region

    

#Region "Reglas de Reintentos de Confirmacion"

    Function SePuedeReintentarLaConfirmacion(intentosYaRealizados As Integer) As Boolean
        Dim resultado = False
        If SePuedeConfirmar() And NoHaSuperadoLosIntentosMaximos(intentosYaRealizados) Then
            resultado = True
        End If
        Return resultado
    End Function


    Private Function NoHaSuperadoLosIntentosMaximos(intentosYaRealizados As Integer) As Boolean
        Return intentosYaRealizados < _maximoDeReintentosPermitidos
    End Function

#End Region

End Class

Public Class ParametrosAlConfirmar
    Implements IEquatable(Of ParametrosAlConfirmar)

    Public Property Url As String
    Public Property TimeOut As Integer
    Public Property Cn As String
    Public Property CodEntidad As String
    Public Property CodReferencia As String

    Sub New(miTransaccion As TransaccionDTO, theUrl As String, theTimeOut As Integer, theCn As String)
        Url = theUrl
        TimeOut = theTimeOut
        Cn = theCn
        CodEntidad = miTransaccion.CodEntidadPadronMovilDestino
        CodReferencia = miTransaccion.CodReferencia
    End Sub

    Public Function SePuedeInvocarAEntidad() As Boolean
        Dim respuesta As Boolean = False

        respuesta = Not String.IsNullOrEmpty(Url)
        respuesta = respuesta And Not String.IsNullOrEmpty(TimeOut)
        respuesta = respuesta And Not String.IsNullOrEmpty(Cn)

        Return respuesta
    End Function

    Public Function ErroresDeParametros() As IList(Of ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos)
        Dim respuesta As New List(Of ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos)
        If Not SePuedeInvocarAEntidad() Then
            respuesta.Add(New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(CodReferencia, CodEntidad))
        End If
        Return respuesta
    End Function

    Public Overloads Function Equals(other As ParametrosAlConfirmar) As Boolean Implements IEquatable(Of ParametrosAlConfirmar).Equals
        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqUrl = Url.Equals(other.Url)
            Dim eqTimeOut = TimeOut.Equals(other.TimeOut)
            Dim eqCn = Cn.Equals(other.Cn)
            respuesta = eqUrl And eqTimeOut And eqCn
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, ParametrosAlConfirmar))
    End Function

End Class

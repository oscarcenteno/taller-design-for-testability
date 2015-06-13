
Public Class InstruccionParaRecalendarizar
    Implements IEquatable(Of InstruccionParaRecalendarizar)

    Property CodReferencia As String
    Property FechaInicio As Date
    Property Intentos As Integer

    Public Sub New()
        CodReferencia = String.Empty
        FechaInicio = New Date
        Intentos = 0
    End Sub

    Public Overloads Function Equals(other As InstruccionParaRecalendarizar) _
    As Boolean Implements IEquatable(Of InstruccionParaRecalendarizar).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqFechaInicio = FechaInicio.Equals(other.FechaInicio)
            Dim eqIntentos = Intentos.Equals(other.Intentos)
            Dim eqCodReferencia = CodReferencia.Equals(other.CodReferencia)
            respuesta = eqFechaInicio And eqIntentos And eqCodReferencia
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, InstruccionParaRecalendarizar))
    End Function


End Class

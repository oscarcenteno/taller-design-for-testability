Public Class PagoConfirmado
    Implements IEquatable(Of PagoConfirmado)

    Public Property CodigoDeReferencia As String
    Public Property FechaDeConfirmacion As Date
    Public Property SeHaNotificado As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(CodigoDeReferencia As String, FechaDeConfirmacion As Date)

        Me.CodigoDeReferencia = CodigoDeReferencia
        Me.FechaDeConfirmacion = FechaDeConfirmacion
        Me.SeHaNotificado = True

    End Sub

    Public Overloads Function Equals(other As PagoConfirmado) _
        As Boolean Implements IEquatable(Of PagoConfirmado).Equals

        Dim respuesta As Boolean = False

        If other IsNot Nothing Then
            Dim eqCodReferencia = CodigoDeReferencia.Equals(other.CodigoDeReferencia)
            Dim eqFechaDeConfirmacion = FechaDeConfirmacion.Equals(other.FechaDeConfirmacion)
            Dim eqSeHaNotificado = SeHaNotificado.Equals(other.SeHaNotificado)
            respuesta = eqCodReferencia And eqFechaDeConfirmacion And eqSeHaNotificado
        End If

        Return respuesta
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, PagoConfirmado))
    End Function


End Class

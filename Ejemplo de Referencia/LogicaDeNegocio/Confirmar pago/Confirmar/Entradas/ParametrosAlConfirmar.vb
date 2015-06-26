Public Class ParametrosAlConfirmar

    Public Property Url As String
    Public Property TimeOut As Integer
    Public Property Cn As String

    Public Sub New(Url As String, TimeOut As Integer, Cn As String)
        Me.Url = Url
        Me.TimeOut = TimeOut
        Me.Cn = Cn
    End Sub

End Class

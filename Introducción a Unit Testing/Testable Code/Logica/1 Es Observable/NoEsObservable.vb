Imports System.IO

Public Class NoEsObservable

    ' No es observable
    ' Es dificil observarlo porque se debe trabajar mas para obtener el valor de la Suma.
    Public Sub Sume(x As Integer, y As Integer)
        Dim resultado As String = String.Format("La respuesta es {0}", x + y)
        File.WriteAllText("results.txt", resultado)
    End Sub

End Class

Imports System
Imports TechTalk.SpecFlow
Imports LogicaDeNegocio

<Binding()> _
Public Class ConfirmarUnPagoSteps
    Private _miPago As PagoBE

#Region "Escenario Se puede confirmar"
    Private _sePuedeConfirmarObtenido As Booleano = Booleano.No

    <TechTalk.SpecFlow.Given("un pago en estado ""(.*)"" y ""(.*)""")> _
    Public Sub DadoUnPagoEnEstadoY(ByVal estado As EstadoPago, seHaNotificado As Booleano)
        _miPago = New PagoBE(New PagoPorConfirmar With {.Estado = estado, .SeHaNotificado = seHaNotificado})
    End Sub

    <TechTalk.SpecFlow.When("se solicita confirmar")> _
    Public Sub CuandoSeSolicitaConfirmar()
        _sePuedeConfirmarObtenido = _miPago.SePuedeConfirmar()
    End Sub

    <TechTalk.SpecFlow.Then("se confirma ""(.*)""")> _
    Public Sub EntoncesSeConfirma(ByVal resultadoEsperado As Booleano)
        Assert.AreEqual(resultadoEsperado, _sePuedeConfirmarObtenido)
    End Sub

#End Region

End Class


Imports System
Imports TechTalk.SpecFlow
Imports LogicaDeNegocio

Namespace Especificaciones

    <Binding()> _
    Public Class ConfirmarUnPagoSteps
        Private _miTransaccion As TransaccionBE

#Region "Escenario Se puede confirmar"
        Private _sePuedeConfirmarObtenido As Booleano = Booleano.No

        <TechTalk.SpecFlow.Given("una transaccion en estado ""(.*)"" y ""(.*)""")> _
        Public Sub DadoUnaTransaccionEnEstadoY(ByVal estado As EstadoTransaccion, seHaNotificado As Booleano)
            _miTransaccion = New TransaccionBE(New TransaccionDTO With {.Estado = estado, .SeHaNotificado = seHaNotificado})
        End Sub

        <TechTalk.SpecFlow.When("se solicita confirmar")> _
        Public Sub CuandoSeSolicitaConfirmar()
            _sePuedeConfirmarObtenido = _miTransaccion.SePuedeConfirmar()
        End Sub

        <TechTalk.SpecFlow.Then("se confirma ""(.*)""")> _
        Public Sub EntoncesSeConfirma(ByVal resultadoEsperado As Booleano)
            Assert.AreEqual(resultadoEsperado, _sePuedeConfirmarObtenido)
        End Sub

#End Region

#Region "Escenario al Confirmar"

        Private _fechaEsperada As Date = Nothing

        <TechTalk.SpecFlow.Given("una transaccion que no ha sido notificada")> _
        Public Sub DadoUnaTransaccionQueNoHaSidoNotificada()
            _miTransaccion = New TransaccionBE(New TransaccionDTO With {.Estado = EstadoTransaccion.Autorizada, .SeHaNotificado = False})
        End Sub

        <TechTalk.SpecFlow.Given("la fecha es ""(.*)""")> _
        Public Sub DadoLaFechaEs(ByVal fechaActual As Date)
            _fechaEsperada = fechaActual
        End Sub

        <TechTalk.SpecFlow.When("se confirma")> _
        Public Sub CuandoSeConfirma()
            _miTransaccion.Confirmar(_fechaEsperada)
        End Sub

        <TechTalk.SpecFlow.Then("la transaccion se ha notificado")> _
        Public Sub EntoncesLaTransaccionSeHaNotificado()
            Assert.IsTrue(_miTransaccion.SeHaNotificado)
        End Sub

        <TechTalk.SpecFlow.Then("registra la fecha de la confirmación")> _
        Public Sub EntoncesRegistraLaFechaDeLaConfirmacion()
            Assert.AreEqual(_fechaEsperada, _miTransaccion.FechaDeConfirmacion)
        End Sub

#End Region

    End Class

End Namespace

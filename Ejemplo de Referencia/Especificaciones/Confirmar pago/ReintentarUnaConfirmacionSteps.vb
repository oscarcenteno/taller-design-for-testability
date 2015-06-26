Imports TechTalk.SpecFlow
Imports LogicaDeNegocio

<Binding()> _
Public Class ReintentarUnaConfirmacionSteps

    Private _losParametros As ParametrosAlReintentar
    Private _logicaReintentos As ReintentosBE = Nothing
    Private _solicitud As SolicitudAlConfirmar

    <TechTalk.SpecFlow.Given("un pago que ha sido intentado (.*) veces")> _
    Public Sub DadoUnPagoQueHaSidoIntentadoVeces(ByVal intentosRealizados As Int32)
        _solicitud = New SolicitudAlConfirmar("Cod1", intentosRealizados, Date.Now)
    End Sub

    <TechTalk.SpecFlow.Given("se definio un maximo de (.*) reintentos por pago")> _
    Public Sub DadoSeDefinioUnMaximoDeReintentosPorPago(ByVal maximoDeReintentos As Int32)
        ' TODO: Mejorar esta linea
        _losParametros = New ParametrosAlReintentar(maximoDeReintentos, String.Empty)
        _logicaReintentos = New ReintentosBE(_losParametros, _solicitud)
    End Sub

    <TechTalk.SpecFlow.When("no se pudo contactar a la entidad destino")> _
    Public Sub CuandoNoSePudoContactarALaEntidadDestino()
    End Sub

    <TechTalk.SpecFlow.Then("se puede ""(.*)""")> _
    Public Sub EntoncesSePuedeReintentar(ByVal resultadoEsperado As Booleano)
        Dim convertido As Boolean = resultadoEsperado
        Assert.AreEqual(convertido, _logicaReintentos.SePuedeReintentar())
    End Sub

End Class


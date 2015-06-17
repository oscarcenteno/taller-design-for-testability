Imports TechTalk.SpecFlow
Imports LogicaDeNegocio

Namespace Especificaciones

    <Binding()> _
    Public Class ReintentarUnaConfirmacionSteps

        Private _unaTransaccion As New TransaccionDTO
        Private _losParametros As New ParametrosAlRecalendarizarDTO
        Private _logicaReintentos As ReintentosBE = Nothing
        Private _intentosYaRealizados As Integer = 0

        <TechTalk.SpecFlow.Given("una transaccion que ha sido intentada (.*) veces")> _
        Public Sub DadoUnaTransaccionQueHaSidoIntentadaVeces(ByVal intentosRealizados As Int32)
            _intentosYaRealizados = intentosRealizados
        End Sub

        <TechTalk.SpecFlow.Given("se definio un maximo de (.*) reintentos por transaccion")> _
        Public Sub DadoSeDefinioUnMaximoDeReintentosPorTransaccion(ByVal maximoDeReintentos As Int32)
            _losParametros.CantidadMaximaDeIntentos = maximoDeReintentos
            _logicaReintentos = New ReintentosBE(_losParametros, _unaTransaccion, _intentosYaRealizados, Nothing)
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

End Namespace

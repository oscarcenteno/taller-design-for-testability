Imports System
Imports TechTalk.SpecFlow
Imports TechTalk.SpecFlow.Assist

Namespace LogicaDeNegocio.UnitTests

    <Binding()> _
    Public Class ComparacionDeTransaccionDTOSteps

        Dim _laTransaccion As TransaccionDTO
        Dim _laOtraTransaccion As TransaccionDTO
        Dim obtenido As Boolean

        <TechTalk.SpecFlow.Given("una transaccion base")> _
        Public Sub DadoUnaTransaccion(ByVal table As Table)
            _laTransaccion = table.CreateInstance(Of TransaccionDTO)()
        End Sub
        <TechTalk.SpecFlow.Given("otra con ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")> _
        Public Sub DadoOtraCon(ByVal CodigoDeReferencia As String, ByVal CodigoDeEntidadDestino As Int32, ByVal CodigoDeMonedaDestino As Int32, ByVal CodigoDeEntidadOrigen As Int32, ByVal NumeroDeTelefonoDestino As Int32, ByVal Estado As EstadoTransaccion, ByVal FechaValor As Date, ByVal FechaDeConfirmacion As Date, ByVal SeHaNotificado As Booleano)
            _laOtraTransaccion = New TransaccionDTO With {.CodigoDeReferencia = CodigoDeReferencia, .CodigoDeEntidadDestino = CodigoDeEntidadDestino, .CodigoDeMonedaDestino = CodigoDeMonedaDestino, .CodigoDeEntidadOrigen = CodigoDeEntidadOrigen, .NumeroDeTelefonoDestino = NumeroDeTelefonoDestino, .Estado = Estado, .FechaValor = FechaValor, .FechaDeConfirmacion = FechaDeConfirmacion, .SeHaNotificado = CType(SeHaNotificado, Boolean)}
        End Sub
        
        <TechTalk.SpecFlow.When("se compara")> _
        Public Sub CuandoSeCompara()
            obtenido = TransaccionDTO.Equals(_laTransaccion, _laOtraTransaccion)
        End Sub
        
        <TechTalk.SpecFlow.Then("""(.*)""")> _
        Public Sub Entonces(ByVal SonIguales As Booleano)
            Assert.AreEqual(CType(SonIguales, Boolean), obtenido)
        End Sub

    End Class

End Namespace

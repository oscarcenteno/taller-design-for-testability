Imports TechTalk.SpecFlow
Imports TechTalk.SpecFlow.Assist
Imports LogicaDeNegocio
Imports FakeItEasy

Namespace LogicaDeAplicacion.UnitTests

    <Binding()> _
    Public Class SePuedeConfirmarUnaTransaccionAutorizadaSteps

        'inputs
        Dim dto As TransaccionDTO
        Dim _fechaActual As Date
        Dim _parametros As ParametrosAlConfirmar

        ' dependencias
        Dim _repo As IRepositorioAlConfirmar
        Dim _bita As IBitacoraAlConfirmar
        Dim _cale As ICalendarizadorAlConfirmar
        Dim _invo As IInvocadorAlConfirmar
        Dim _codReferencia As String
        Dim _dtoConfirmada As TransaccionDTO

        <TechTalk.SpecFlow.Given("esta transaccion")> _
        Public Sub DadoEstaTransaccion(ByVal table As Table)
            dto = table.CreateInstance(Of TransaccionDTO)()
        End Sub

        <TechTalk.SpecFlow.Given("estos parametros de confirmacion")> _
        Public Sub DadoEstosParametrosDeConfirmacion(ByVal table As Table)
            _parametros = table.CreateInstance(Of ParametrosAlConfirmar)()
        End Sub

        <TechTalk.SpecFlow.Given("la fecha actual es ""(.*)""")> _
        Public Sub DadoLaFechaActualEs(ByVal fechaActual As Date)
            _fechaActual = fechaActual
        End Sub

        <TechTalk.SpecFlow.When("la transaccion ""(.*)"" se envia a confirmar exitosamente")> _
        Public Sub CuandoLaTransaccionSeEnviaAConfirmarExitosamente(ByVal codReferencia As String)

            _repo = A.Fake(Of IRepositorioAlConfirmar)()
            _bita = A.Fake(Of IBitacoraAlConfirmar)()
            _invo = A.Fake(Of IInvocadorAlConfirmar)()
            ' comportamiento de fakes
            A.CallTo(Function() _repo.ObtenerTransaccion(codReferencia)).Returns(dto)
            A.CallTo(Function() _repo.ObtenerParametrosParaConfirmar(dto)).Returns(_parametros)

            _codReferencia = codReferencia
        End Sub

        <TechTalk.SpecFlow.Then("se genera esta confirmacion a la entidad destino")> _
        Public Sub EntoncesSeGeneraEstaConfirmacionALaEntidadDestino(ByVal table As Table)
            Dim _instrConfirmacion = table.CreateInstance(Of InstruccionDeConfirmacion)()

            Dim exito = New RespuestaAlInvocarEntidad() With {.ComunicacionFueFallida = False}
            A.CallTo(Function() _invo.Confirmar(_instrConfirmacion)).Returns(exito)
        End Sub

        <TechTalk.SpecFlow.Then("se actualiza la informacion de la transaccion")> _
        Public Sub EntoncesSeActualizaLaInformacionDeLaTransaccion(ByVal table As Table)
            _dtoConfirmada = table.CreateInstance(Of TransaccionDTO)()
        End Sub

        <TechTalk.SpecFlow.Then("se registra este mensaje en la bitacora ""(.*)""")> _
        Public Sub EntoncesSeRegistraEsteMensajeEnLaBitacora(ByVal mensaje As String)

            ' sut
            Dim app As New App(_bita, _repo, _invo, Nothing)
            app.Confirmar(_codReferencia, 0, _fechaActual)

            ' verificar resultados
            A.CallTo(Function() _repo.ObtenerTransaccion(_codReferencia)).MustHaveHappened()
            A.CallTo(Function() _repo.ObtenerParametrosParaConfirmar(dto)).MustHaveHappened()
            A.CallTo(Sub() _repo.ActualizarTransaccionConfirmada(_dtoConfirmada)).MustHaveHappened()
            Dim mensajeEsperado As New InformativoTransaccionFueConfirmada(dto)
            A.CallTo(Sub() _bita.EscribirTransaccionConfirmada(mensajeEsperado)).MustHaveHappened()
            mensajeEsperado.ToString.Equals(mensaje)
        End Sub

    End Class

End Namespace

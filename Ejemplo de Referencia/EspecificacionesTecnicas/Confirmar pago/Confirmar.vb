Imports TechTalk.SpecFlow
Imports TechTalk.SpecFlow.Assist
Imports LogicaDeNegocio
Imports FakeItEasy

Namespace LogicaDeAplicacion.UnitTests

    <Binding()> _
    Public Class CasoBasicoAlConfirmarUnPagoSteps

        'datos existentes
        Friend _laTransaccion As TransaccionDTO
        Friend _fechaActual As Date
        Friend _parametrosDeEntidad As ParametrosAlConfirmarDTO

        ' dependencias
        Friend _repo As IRepositorioAlConfirmar = A.Fake(Of IRepositorioAlConfirmar)()
        Friend _bita As IBitacoraAlConfirmar = A.Fake(Of IBitacoraAlConfirmar)()
        Friend _cale As ICalendarizadorAlConfirmar = A.Fake(Of ICalendarizadorAlConfirmar)()
        Friend _invo As IInvocadorAlConfirmar = A.Fake(Of IInvocadorAlConfirmar)()

        ' datos del escenario
        Friend _codigoDeReferencia As String
        Friend _transaccionConfirmada As TransaccionDTO

        <TechTalk.SpecFlow.Given("una transaccion")> _
        Public Sub DadoUnaTransaccion(ByVal laTransaccion As TransaccionDTO)
            _laTransaccion = laTransaccion
        End Sub

        <TechTalk.SpecFlow.Given("estos parametros de confirmacion de la entidad 501")> _
        Public Sub DadoEstosParametrosDeConfirmacion(ByVal parametrosDeEntidad As ParametrosAlConfirmarDTO)
            _parametrosDeEntidad = parametrosDeEntidad
        End Sub

        <TechTalk.SpecFlow.Given("la fecha actual es ""(.*)""")> _
        Public Sub DadoLaFechaActualEs(ByVal fechaActual As Date)
            _fechaActual = fechaActual
        End Sub

#Region "Basico"

        Dim _instruccionDeConfirmacion As InstruccionDeConfirmacion
        Dim _respuestaNoFueExitosa As New RespuestaAlInvocarEntidad() With {.ComunicacionFueExitosa = False}
        Dim _respuestaFueExitosa As New RespuestaAlInvocarEntidad() With {.ComunicacionFueExitosa = True}


        <TechTalk.SpecFlow.When("la transaccion ""(.*)"" se envia a confirmar")> _
        <TechTalk.SpecFlow.When("la transaccion ""(.*)"" se envia a confirmar exitosamente")> _
        Public Sub CuandoLaTransaccionSeEnviaAConfirmar(ByVal codigoDeReferencia As String)
            _codigoDeReferencia = codigoDeReferencia
        End Sub


        <TechTalk.SpecFlow.Then("se generara esta confirmacion a la entidad destino")> _
        Public Sub EntoncesSeGeneraEstaConfirmacionALaEntidadDestino(ByVal instruccion As InstruccionDeConfirmacion)
            _instruccionDeConfirmacion = instruccion
        End Sub

        <TechTalk.SpecFlow.Then("se actualizara la informacion de la transaccion")> _
        Public Sub EntoncesSeActualizaLaInformacionDeLaTransaccion(ByVal transaccionConfirmada As TransaccionDTO)
            _transaccionConfirmada = transaccionConfirmada
        End Sub

        <TechTalk.SpecFlow.Then("se escribirá en bitacora que la transaccion fue confirmada")> _
        Public Sub SeEscribiraEnBitacoraQueLaTransaccionFueConfirmada()
            ' HACK La invocacion al SUT se realiza en el ultimo paso del escenario ya que es ahi donde se ha establecido todos los datos requeridos
            ' Esto es solamente en los casos en donde se utiliza Fakes.
            A.CallTo(Function() _repo.ObtenerTransaccion(_transaccionConfirmada.CodigoDeReferencia)).Returns(_laTransaccion)
            A.CallTo(Function() _repo.ObtenerParametros(_laTransaccion)).Returns(_parametrosDeEntidad)
            A.CallTo(Function() _invo.Confirmar(_instruccionDeConfirmacion)).Returns(_respuestaFueExitosa)

            ' sut
            Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, Nothing)
            app.Confirmar(_codigoDeReferencia, 0, _fechaActual)

            ' verificaciones
            A.CallTo(Sub() _repo.ActualizarTransaccionConfirmada(_transaccionConfirmada)).MustHaveHappened()
            Dim mensajeEsperado As New MensajeTransaccionFueConfirmada(_laTransaccion)
            A.CallTo(Sub() _bita.EscribirTransaccionConfirmada(mensajeEsperado)).MustHaveHappened()
        End Sub

#End Region

#Region "Solo se confirmara transacciones autorizadas"

        <TechTalk.SpecFlow.Then("se registra en bitacora que solo se confirma transacciones autorizadas y no notificadas")> _
        Public Sub EntoncesSeRegistraEnBitacoraQueSoloSeConfirmaTransaccionesAutorizadasYNoNotificadas()
            A.CallTo(Function() _repo.ObtenerTransaccion(_laTransaccion.CodigoDeReferencia)).Returns(_laTransaccion)
            ' sut
            Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, Nothing)
            app.Confirmar(_codigoDeReferencia, 0, _fechaActual)

            ' verificar resultados
            Dim mensajeEsperado As New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(_laTransaccion)
            A.CallTo(Sub() _bita.EscribirErrorAlConfirmar(mensajeEsperado)).MustHaveHappened()
        End Sub

#End Region

#Region "Se reintentará si no hay comunicación con la entidad destino"

        Private _parametrosAlRecalendarizar As ParametrosAlRecalendarizarDTO
        Private _respuestaEntidad As RespuestaAlInvocarEntidad
        Private _reintentosRealizados As Integer
        Private _laInstruccionParaRecalendarizar As InstruccionParaRecalendarizar

        <TechTalk.SpecFlow.Given("estos parametros de recalendarizacion")> _
        Public Sub DadoEstosParametrosDeRecalendarizacion(ByVal parametros As ParametrosAlRecalendarizarDTO)
            _parametrosAlRecalendarizar = parametros
        End Sub

        <TechTalk.SpecFlow.Given("no se tiene comunicacion con la entidad destino")> _
        Public Sub DadoNoSeTieneComunicacionConLaEntidadDestino()
        End Sub

        <TechTalk.SpecFlow.When("la transaccion ""(.*)"" se envia a confirmar con (.*) reintentos realizados")> _
        Public Sub CuandoLaTransaccionSeEnviaAConfirmarConReintentosRealizados(ByVal codigoDeReferencia As String, ByVal reintentosRealizados As Int32)
            _reintentosRealizados = reintentosRealizados
            _codigoDeReferencia = codigoDeReferencia
        End Sub

        <TechTalk.SpecFlow.Then("se recalendariza de esta manera")> _
        Public Sub EntoncesSeRecalendarizaDeEstaManera(ByVal laInstruccion As InstruccionParaRecalendarizar)
            _laInstruccionParaRecalendarizar = laInstruccion
        End Sub

        <TechTalk.SpecFlow.Then("se registra la recalendarizacion en la bitacora")> _
        Public Sub EntoncesSeRegistraLaRecalendarizacionEnLaBitacora()
            A.CallTo(Function() _repo.ObtenerTransaccion(_codigoDeReferencia)).Returns(_laTransaccion)
            A.CallTo(Function() _repo.ObtenerParametros(_laTransaccion)).Returns(_parametrosDeEntidad)
            A.CallTo(Function() _invo.Confirmar(_instruccionDeConfirmacion)).Returns(_respuestaNoFueExitosa)
            A.CallTo(Function() _repo.ObtenerParametrosParaRecalendarizar()).Returns(_parametrosAlRecalendarizar)

            ' sut
            Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
            app.Confirmar(_codigoDeReferencia, _reintentosRealizados, _fechaActual)

            ' verificaciones
            A.CallTo(Sub() _cale.Reintentar(_laInstruccionParaRecalendarizar)).MustHaveHappened()
            Dim mensajeEsperado As New MensajeTransaccionFueRecalendarizada(_codigoDeReferencia, _laInstruccionParaRecalendarizar.NumeroDeReintento, _laInstruccionParaRecalendarizar.FechaDeInicio)
            A.CallTo(Sub() _bita.EscribirTransaccionFueRecalendarizada(mensajeEsperado)).MustHaveHappened()
        End Sub

        <TechTalk.SpecFlow.Then("se registra en bitacora que no se puede invocar a la entidad por motivo de parametros invalidos")> _
        Public Sub EntoncesSeRegistraEnBitacoraQueNoSePuedeInvocarALaEntidadPorMotivoDeParametrosInvalidos()
            A.CallTo(Function() _repo.ObtenerTransaccion(_codigoDeReferencia)).Returns(_laTransaccion)
            A.CallTo(Function() _repo.ObtenerParametros(_laTransaccion)).Returns(_parametrosDeEntidad)

            ' sut
            Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
            app.Confirmar(_codigoDeReferencia, _reintentosRealizados, _fechaActual)

            ' verificaciones
            Dim mensajeEsperado As New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(_laTransaccion)
            A.CallTo(Sub() _bita.EscribirErrorNoSePuedeInvocarAEntidad(mensajeEsperado)).MustHaveHappened()
        End Sub


#End Region

#Region "Mensajes de bitacora"

        Private _fechaDeCalendarizacion As Date
        Private _mensajeObtenido As String

        <TechTalk.SpecFlow.Given("la fecha de calendarizacion es ""(.*)""")> _
        Public Sub DadoLaFechaDeCalendarizacionEs(ByVal fechaDeCalendarizacion As Date)
            _fechaDeCalendarizacion = fechaDeCalendarizacion
        End Sub

        <TechTalk.SpecFlow.When("se confirma")> _
        Public Sub CuandoSeConfirma()
            _mensajeObtenido = New MensajeTransaccionFueConfirmada(_laTransaccion).ToString
        End Sub

        <TechTalk.SpecFlow.When("se intenta confirmar pero no es permitido")> _
        Public Sub CuandoSeIntentaConfirmarPeroNoEsPermitido()
            _mensajeObtenido = New ErrorTransaccionDebeEstarAutorizadaYNoNotificada(_laTransaccion).ToString
        End Sub

        <TechTalk.SpecFlow.When("no se tiene los parametros necesarios para invocar a la entidad")> _
        Public Sub CuandoNoSeTieneLosParametrosNecesariosParaInvocarALaEntidad()
            _mensajeObtenido = New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(_laTransaccion).ToString
        End Sub

        <TechTalk.SpecFlow.When("no se tuvo un error de comunicacion y se recalendariza el reintento (.*)")> _
        Public Sub CuandoNoSeTuvoUnErrorDeComunicacionYSeRecalendarizaElReintento(ByVal numeroDeReintento As Int32)
            _mensajeObtenido = New MensajeTransaccionFueRecalendarizada(_laTransaccion.CodigoDeReferencia, numeroDeReintento, _fechaDeCalendarizacion).ToString()
        End Sub

        <TechTalk.SpecFlow.Then("este será el mensaje en bitacora ""(.*)""")> _
        Public Sub EntoncesEsteSeraElMensajeEnBitacora(ByVal mensajeEsperado As String)
            Assert.AreEqual(mensajeEsperado, _mensajeObtenido)
        End Sub
#End Region

    End Class

End Namespace

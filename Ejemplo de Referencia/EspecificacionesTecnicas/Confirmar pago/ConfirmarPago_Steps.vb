Imports TechTalk.SpecFlow
Imports TechTalk.SpecFlow.Assist
Imports LogicaDeNegocio
Imports LogicaDeAplicacion
Imports FakeItEasy

<Binding()> _
Public Class ConfirmarPago_Steps

    'datos existentes
    Friend _laSolicitud As SolicitudAlConfirmar
    Friend _elPago As PagoPorConfirmar
    Friend _parametrosDeEntidad As ParametrosAlConfirmar

    ' dependencias
    Friend _repo As IRepositorioAlConfirmar
    Friend _bita As IBitacoraAlConfirmar
    Friend _cale As ICalendarizadorAlConfirmar
    Friend _invo As IInvocadorAlConfirmar

    ' datos del escenario
    Friend _pagoConfirmado As PagoPorConfirmar

    Public Sub New()
        _repo = A.Fake(Of IRepositorioAlConfirmar)()
        _bita = A.Fake(Of IBitacoraAlConfirmar)()
        _cale = A.Fake(Of ICalendarizadorAlConfirmar)()
        _invo = A.Fake(Of IInvocadorAlConfirmar)()
    End Sub

    <TechTalk.SpecFlow.Given("un pago")> _
    Public Sub DadoUnPago(ByVal elPago As PagoPorConfirmar)
        _elPago = elPago
    End Sub

    <TechTalk.SpecFlow.When("se solicita confirmar el pago")> _
    Public Sub CuandoSeSolicitaConfirmarElPago(ByVal laSolicitud As SolicitudAlConfirmar)
        _laSolicitud = laSolicitud
    End Sub

    <TechTalk.SpecFlow.Given("estos parametros de confirmacion de la entidad 501")> _
    Public Sub DadoEstosParametrosDeConfirmacion(ByVal parametrosDeEntidad As ParametrosAlConfirmar)
        _parametrosDeEntidad = parametrosDeEntidad
    End Sub

#Region "Basico"

    Dim _instruccionDeConfirmacion As InstruccionDeConfirmacion
    Dim _respuestaFueExitosa As New RespuestaAlInvocarEntidad() With {.ComunicacionFueExitosa = True}


    <TechTalk.SpecFlow.Then("se generara esta confirmacion a la entidad destino")> _
    Public Sub EntoncesSeGeneraEstaConfirmacionALaEntidadDestino(ByVal instruccion As InstruccionDeConfirmacion)
        _instruccionDeConfirmacion = instruccion
    End Sub

    <TechTalk.SpecFlow.Then("se actualizara la informacion del pago en la base de datos")> _
    Public Sub EntoncesSeActualizaLaInformacionDelPagoEnLaBaseDeDatos(ByVal pagoConfirmado As PagoPorConfirmar)
        _pagoConfirmado = pagoConfirmado
    End Sub

    <TechTalk.SpecFlow.Then("se escribirá en bitacora que el pago fue confirmado")> _
    Public Sub SeEscribiraEnBitacoraQueElPagoFueConfirmado()
        ' HACK La invocacion al SUT se realiza en el ultimo paso del escenario ya que es ahi donde se ha establecido todos los datos requeridos
        ' Esto es solamente en los casos en donde se utiliza Fakes.
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_pagoConfirmado.CodigoDeReferencia)).Returns(_elPago)
        A.CallTo(Function() _repo.ObtenerParametros(_elPago)).Returns(_parametrosDeEntidad)
        A.CallTo(Function() _invo.InvocarParaConfirmar(_instruccionDeConfirmacion)).Returns(_respuestaFueExitosa)

        ' sut
        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
        app.Confirmar(_laSolicitud)

        ' verificaciones
        A.CallTo(Sub() _repo.ActualizarPagoConfirmado(_pagoConfirmado)).MustHaveHappened()
        Dim mensajeEsperado As New MensajePagoFueConfirmado(_elPago)
        A.CallTo(Sub() _bita.EscribirPagoFueConfirmado(mensajeEsperado)).MustHaveHappened()
    End Sub

#End Region

#Region "Solo se confirmara pagos autorizados"

    <TechTalk.SpecFlow.Then("se registra en bitacora que solo se confirma pagos autorizados y no notificados")> _
    Public Sub EntoncesSeRegistraEnBitacoraQueSoloSeConfirmaPagosAutorizadosYNoNotificados()
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_elPago.CodigoDeReferencia)).Returns(_elPago)
        ' sut
        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, Nothing)
        app.Confirmar(_laSolicitud)

        ' verificar resultados
        Dim mensajeEsperado As New ErrorPagoDebeEstarAutorizadoYNoNotificado(_elPago)
        A.CallTo(Sub() _bita.EscribirErrorAlConfirmar(mensajeEsperado)).MustHaveHappened()
    End Sub

#End Region

#Region "Se reintentará si no hay comunicación con la entidad destino"

    Private _parametrosAlRecalendarizar As ParametrosAlReintentar
    Private _respuestaEntidad As RespuestaAlInvocarEntidad
    Private _laInstruccionParaReintentar As InstruccionParaReintentar

    <TechTalk.SpecFlow.Given("estos parametros de recalendarizacion")> _
    Public Sub DadoEstosParametrosDeRecalendarizacion(ByVal parametros As ParametrosAlReintentar)
        _parametrosAlRecalendarizar = parametros
    End Sub

    Dim _respuestaNoFueExitosa As RespuestaAlInvocarEntidad = Nothing

    <TechTalk.SpecFlow.Given("no se tiene comunicacion con la entidad destino")> _
    Public Sub DadoNoSeTieneComunicacionConLaEntidadDestino()
        _respuestaNoFueExitosa = New RespuestaAlInvocarEntidad() With {.ComunicacionFueExitosa = False}
    End Sub

    <TechTalk.SpecFlow.Then("se recalendariza de esta manera")> _
    Public Sub EntoncesSeRecalendarizaDeEstaManera(ByVal laInstruccion As InstruccionParaReintentar)
        _laInstruccionParaReintentar = laInstruccion
    End Sub

    <TechTalk.SpecFlow.Then("se registra la recalendarizacion en la bitacora")> _
    Public Sub EntoncesSeRegistraLaRecalendarizacionEnLaBitacora()
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_laSolicitud.CodigoDeReferencia)).Returns(_elPago)
        A.CallTo(Function() _repo.ObtenerParametros(_elPago)).Returns(_parametrosDeEntidad)
        A.CallTo(Function() _invo.InvocarParaConfirmar(_instruccionDeConfirmacion)).Returns(_respuestaNoFueExitosa)
        A.CallTo(Function() _repo.ObtenerParametrosParaReintentar()).Returns(_parametrosAlRecalendarizar)

        ' sut
        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
        app.Confirmar(_laSolicitud)

        ' verificaciones
        A.CallTo(Sub() _cale.Reintentar(_laInstruccionParaReintentar)).MustHaveHappened()
        Dim mensajeEsperado As New MensajeConfirmacionDePagoReintentado(_laInstruccionParaReintentar)
        A.CallTo(Sub() _bita.EscribirConfirmacionDePagoReintentado(mensajeEsperado)).MustHaveHappened()
    End Sub

    <TechTalk.SpecFlow.Then("se registra en bitacora que no se puede invocar a la entidad por motivo de parametros invalidos")> _
    Public Sub EntoncesSeRegistraEnBitacoraQueNoSePuedeInvocarALaEntidadPorMotivoDeParametrosInvalidos()
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_laSolicitud.CodigoDeReferencia)).Returns(_elPago)
        A.CallTo(Function() _repo.ObtenerParametros(_elPago)).Returns(_parametrosDeEntidad)

        ' sut
        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
        app.Confirmar(_laSolicitud)

        ' verificaciones
        Dim mensajeEsperado As New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(_elPago)
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
        _mensajeObtenido = New MensajePagoFueConfirmado(_elPago).ToString
    End Sub

    <TechTalk.SpecFlow.When("se intenta confirmar pero no es permitido")> _
    Public Sub CuandoSeIntentaConfirmarPeroNoEsPermitido()
        _mensajeObtenido = New ErrorPagoDebeEstarAutorizadoYNoNotificado(_elPago).ToString
    End Sub

    <TechTalk.SpecFlow.When("no se tiene los parametros necesarios para invocar a la entidad")> _
    Public Sub CuandoNoSeTieneLosParametrosNecesariosParaInvocarALaEntidad()
        _mensajeObtenido = New ErrorNoSePuedeInvocarAEntidadPorParametrosInvalidos(_elPago).ToString
    End Sub

    <TechTalk.SpecFlow.Given("un reintento de confirmacion")> _
    Public Sub DadoUnReintentoDeConfirmacion(ByVal laInstruccionParaReintentar As InstruccionParaReintentar)
        _laInstruccionParaReintentar = laInstruccionParaReintentar
    End Sub

    <TechTalk.SpecFlow.When("se registra en bitacora")> _
    Public Sub CuandoSeRegistraEnBitacora()
        _mensajeObtenido = New MensajeConfirmacionDePagoReintentado(_laInstruccionParaReintentar).ToString()
    End Sub

    <TechTalk.SpecFlow.Then("este será el mensaje ""(.*)""")> _
    Public Sub EntoncesEsteSeraElMensaje(ByVal mensajeEsperado As String)
        Assert.AreEqual(mensajeEsperado, _mensajeObtenido)
    End Sub

#End Region

End Class


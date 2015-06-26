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
    Friend _bita As IBitacora
    Friend _cale As ICalendarizador
    Friend _invo As IInvocador

    ' datos del escenario
    Friend _pagoConfirmado As PagoConfirmado

    Public Sub New()
        _repo = A.Fake(Of IRepositorioAlConfirmar)()
        _bita = A.Fake(Of IBitacora)()
        _cale = A.Fake(Of ICalendarizador)()
        _invo = A.Fake(Of IInvocador)()
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
    Public Sub EntoncesSeActualizaLaInformacionDelPagoEnLaBaseDeDatos(ByVal pagoConfirmado As PagoConfirmado)
        _pagoConfirmado = pagoConfirmado
    End Sub

    <TechTalk.SpecFlow.Then("se escribirá en bitacora ""(.*)""")> _
    Public Sub EntoncesSeEscribiraEnBitacora(ByVal elMensajeEsperado As String)

        ' HACK La invocacion al SUT se realiza en el ultimo paso del escenario ya que es ahi donde se ha establecido todos los datos requeridos
        ' Esto es solamente en los casos en donde se utiliza Fakes.
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_pagoConfirmado.CodigoDeReferencia)).Returns(_elPago)
        A.CallTo(Function() _repo.ObtenerParametrosParInvocarEntidad(_elPago.CodigoDeEntidadDestino)).Returns(_parametrosDeEntidad)
        A.CallTo(Function() _invo.InvocarParaConfirmar(_instruccionDeConfirmacion)).Returns(_respuestaFueExitosa)

        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
        app.Confirmar(_laSolicitud)

        ' verificaciones
        A.CallTo(Sub() _repo.ActualizarPagoConfirmado(_pagoConfirmado)).MustHaveHappened()
        A.CallTo(Sub() _bita.EscribirMensajeInformativo(elMensajeEsperado)).MustHaveHappened()
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

#End Region

    <TechTalk.SpecFlow.Then("se registra en bitacora que solo se confirma pagos autorizados y no notificados ""(.*)""")> _
    Public Sub EntoncesSeRegistraEnBitacoraQueSoloSeConfirmaPagosAutorizadosYNoNotificados(ByVal mensajeEsperado As String)
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_elPago.CodigoDeReferencia)).Returns(_elPago)
        ' sut
        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, Nothing)
        app.Confirmar(_laSolicitud)

        ' verificar resultados
        A.CallTo(Sub() _bita.EscribirError(mensajeEsperado)).MustHaveHappened()
    End Sub

    <TechTalk.SpecFlow.Then("se registra la recalendarizacion en la bitacora ""(.*)""")> _
    Public Sub EntoncesSeRegistraLaRecalendarizacionEnLaBitacora(ByVal mensajeEsperado As String)
        A.CallTo(Function() _repo.ObtenerPagoPorConfirmar(_laSolicitud.CodigoDeReferencia)).Returns(_elPago)
        A.CallTo(Function() _repo.ObtenerParametrosParInvocarEntidad(_elPago.CodigoDeEntidadDestino)).Returns(_parametrosDeEntidad)
        A.CallTo(Function() _invo.InvocarParaConfirmar(_instruccionDeConfirmacion)).Returns(_respuestaNoFueExitosa)
        A.CallTo(Function() _repo.ObtenerParametrosParaReintentar()).Returns(_parametrosAlRecalendarizar)

        ' sut
        Dim app As New CoordinadorAlConfirmar(_bita, _repo, _invo, _cale)
        app.Confirmar(_laSolicitud)

        ' verificaciones
        A.CallTo(Sub() _cale.Reintentar(_laInstruccionParaReintentar)).MustHaveHappened()
        A.CallTo(Sub() _bita.EscribirMensajeInformativo(mensajeEsperado)).MustHaveHappened()
    End Sub

    <TechTalk.SpecFlow.Then("se escribirá el error de parametros en bitacora ""(.*)""")> _
    Public Sub EntoncesSeEscribiraElErrorDeParametrosEnBitacora(ByVal mensajeEsperado As String)

    End Sub

End Class


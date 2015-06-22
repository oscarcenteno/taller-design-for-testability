Imports TechTalk.SpecFlow
Imports TechTalk.SpecFlow.Assist
Imports LogicaDeNegocio
Imports LogicaDeAplicacion

<Binding()> _
Public Class ConversionesDeParametros

    <StepArgumentTransformation()>
    Public Function TransformeASolicitudAlConfirmar(table As Table) As SolicitudAlConfirmar
        Return table.CreateInstance(Of SolicitudAlConfirmar)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAPagoPorConfirmar(table As Table) As PagoPorConfirmar
        Return table.CreateInstance(Of PagoPorConfirmar)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAParametrosAlConfirmarDTO(table As Table) As ParametrosAlConfirmar
        Return table.CreateInstance(Of ParametrosAlConfirmar)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAInstruccionDeConfirmacion(table As Table) As InstruccionDeConfirmacion
        Return table.CreateInstance(Of InstruccionDeConfirmacion)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAParametrosAlRecalendarizarDTO(table As Table) As ParametrosAlReintentar
        Return table.CreateInstance(Of ParametrosAlReintentar)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAInstruccionParaReintentar(table As Table) As InstruccionParaReintentar
        Return table.CreateInstance(Of InstruccionParaReintentar)()
    End Function

End Class


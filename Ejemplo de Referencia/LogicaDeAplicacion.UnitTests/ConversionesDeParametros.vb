Imports TechTalk.SpecFlow
Imports TechTalk.SpecFlow.Assist
Imports LogicaDeNegocio

<Binding()> _
Public Class ConversionesDeParametros

    <StepArgumentTransformation()>
    Public Function TransformeATransaccionDTO(table As Table) As TransaccionDTO
        Return table.CreateInstance(Of TransaccionDTO)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAParametrosAlConfirmarDTO(table As Table) As ParametrosAlConfirmarDTO
        Return table.CreateInstance(Of ParametrosAlConfirmarDTO)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAInstruccionDeConfirmacion(table As Table) As InstruccionDeConfirmacion
        Return table.CreateInstance(Of InstruccionDeConfirmacion)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAParametrosAlRecalendarizarDTO(table As Table) As ParametrosAlRecalendarizarDTO
        Return table.CreateInstance(Of ParametrosAlRecalendarizarDTO)()
    End Function

    <StepArgumentTransformation()>
    Public Function TransformeAInstruccionParaRecalendarizar(table As Table) As InstruccionParaRecalendarizar
        Return table.CreateInstance(Of InstruccionParaRecalendarizar)()
    End Function

End Class


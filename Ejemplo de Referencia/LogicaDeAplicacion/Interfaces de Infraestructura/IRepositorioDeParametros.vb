Imports LogicaDeNegocio

Public Interface IRepositorioDeParametros

    Function ObtenerParametrosParaConfirmar() As ParametrosAlConfirmar

    Function ObtenerParametrosParaRecalendarizar() As ParametrosAlRecalendarizar

End Interface

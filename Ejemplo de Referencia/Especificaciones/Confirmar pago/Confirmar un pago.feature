Característica: Confirmar un pago
	Reglas al confirmar un pago
	
Esquema del escenario: Se puede confirmar si el pago esta autorizado y no se ha notificado
Dado un pago en estado "<estado>" y "<se ha notificado>"
Cuando se solicita confirmar
Entonces se confirma "<se confirma>"

Ejemplos:
| intencion                      | estado     | se ha notificado | se confirma |
| el caso basico                 | Autorizado | No               | Si          |
| no se confirma varias veces    | Autorizado | Si               | No          |
| en proceso no se confirman     | EnProceso  | No               | No          |
| los rechazados no se confirman | Rechazado  | Si               | No          |
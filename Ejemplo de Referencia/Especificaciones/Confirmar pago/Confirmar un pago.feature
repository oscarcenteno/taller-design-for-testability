Característica: Confirmar un pago
	Reglas al confirmar un pago
	
Esquema del escenario: Se puede confirmar si el pago esta autorizado y no se ha notificado
Dada una transaccion en estado "<estado>" y "<se ha notificado>"
Cuando se solicita confirmar
Entonces se confirma "<se confirma>"

Ejemplos:
| intencion                      | estado     | se ha notificado | se confirma |
| el caso basico                 | Autorizada | No               | Si          |
| no se confirma varias veces    | Autorizada | Si               | No          |
| en proceso no se confirman     | EnProceso  | No               | No          |
| las rechazadas no se confirman | Rechazada  | Si               | No          |

Escenario: Luego de confirmar, la transaccion está notificada

Dada una transaccion que no ha sido notificada
Y la fecha es "2015-06-01 12:00 PM"
Cuando se confirma
Entonces la transaccion se ha notificado
Y registra la fecha de la confirmación 

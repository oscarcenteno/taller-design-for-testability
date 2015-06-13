Característica: Confirmar una transaccion
	Se debe poder confirmar solamente las transacciones autorizadas y que no se han notificado.
	Se debe reintentar las confirmaciones.

Esquema del escenario: Se puede confirmar si la transaccion esta autorizada y no se ha notificado
Dada una transaccion en estado "<estado>" y "<se ha notificado>"
Cuando se solicita confirmar
Entonces se confirma "<se confirma>"

Ejemplos:
| intencion                      | estado     | se ha notificado | se confirma |
| el caso basico                 | Autorizada | No               | Si          |
| no se confirma varias veces    | Autorizada | Si               | No          |
| en proceso no se confirman     | EnProceso  | No               | No          |
| las rechazadas no se confirman | Rechazada  | Si               | No          |

Esquema del escenario: Se reintenta hasta tres veces

Dada una transaccion que ha sido intentada "<n>" veces
Y ha sido autorizada pero no notificada
Cuando se da un error al confirmarla
Entonces se puede reintentar "<reintentar>"

Ejemplos:
| intencion                  | n | reintentar |
| si nunca se ha reintentado | 0 | Si         |
| primer reintento           | 1 | Si         |
| segundo reintento          | 2 | Si         |
| el maximo no se reintenta  | 3 | No         |

Escenario: Luego de confirmar, la transaccion está notificada

Dada una transaccion que no ha sido notificada
Y la fecha es "2015-06-01 12:00 PM"
Cuando se confirma
Entonces la transaccion se ha notificado
Y registra la fecha de la confirmación 

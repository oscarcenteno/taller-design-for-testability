Característica: Reintentar una confirmacion
	Se debe reintentar las confirmaciones si no se pudo contactar a la entidad destino

Esquema del escenario: Se reintenta hasta tres veces

Dada un pago que ha sido intentado <n> veces
Y se definio un maximo de 3 reintentos por pago
Cuando no se pudo contactar a la entidad destino
Entonces se puede "<reintentar>"

Ejemplos:
| intencion del ejemplo      | n | reintentar |
| si nunca se ha reintentado | 0 | Si         |
| primer reintento           | 1 | Si         |
| segundo reintento          | 2 | Si         |
| el maximo no se reintenta  | 3 | No         |
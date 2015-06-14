Característica: Flujo Basico

Escenario: Confirmar_FlujoBasico_Exitoso

Dada esta transaccion
| Propiedad          | Valor               |
| CodReferencia      | Ref1                |
| Estado             | Autorizada          |
| Se ha notificado   | False               |
| CodEntidadOrigen   | 401                 |
| CodEntidadDestino  | 501                 |
| NumTelefonoDestino | 60607070            |
| CodMonedaDestino   | 1                   |
| FecValor           | 2015-06-01 11:59 AM |

Y estos parametros de confirmacion
| Propiedad | Valor             |
| Url       | http://entidad501 |
| TimeOut   | 3000              |
| Cn        | CnEntidad501      |

Y la fecha actual es "2015-06-01 12:00 PM"

Cuando la transaccion "Ref1" se envia a confirmar exitosamente

Entonces se genera esta confirmacion a la entidad destino
| Propiedad         | Valor               |
| CodReferencia     | Ref1                |
| CodEntidadDestino | 501                 |
| Url               | http://entidad501   |
| TimeOut           | 3000                |
| Cn                | CnEntidad501        |
| FecConfirmacion   | 2015-06-01 12:00 PM |

Y se actualiza la informacion de la transaccion
| Propiedad          | Valor               |
| CodReferencia      | Ref1                |
| Estado             | Autorizada          |
| Se ha notificado   | True                |
| CodEntidadOrigen   | 401                 |
| CodEntidadDestino  | 501                 |
| NumTelefonoDestino | 60607070            |
| CodMonedaDestino   | 1                   |
| FecValor           | 2015-06-01 11:59 AM |
| FecConfirmacion    | 2015-06-01 12:00 PM |

Y se registra este mensaje en la bitacora "Se confirmó la operación con referencia [Ref1] de la entidad origen [401], en moneda [1] hacia el teléfono [60607070] asociado a la entidad destino [501]"

Característica: Casos alternos al confirmar un pago
	Se detalla las validaciones y acciones esperadas.

Escenario: Solo se confirmará pagos autorizados

Dada una transaccion 
| Propiedad            | Valor     |
| Codigo de Referencia | Ref2      |
| Estado               | EnProceso |
Cuando la transaccion "Ref2" se envia a confirmar
Entonces se registra en bitacora que solo se confirma transacciones autorizadas y no notificadas

Escenario: Se enviará una sola confirmación a la entidad destino

Dada una transaccion
| Propiedad            | Valor      |
| Codigo de Referencia | Ref2       |
| Estado               | Autorizada |
| Se ha notificado     | True       |
Y la fecha actual es "2015-06-01 12:00 PM"
Cuando la transaccion "Ref2" se envia a confirmar
Entonces se registra en bitacora que solo se confirma transacciones autorizadas y no notificadas

Escenario: Se reintentará si no hay comunicación con la entidad destino

Dada una transaccion
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Estado                     | Autorizada          |
| Se ha notificado           | False               |
| Codigo de Entidad Origen   | 401                 |
| Codigo de Entidad Destino  | 501                 |
| Numero de Telefono Destino | 60607070            |
| Codigo de Moneda Destino   | 1                   |
| Fecha Valor                | 2015-06-01 11:59 AM |

Y estos parametros de confirmacion de la entidad 501
| Propiedad | Valor             |
| Url       | http://entidad501 |
| TimeOut   | 3000              |
| Cn        | CnEntidad501      |

Y estos parametros de recalendarizacion
| Propiedad                   | Valor    |
| Cantidad maxima de intentos | 3        |
| Intervalo de notificacion   | 00:05:00 |

Y la fecha actual es "2015-06-01 12:00 PM"
Y no se tiene comunicacion con la entidad destino 

Cuando la transaccion "Ref1" se envia a confirmar con 0 reintentos realizados

Entonces se generara esta confirmacion a la entidad destino
| Propiedad                 | Valor               |
| Codigo de Referencia      | Ref1                |
| Codigo de Entidad Destino | 501                 |
| Url                       | http://entidad501   |
| TimeOut                   | 3000                |
| Cn                        | CnEntidad501        |
| Fecha de Confirmacion     | 2015-06-01 12:00 PM |

Y se recalendariza de esta manera
| Propiedad            | Valor               |
| Codigo de Referencia | Ref1                |
| Fecha de Inicio      | 2015-06-01 12:05 PM |
| Numero de Reintento  | 1                   |
Y se registra la recalendarizacion en la bitacora

Escenario: No se reintentará si no se puede invocar a la entidad por problemas de parámetros
Dada una transaccion
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Estado                     | Autorizada          |
| Se ha notificado           | False               |
| Codigo de Entidad Origen   | 401                 |
| Codigo de Entidad Destino  | 501                 |
| Numero de Telefono Destino | 60607070            |
| Codigo de Moneda Destino   | 1                   |
| Fecha Valor                | 2015-06-01 11:59 AM |

Y estos parametros de confirmacion de la entidad 501
| Propiedad | Valor             |
| Url       | |
| TimeOut   | 3000              |
| Cn        | CnEntidad501      |

Cuando la transaccion "Ref1" se envia a confirmar con 0 reintentos realizados

Entonces se registra en bitacora que no se puede invocar a la entidad por motivo de parametros invalidos
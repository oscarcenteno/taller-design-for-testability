Característica: Casos alternos al confirmar un pago
	Se detalla las validaciones y acciones esperadas.

Escenario: Solo se confirmará pagos autorizados

Dado un pago
| Propiedad            | Valor     |
| Codigo de Referencia | Ref2      |
| Estado               | EnProceso |
Cuando se solicita confirmar el pago 
| Propiedad            | Valor |
| Codigo de referencia | Ref2  |
Entonces se registra en bitacora que solo se confirma pagos autorizados y no notificados "No se confirmó el pago con Cod. Referencia [Ref2] pues debe estar autorizado y no notificado."

Escenario: Se enviará una sola confirmación a la entidad destino

Dado un pago
| Propiedad            | Valor      |
| Codigo de Referencia | Ref2       |
| Estado               | Autorizado |
| Se ha notificado     | True       |

Cuando se solicita confirmar el pago 
| Propiedad            | Valor |
| Codigo de Referencia | Ref2  |

Entonces se registra en bitacora que solo se confirma pagos autorizados y no notificados "No se confirmó el pago con Cod. Referencia [Ref2] pues debe estar autorizado y no notificado."

Escenario: Se reintentará si no hay comunicación con la entidad destino

Dado un pago
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Estado                     | Autorizado          |
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

Y no se tiene comunicacion con la entidad destino 

Cuando se solicita confirmar el pago 
| Propiedad             | Valor               |
| Codigo de referencia  | Ref1                |
| Fecha y Hora Actual   | 2015-06-01 12:00 PM |
| Reintentos realizados | 0                   |

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
Y se registra la recalendarizacion en la bitacora "Se ha calendarizado el reintento para confirmar #1 para el pago con referencia Ref1 en la fecha/hora 01/06/2015 12:05:00."

Escenario: No se reintentará si no se puede invocar a la entidad por problemas de parámetros
Dado un pago
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Estado                     | Autorizado          |
| Se ha notificado           | False               |
| Codigo de Entidad Origen   | 401                 |
| Codigo de Entidad Destino  | 501                 |
| Numero de Telefono Destino | 60607070            |
| Codigo de Moneda Destino   | 1                   |
| Fecha Valor                | 2015-06-01 11:59 AM |

Y estos parametros de confirmacion de la entidad 501
| Propiedad | Valor        |
| Url       |              |
| TimeOut   | 3000         |
| Cn        | CnEntidad501 |

Cuando se solicita confirmar el pago 
| Propiedad            | Valor |
| Codigo de referencia | Ref1  |
Entonces se escribirá el error de parametros en bitacora "No se confirmó el pago Ref1 pues los parametros requeridos para invocar a la entidad 501 no están configurados"
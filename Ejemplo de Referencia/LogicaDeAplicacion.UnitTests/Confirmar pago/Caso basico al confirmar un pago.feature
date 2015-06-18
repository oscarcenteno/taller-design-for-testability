Característica: Caso basico al confirmar un pago
	La confirmación de un pago implica la invocación a la entidad destino, la actualización en base de datos y registro en bitácora

Escenario: Se realiza la confirmación de un pago autorizado por notificar

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

Y la fecha actual es "2015-06-01 12:00 PM"

Cuando la transaccion "Ref1" se envia a confirmar exitosamente

Entonces se generara esta confirmacion a la entidad destino
| Propiedad                 | Valor               |
| Codigo de Referencia      | Ref1                |
| Codigo de Entidad Destino | 501                 |
| Url                       | http://entidad501   |
| TimeOut                   | 3000                |
| Cn                        | CnEntidad501        |
| Fecha de Confirmacion     | 2015-06-01 12:00 PM |

Y se actualizara la informacion de la transaccion
| Propiedad                  | Valor               |
| Codigo de Referencia       | Ref1                |
| Estado                     | Autorizada          |
| Se ha notificado           | True                |
| Codigo de Entidad Origen   | 401                 |
| Codigo de Entidad Destino  | 501                 |
| Numero de Telefono Destino | 60607070            |
| Codigo de Moneda Destino   | 1                   |
| Fecha Valor                | 2015-06-01 11:59 AM |
| Fecha de Confirmacion      | 2015-06-01 12:00 PM |

Y se escribirá en bitacora que la transaccion fue confirmada
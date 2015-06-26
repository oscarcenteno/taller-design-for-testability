Característica: Caso basico al confirmar un pago
	La confirmación de un pago implica la invocación a la entidad destino, la actualización en base de datos y registro en bitácora

Escenario: Se realiza la confirmación de un pago autorizado por notificar

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

Cuando se solicita confirmar el pago 
| Propiedad            | Valor               |
| Codigo de Referencia | Ref1                |
| Fecha y Hora Actual  | 2015-06-01 12:00 PM |

Entonces se generara esta confirmacion a la entidad destino
| Propiedad                 | Valor               |
| Codigo de Referencia      | Ref1                |
| Codigo de Entidad Destino | 501                 |
| Url                       | http://entidad501   |
| TimeOut                   | 3000                |
| Cn                        | CnEntidad501        |
| Fecha de Confirmacion     | 2015-06-01 12:00 PM |

Y se actualizara la informacion del pago en la base de datos
| Propiedad                  | Valor               |
| Codigo de Referencia       | Ref1                |
| Se ha notificado           | True                |
| Fecha de Confirmacion      | 2015-06-01 12:00 PM |

Y se escribirá en bitacora "Se confirmó el pago con referencia [Ref1] de la entidad origen [401], en moneda [1] hacia el teléfono [60607070] asociado a la entidad destino [501]"
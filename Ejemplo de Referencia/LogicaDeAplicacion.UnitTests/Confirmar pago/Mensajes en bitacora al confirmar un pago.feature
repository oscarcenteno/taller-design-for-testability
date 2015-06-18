Característica: Mensajes en bitacora al confirmar un pago

Escenario: Cuando se confirma una transaccion

Dada una transaccion
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Codigo de Entidad Origen   | 401                 |
| Codigo de Moneda Destino   | 1                   |
| Numero de Telefono Destino | 60607070            |
| Codigo de Entidad Destino  | 501                 |
Cuando se confirma 
Entonces este será el mensaje en bitacora "Se confirmó la operación con referencia [Ref1] de la entidad origen [401], en moneda [1] hacia el teléfono [60607070] asociado a la entidad destino [501]"

Escenario: Cuando no es permitido confirmar una transaccion
Dada una transaccion
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
Cuando se intenta confirmar pero no es permitido
Entonces este será el mensaje en bitacora "No se confirmó la transacción con Cod. Referencia [Ref1] pues debe estar autorizada y no notificada."

Escenario: Cuando no se puede invocar a la entidad por parametros invalidos
Dada una transaccion
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Codigo de Entidad Destino  | 501                 |
Cuando no se tiene los parametros necesarios para invocar a la entidad
Entonces este será el mensaje en bitacora "No se confirmó la transacción Ref1 pues los parametros requeridos para invocar a la entidad 501 no están configurados"

Escenario: Cuando se recalendariza la confirmacion
Dada una transaccion
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
Y la fecha de calendarizacion es "15-06-2015 9:00:00"
Cuando no se tuvo un error de comunicacion y se recalendariza el reintento 3
Entonces este será el mensaje en bitacora "Se ha calendarizado el reintento para Confirmar #3 para la transacción con referencia Ref1 en la fecha/hora 15/06/2015 9:00:00."

Característica: Mensajes en bitacora al confirmar un pago

Escenario: Cuando se confirma una transaccion

Dado un pago
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Codigo de Entidad Origen   | 401                 |
| Codigo de Moneda Destino   | 1                   |
| Numero de Telefono Destino | 60607070            |
| Codigo de Entidad Destino  | 501                 |
Cuando se confirma 
Entonces este será el mensaje "Se confirmó el pago con referencia [Ref1] de la entidad origen [401], en moneda [1] hacia el teléfono [60607070] asociado a la entidad destino [501]"

Escenario: Cuando no es permitido confirmar una transaccion
Dado un pago
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
Cuando se intenta confirmar pero no es permitido
Entonces este será el mensaje "No se confirmó el pago con Cod. Referencia [Ref1] pues debe estar autorizado y no notificado."

Escenario: Cuando no se puede invocar a la entidad por parametros invalidos
Dado un pago
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Codigo de Entidad Destino  | 501                 |
Cuando no se tiene los parametros necesarios para invocar a la entidad
Entonces este será el mensaje "No se confirmó el pago Ref1 pues los parametros requeridos para invocar a la entidad 501 no están configurados"

Escenario: Cuando se reintenta la confirmacion
Dada un reintento de confirmacion
| Propiedad            | Valor              |
| Codigo de referencia | Ref1               |
| Fecha de inicio      | 15-06-2015 9:00:00 |
| Numero de reintento  | 3                  |
Cuando se registra en bitacora
Entonces este será el mensaje "Se ha calendarizado el reintento para confirmar #3 para el pago con referencia Ref1 en la fecha/hora 15/06/2015 9:00:00."

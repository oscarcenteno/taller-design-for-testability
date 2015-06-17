Característica: Comparacion de TransaccionDTO

Antecedentes: 
Dado una transaccion base
| Propiedad                  | Valor               |
| Codigo de referencia       | Ref1                |
| Codigo de Entidad Destino  | 501                 |
| Codigo de Moneda Destino   | 1                   |
| Codigo de Entidad Origen   | 401                 |
| Numero de Telefono Destino | 60607070            |
| Estado                     | Autorizada          |
| Fecha Valor                | 2015-06-01 11:59 AM |
| Se ha notificado           | False               |
| FechaDeConfirmacion        | 2015-06-02 11:59 AM |

Esquema del escenario: TransaccionDTO_Equals
Dado otra con "<CodigoDeReferencia>", "<CodigoDeEntidadDestino>", "<CodigoDeMonedaDestino>", "<CodigoDeEntidadOrigen>", "<NumeroDeTelefonoDestino>", "<Estado>", "<FechaValor>", "<FechaDeConfirmacion>", " <SeHaNotificado> "
Cuando se compara
Entonces "<son iguales>"

Ejemplos:

| Intencion                            | CodigoDeReferencia | CodigoDeEntidadDestino | CodigoDeMonedaDestino | CodigoDeEntidadOrigen | NumeroDeTelefonoDestino | Estado     | FechaValor          | FechaDeConfirmacion | SeHaNotificado | son iguales |
| Base                                 | Ref1               | 501                    | 1                     | 401                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | Si          |
| CodigoDeReferencia es diferente      | Ref2               | 501                    | 1                     | 401                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| CodigoDeEntidadDestino es diferente  | Ref1               | 503                    | 1                     | 401                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| CodigoDeMonedaDestino es diferente   | Ref1               | 501                    | 2                     | 401                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| CodigoDeEntidadOrigen es diferente   | Ref1               | 501                    | 1                     | 402                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| NumeroDeTelefonoDestino es diferente | Ref1               | 501                    | 1                     | 401                   | 60607071                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| Estado es diferente                  | Ref1               | 501                    | 1                     | 401                   | 60607070                | EnProceso  | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| FechaValor es diferente              | Ref1               | 501                    | 1                     | 401                   | 60607070                | Autorizada | 2015-06-02 11:59 AM | 2015-06-02 11:59 AM | No             | No          |
| FechaDeConfirmacion es diferente     | Ref1               | 501                    | 1                     | 401                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 12:59 AM | No             | No          |
| SeHaNotificado es diferente          | Ref1               | 501                    | 1                     | 401                   | 60607070                | Autorizada | 2015-06-01 11:59 AM | 2015-06-02 11:59 AM | Si             | No          |             

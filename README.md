# FCE - UBA | CAI 02/2019 | Grupo2
## Trabajo Práctico - Comercio

### Descripción

Esta app implementa la sección de comercio de los lineamientos del trabajo práctico.

![Descripción](https://i.imgur.com/v1CuPCd.png)

- Recibe pedidos resultantes de ventas mediante los canales de venta online en la pantalla `Cargar ventas a lote diario`. Carga manual para el lote del día.
- En base a las ventas del día confecciona un lote de bultos para enviar a la empresa de logística para su distribución desde la pantalla `Enviar lote diario a logística`. Confirmación manual diaria.
- Desde la pantalla `Reportes de entrega` recibe de la empresa de logística un reporte de entrega de donde se puede reingresar el stock de los pedidos no entregados o devueltos. Este reingreso va a aumentar el stock real disponible.
- Administra el stock inicial del comercio. Las ventas ingresadas al sistema comprometen stock, que es deducido del real con los envíos a logística. En cuanto el stock real cae por debajo del punto de reposición ya sea por repetidas ventas o por tener más stock comprometido que real, se pueden hacer pedidos diarios a industrias en la pantalla `Pedidos de stock ` para reposición y recepción del stock en cantidades fijas por producto (modificables) en la pantalla `Control de stock`
- Los archivos generados y aceptados se condicen con el formato definido en los lineamientos.

### Uso

Clonando el repositorio y compilando, se puede empezar a usar. 

- La pantalla de bienvenida da una clara idea de la funcionalidad de cada sección, aparte de tener un panel de ayuda en cada una con una breve guía de uso. El flujo entre pantallas está ilustrado en el diagrama más arriba.
- El programa opera con un inventario de diez productos distintos.
- La interfaz se puede navegar con la ayuda de la tecla tab para ahorrar clics.
- Una vez que se eligió un producto en la lista desplegable en la pantalla de ventas online, la cantidad puede ser ingresada directamente dado que el foco pasa al cuadro de texto inmediatamente debajo, y se puede agregar a la lista de pedidos presionando la tecla enter en el mismo cuadro de texto, ahorrando el uso del mouse para buena parte de esta operatoria repetitiva.
- Las pantallas `Pedidos de stock` y `Enviar lote diario a logística` guardan un historial de los pedidos y lotes enviados (en base a los archivos generados) para comodidad y referencia del usuario.

### Descripciones, comportamiento esperado y validaciones implementadas

#### Archivos escenciales para el funcionamiento del programa:
- `DatosComercio.txt` contiene la información del comercio a cargar. Posee una sola línea de items delimitados por separadores ";" del tipo `CodigoComercio;RazonSocial;CUIT;DireccionEntregaPedidosStock;DireccionDevolucionLotesVentas`. Está validado por existencia del archivo, por cantidad de líneas (debe ser una sola), cantidad de elementos en la línea (deben ser 5), existencia del delimitador de split ";" (y por ende líneas vacías), chequeo por archivo vacío, y el código de comercio debe consistir de la letra C + un número positivo parseable, y el CUIT se valida por ser un número entero positivo parseable y tener 11 dígitos.

- `CantidadesReposicionStock.txt` contiene las cantidades fijas a pedir a industrias por producto por stock bajo que se cargan al iniciar el programa, con el formato `IDproduto;Cantidad`. Está validado por existencia del archivo, por cantidad de líneas (deben ser diez como el archivo de stock), por existencia del delimitador de split ';' (y por ende líneas vacías), por cantidad de items por línea (deben ser dos), por archivo vacío, por IDs duplicados y por posibilidad de parsear cada item de cada línea.

- `Stock.txt` contiene el stock inicial que se cargará. Su formato es el de líneas de números enteros delimitados por separadores ";" del tipo `IDproducto;StockReal;PuntoReposicion;StockComprometido;StockPendiente`. Contiene diez líneas, por ende diez productos a cargar al sistema. Está validado por existencia del archivo, por cantidad de líneas (deben ser diez como el archivo de cantidades de reposición), por existencia del delimitador de split ';' (y por ende líneas vacías), por cantidad de items por línea (deben ser cinco), por archivo vacío, por IDs duplicados y por posibilidad de parsear cada item de cada línea.

`DatosComercio.txt` y `CantidadesReposicionStock.txt` se validan uno contra el otro sobre la columna de IDs de producto, tanto en orden de los productos como en cantidad de productos totales, luego de pasar todas las validaciones que los harían válidos cada uno por separado. Ambos deben coincidir en este dato.

Estos tres archivos se copian siempre a la carpeta del ejecutable, vienen incluídos en la solución. Si alguno de los tres falla en su chequeo durante la carga, el programa se cierra con error fatal.

Todos los archivos de salida como se muestran en el diagrama se guardan en la carpeta Grupo2 ubicada en la raíz de la unidad C. Si existe, borra el contenido (de la última ejecución), sino la crea.

Todos los archivos que el programa crea para funcionar donde corre el ejecutable son borrados al salir.


#### Pantalla "Control de stock":
- Tabla de control de stock de solo lectura para el usuario, se llena al iniciar el programa desde el archivo "Stock.txt" y se va modificando a medida que se realizan transacciones en la aplicación, como confirmar un pedido o emitir un lote.
- Si el stock real de algún producto baja lo suficiente para caer por debajo del punto de reposición o se encuentra sobrecomprometido por ventas, se marcará en negrita para alertar al usuario de que tiene que hacer un pedido prontamente para reponer stock.
- La tabla de reposición indica el valor que se pide a fábrica cuando el stock de ese producto queda por debajo del punto de reposición. Los ID de producto son de solo lectura, los campos con las cantidades están limitados a 5 caracteres, solo se pueden ingresar números salvo el 0, y no se puede dejar vacía (queda marcado un error que no desaparece hasta que se llene con algo correcto).
- Tabla entregas pendientes de recepción que permite visualizar los productos que se pidieron a industria y están pendientes de recepción. Se deben tildar los pedidos que han ingresado y confirmarlos mediante el botón `Cargar pedido pendiente`. Solo se puede tildar dicho campo, los otros son de solo lectura. Debe recepcionar todo el stock entrante antes de poder hacer otro pedido y volver a recepcionar.

#### Pantalla "Cargar ventas a lote diario":
- El botón "Agregar item al pedido" no se habilita si no hay un ingreso en el campo "Cantidad". Este campo solo permite números enteros positivos de hasta cuatro dígitos, sin espacios ni ningún otro tipo de caracter.
- El botón "Confirmar pedido" se habilita si se ingresó un código de cliente (solo permite números enteros positivos de hasta 5 dígitos), una dirección de entrega (solo caracteres alfanuméricos) y si se ingresó al menos un producto para la venta a cargar.
- No permite ingresar pedidos que contengan algún producto cuya cantidad sea 0. Fuerza reingreso de la venta.

#### Pantalla "Enviar lote diario a logística"
- No permite usar el botón "Enviar lote a logística" si no se ha cargado al menos una venta anteriormente.
- El botón se deshabilita al enviar un lote, se vuelve a habilitar en cuanto se cargue al menos una nueva venta.
- Cuenta las ventas cargadas en el lote diario esperando ser enviadas, y las ventas pendientes por falta de stock. En cuanto se envían las diarias, el contador vuelve a cero, y en el historial/vista previa queda escrito el día y el nombre del archivo generado para ese día, con el pedido. Para comodidad del usuario aparte de los archivos de salida necesarios.
- Las ventas pendientes dependen de que se haga el pedido de stock a industrias, y se recepcione el nuevo stock. Recién con estas condiciones (stock real > stock comprometido) es que van a ser enviadas con el lote diario siguiente.
- Todos los campos y vistas previas en esta pantalla son de solo lectura, a modo informativo de los datos cargados desde "DatosComercio.txt" y del lote generado.

#### Pantalla "Pedidos Stock"
- El botón "Realizar pedido de stock a industrias" se habilita en cuanto algún producto de la lista tiene su stock real + comprometido + pendiente debajo del punto de reposición (cuando se marca en negrita en la pantalla de control de stock). Al presionar se genera un pedido que incluye todos los que se encuentran en esa condición y en la carpeta de salida se lo puede leer.
- Cuenta los productos cuyo stock es bajo y aquellos sobrecomprometidos por ventas (más cantidad que el stock real disponible), listos para ser pedidos. En cuanto se piden, el contador vuelve a cero, y en el historial/vista previa queda escrito el día y el nombre del archivo generado para ese día, con el pedido. Para comodidad del usuario aparte de los archivos de salida necesarios.
- Debe recepcionar el stock de un pedido a industrias hecho para poder hacer otro pedido nuevo.
- Todos los campos y vistas previas en esta pantalla son de solo lectura, a modo informativo de los datos cargados desde "DatosComercio.txt" y del pedido generado.


#### Pantalla "Reportes de entrega":
El archivo de entrada de logística está validado por contenido (formato esperado según lineamientos, archivo vacío, entradas duplicadas), por extensión (debe ser .txt) y por nombre. En particular, de los archivos de prueba/ejemplo disponibles en la carpeta `EjemplosReportes` junto al archivo de solución:
- `Entrega_C340_L643.txt` es aceptado, tiene su nombre y su contenido según el formato `[CodReferencia];[true/false]` (un juego por línea) acorde al archivo modelo en los lineamientos. Contiene entradas tanto entregadas como no entregadas.
- `Entrega_C340_L644.txt` es aceptado, y contiene todas sus entradas entregadas.
- `Entrega_C332_L117.txt` falla por tener entradas duplicadas
- `Entrega_C332_L116.txt` falla por no respetar el separador ';' entre código de referencia y estado del envío
- `Entrega_C332_L115.txt` falla por contener una frase que no coincide con el formato esperado
- Cualquier otro archivo con otro formato de nombre (sea su contenido válido como el del primero, o no) no es aceptado
- Cualquier otro archivo al que se le haya asignado un nombre correcto siendo cualquier otra cosa su contenido, no es aceptado


No se puede apretar el botón "Cargar stock de no entregados" si no se carga un reporte primero. Una vez que haya cargado un archivo válido, se van a mostrar en ambos lados de la pantalla los pedidos enviados y no enviados pertenecientes a este reporte. Ahora puede reingresar los pedidos que no hayan sido entregados al stock real. En caso de que el reporte contenga todos pedidos entregados, lo notifica y no permite ingresar nada al sistema.

Para probar la funcionaildad de reingreso:
1) Cargue algunas ventas en el sistema. 
2) Envíe la/s ventas que haya cargado. Necesita enviar al menos un lote con una venta (es decir, tener al menos un número de referencia). Revise el estado del stock antes de seguir.
3) En la carpeta de salida `C:\Grupo2`, haga una copia del archivo `Lote_(CodCliente)_(CodLote)_.txt` y cambie su nombre al formato esperado correspondiente, `Entrega_(CodCliente)_(CodLote)_.txt` usando el mismo código de cliente y el mismo código de lote.
4) Cambie el contenido del archivo para reflejar el estado de los números de referencia de los pedidos que se han entregado en el lote. Por ejemplo si su archivo de lote tiene un pedido con referencia `R1`, el contenido respectivo en el archivo de Entrega debe ser `R1;false` para denotar que no fue entregado, o `R1;true` para decir que fue entregado. Un número de referencia por renglón, como dice el archivo de ejemplo en los lineamientos. Números de referencia que no corresponden al lote se ignoran.
5) Ingrese este archivo en la pantalla y luego presione "Cargar stock de no entregados" si su reporte contiene algún número de referencia marcado como `false`. Revise el estado del stock, verá las cantidades vendidas reingresadas al stock real y habiendo vuelto al estado anterior.

Esta funcionalidad posee las siguientes validaciones:

- Solo reingresa reportes que hayan sido emitidos por la aplicación (es decir, en base a lotes a logística que existan en la carpeta de salida, asumiendo que la salida del programa es válida porque pasó todas las validaciones internas consideradas correctas por coincidir con el formato de salida de los lineamientos tras suficiente prueba => error => corrección)
- No permite reingresar más de una vez el mismo reporte
- Si se cargó un reporte cualquiera que no encuentra su par en la carpeta de salida, no permite su reingreso

### Varios
Naturalmente es trabajo en progreso y es un concepto, no debería ser usado por ningún negocio real (momentaneamente)




## Plan de pruebas

Creemos que con el siguente plan se puede probar toda la funcionalidad del programa, tocando todos los casos que se tuvieron en cuenta al diseñar el mismo

**Carga de datos desde los archivos** `DatosComercio.txt`, `CantidadesReposicionStock.txt`, `Stock.txt`

Todos los archivos se copian automáticamente a la carpeta del ejecutable, aunque hay un chequeo de existencia implementado.

 - `DatosComercio.txt`
	1. Vacíe el archivo. Al abrir el programa tendrá un error sobre este archivo, que está vacío.
	2. Restaure y agregue una segunda línea al archivo. Al abrir el programa tendrá un error sobre este archivo, que debe tener una sola línea.
	3. Restaure y elimine esta segunda linea, ahora elimine uno de los delimitadores ';', o agregue uno más a la línea. Dará error al respecto, deben ser 5 items separados por 4 delimitadores.
	4. Elimine todos los delimitadores. Dará error al respecto.
	5. Restaure. Pruebe de cambiar la letra C del código de comercio, o el número por algo no numérico, o asignar un número mayor a 5 cifras. Dará error al respecto.
	6. Restaure. Pruebe de cambiar el CUIT a un largo distinto de 11 dígitos o agregarle algo no numérico. Dará error al respecto.

- `CantidadesReposicionStock.txt`

	1. Vacíe el archivo. Dará un error sobre este archivo, que está vacío.
	2. Restaure y agregue o elimine una línea, o deje una línea vacía. Dará error al respecto. Deben ser diez contiguas.
	3. Elimine el delimitador ';' de alguna línea. Dará error al respecto.
	4. Agregue un delimitador de más a alguna línea. Dará error al respecto, deben ser dos datos delimitados por un separador por línea.
	5. Cambie por / agregue a algún número algo no numérico. Dará error al respecto.
	6. Repita el ID de producto en algunas líneas. Dará error al respecto.

- `Stock.txt`

	1. Vacíe el archivo. Dará un error sobre este archivo, que está vacío.
	2. Restaure y agregue o elimine una línea, o deje una línea vacía. Dará error al respecto. Deben ser diez contiguas.
	3. Elimine el delimitador ';' de alguna línea. Dará error al respecto.
	4. Agregue un delimitador de más a alguna línea. Dará error al respecto, deben ser cinco datos delimitados por cuatro separadores por línea.
	5. Cambie por / agregue a algún número algo no numérico. Dará error al respecto.
	6. Repita el ID de producto en algunas líneas. Dará error al respecto.

- Tome los archivos `CantidadesReposicionStock.txt` y `Stock.txt` en sus estados iniciales y válidos, cambie el orden de alguna de las líneas cosa de que los IDs no estén en el mismo orden entre los archivos. Dará error al respecto.


Todos estos errores cierran el programa forzosamente.

1) **Iniciar programa. Chequeos iniciales:**
	1. **Pantalla de control de stock:** Familiarizarse con el stock inicial cargado desde el archivo Stock.txt, comprobar que esta tabla no es editable. Comprobar que las cantidades de pedido de stock son editables, con las celdas no pudiendo quedar vacías ni teniendo 0 como valor, ni pudiendo ingresar un número de más de 5 cifras. Haga cambios en estos valores ahora si lo desea. Comprobar que el botón de recepción de stock está deshabilitado por no tener ningún pedido hecho todavía.
	2. **Pantalla de pedidos de stock:** Verificar los datos del comercio cargados desde el archivo DatosComercio.txt, verificar que todo lo que se muestra en esta pantalla es de solo lectura.
	3. **Pantalla de carga de ventas:** Verificar que el botón confirmar pedido se habilita en cuanto se ingresan los datos del cliente y se agrega al menos un producto a este pedido. Verificar que los diez productos posibles de elección son diez como en la tabla de stock. Ingrese algunos valores y verifique que el botón limpiar pantalla cumpla su función.
	4. **Pantalla de envío de lotes diarios:** Verificar los datos del comercio cargados desde el archivo DatosComercio.txt, verificar que todo lo que se muestra en esta pantalla es de solo lectura.
	5. **Pantalla de reportes de entrega:** Se pueden usar los reportes de entrega de ejemplo, con el comportamiento esperado descripto más arriba. Verificar que el archivo `Entrega_C340_L643.txt` con pedidos no entregados no deja cargarse en este momento dado que no se hizo todavía ningún envío a logística. Dará error al respecto.
	6. Puede leer el panel de ayuda de cada pantalla el tiempo que necesite para familiarizarse aún más.

2) **Ir a la pantalla de cargar ventas. Cargar ventas día 1**
	1. Producto 1, cantidad 2100 (stock inicial real 2000, futura condición de sobrecomprometido en 100, < punto reposición)
	2. Producto 2, cantidad 300 (stock inicial real 1000, ningún problema)
	3. Producto 3, cantidad 700 (stock inicial real 800, futura condición de comprometido dejando stock real < punto reposición)
	4. Ingresar código de cliente y dirección de entrega a gusto. Recordarlos para uso futuro
	5. Confirmar pedido para reflejar estos cambios en la tabla de stock y sumar una venta al lote diario
3) **Ir a la pantalla control de stock**
	1. Verificar ID1, comprometido 2100, real 2000 en negrita (por estar sobrecomprometido)
	2. Verificar ID2, comprometido 300
	3. Verificar ID3, comprometido 700, real 800 en negrita (por estar en stock bajo)
4) **Ir a la pantalla de pedidos de stock**
	1. Verificar en el panel de estado: Stock bajo 2, Sobrecomprometido 1
	2. Verificar botón habilitado de pedido de stock a industrias
5) **Ir a la pantalla de envíos de lotes**
	1. Verificar en el panel de estado: Ventas en lote diario 1, pendientes 0
	2. Verificar botón habilitado de enviar lote
6) **Volver a la pantalla de carga de ventas, seguimos en día 1. Vamos a ingresar una nueva venta**
	1. Producto 1, cantidad 100 (stock inicial real 2000, futura condición de sobrecomprometido en 200)
	2. Producto 1, cantidad 100 (stock inicial real 2000, futura condición de sobrecomprometido en 300). Verifique que se actualizó la entrada del producto 1 en la lista al sumarse 100+100 = 200 para este pedido
	3. Ingresar código de cliente y dirección de entrega a gusto. Recordarlos para uso futuro. Use el anterior.
	4. Confirmar pedido para reflejar estos cambios en la tabla de stock y sumar una venta al lote diario
7) **Ir a la pantalla control de stock**
	1. Verificar ID1, comprometido 2300, real 2000 en negrita (por estar sobrecomprometido)
	2. Verificar ID2, comprometido 300
	3. Verificar ID3, comprometido 700, real 800 en negrita (por estar en stock bajo)
8) **Ir a la pantalla de pedidos de stock**
	1. Verificar en el panel de estado: Stock bajo 2, Sobrecomprometido 1
	2. Verificar botón habilitado de pedido de stock a industrias
9) **Ir a la pantalla de envíos de lotes**
	1. Verificar en el panel de estado: Ventas en lote diario 2, pendientes 0
	2. Verificar botón habilitado de enviar lote
10) **Pantalla enviar lote diario, vamos a enviar las dos ventas del día 1**
	1. Clic en enviar lote a logística.
	2. Verificar en la carpeta de salida la existencia del archivo de lote generado. Puede verlo desde el historial si no quiere ir a la carpeta.
	* R1;[Dirección de entrega para este cliente elegida]
	* P1;200
	* P2;300
    * P3;700
	3. Verificar en la pantalla de stock: 
	* ID1, real 1800, comprometido 2100 (-200, los de la segunda venta, la primera de 2100 quedó pendiente por falta de stock real)
	* ID2, real 700, (-300, comprometido 0)
	* ID3, real 100 (-700, comprometido 0, quedó en condición de stock bajo)
	5. Volver a la pantalla de envío de lotes. Verificar que no hay ventas cargadas, pero sí hay una venta pendiente (la de P1 2100) y que el botón está deshabilitado.
	6. Ir a la pantalla de pedidos a industrias. Comprobar que siguen siendo dos productos con stock bajo y uno sobrecomprometido luego de la actualización de la tabla de stock.
11) **Ir a la pantalla de carga de ventas. Día 2**
	1. Producto 1, cantidad 500 (stock real 1800, sumaría 500 al comprometido de 2100). Afectamos un producto de ventas pasadas
	2. Producto 2, cantidad 650 (stock real 700, lo dejaría comprometido en 650 y en condición de stock bajo). 
	3. Producto 3, cantidad 100 (stock real 100, lo dejaría en cero)
	4. Producto 5, cantidad 3000 (stock real 2500, lo dejaría cobrecomprometido y en condición pendiente)
	5. Producto 7, cantidad 3800 (stock real 4000, lo dejaría en condición de stock bajo)
	6. Ingrese un código de cliente y dirección de entrega distintas a las de las primeras dos ventas. Recuerdelas.
	7. Confirme el pedido.
	8. Vaya a la pantalla de envío de lotes, confirme ventas cargadas 1, pendientes 1
	9. Vaya a la pantalla de stock. Verá:
	* ID 1, real 1800, comprometido 2600. Negrita por sobrecomprometido
	* ID 2, real 700, comprometido 650. Negrita por stock bajo
	* ID 3, real 100, comprometido 100. Negrita por stock bajo/cero
	* ID 5, real 2500, comprometido 3000. Negrita por sobrecomprometido
	* ID 7, real 4000, comprometido 3800. Negrita por stock bajo
	10. Cambie las cantidades de reposición de cada producto mencionado con stock bajo a 20000, por decir un número
	10. Vaya a la pantalla de pedidos a industrias, verá stock bajo: 5, sobrecomprometido: 2. Haga el pedido a industrias. 
	* Verá en el historial y en la carpeta el pedido del dia 2:
	* P1;20000
	* P2;20000
	* P3;20000
	* P5;20000
	* P7;20000
	11. Vaya a la pantalla de stock y verifique que ninguno aparece en negrita por tener el stock pendiente de ingreso. Recepcione P1, P5 que son los sobrecomprometidos. Los otros pueden ser enviados todavía con el stock existente sin tener en cuenta el nuevo.
	12. Verifique que P1 y P5 tienen ahora sus columnas pendientes en 0, el real se ha sumado en +20000 (P1 21800, P5 22500) y los pendientes de los otros tres productos siguen ahí.
12) **Ir a la pantalla de carga de ventas nuevamente. Día 2**
	1. Producto 1, 100 (real 21800, comprometido quedará en 2700)
	2. Producto 5, 500 (real 22500, comprometido quedará en 3500)
	3. Producto 7, 500 (real 4000, comprometido quedará en 4300, pendiente 20000)
	4. Ingrese la venta bajo otro cliente y dirección
	5. Verifique en la pantalla de stock lo mencionado antes.
	6. Envíe a logística las dos ventas del día 2. Por tener stock real gracias al pedido a industrias saldrán las ventas pendientes del día anterior y las que en otro caso hubiesen quedado pendientes sin el pedido a industrias del día 2
	7. Verifique en el historial o en la carpeta de salida, las ventas R2 y R3 tienen distintas direcciones de entrega, pero R4 tiene la misma dirección que la venta R1, la del cliente original.
	8. Verifique en la pantalla de stock que los comprometidos han quedado en 0, P1 19100, P2 50, P3 0, P5 19000, P7 -300. 
	9. Recepcione el stock restante, dando como resultado P1 19100, P2 20050, P3 20000, P5 19000, P7 19700 (el stock ya estaba disponible, solo que ahora se refleja en el sistema)
13) **Vaya a la sección de recepción de pedidos de entrega** 
	1. Tome el archivo de lote según el historial del día 2, haga una copia, cambie su nombre (Entrega_Cxxx_L2.txt) y su contenido (R2 y R3 false, R4 true. Si agrega un R5 false/true aquí no hará diferencia porque no existe en el lote enviado) y reingrese el stock según fue explicado en la sección anterior. 
	2. Intente ingresar nuevamente este reporte. Le dará error por reingreso. 
	3. Intente ingresar el reporte `Entrega_C340_L643.txt` de ejemplo, que es válido y contiene referencias no entregadas. Dará error por no corresponder a un lote emitido por este sistema.
	4. Verifique en la pantalla de stock que 
	* P1 19100 -> 19700
	* P2 20050 -> 20700
	* P3 20000 -> 20100
	* P5 19000 -> 22500
	* P7 19700 -> 24000
	5. Acorde a las cantidades despachadas en el lote por ventas diarias y pendientes de reingresar R2 y R3, no R4 (en total: P1 +600, P2 +650, P3 +100, P5 +3500, P7 +4300)
14) **Vamos a hacer una venta. Día 3**
	1. Producto 9, 1800 (real 2100, comprometerá por 1800, condición de stock bajo)
	2. Producto 10, 7000 (real 6000, comprometerá por 1000, condición sobrecomprometida por 1000)
	3. Ingrese la venta con datos de un cliente nuevo.
	4. Envíe el lote del día 3. 
	* Naturalmente solo se envió el producto 9, el 10 necesita pedir stock. Tenemos dos productos en necesidad de pedido de stock
	5. Edite las cantidades a pedir para los productos 9 (1000) y 10 (1000).
	6. Haga el pedido y recepcione el stock para estos dos productos. No puede hacer un nuevo pedido sin antes recepcionar el stock entrante.
	7. Compruebe que la condición de stock bajo y sobrecomprometido continúa, vaya a la pantalla de pedidos y verá que puede seguir pidiendo (alternando entre esta pantalla y la de stock para comprobar el estado e ingresar el stock entrante) hasta que ambos productos solucionen su situación o cambie la cantidad a pedir por una mayor y resolver en menos pedidos el problema.
15) **Día 4**
	1. Cargue una nueva venta para poder despachar la pendiente P10 7000 junto a esta. Por ejemplo P6 2000

16) **Revisión final**
	1. Vea que ya no quedan ventas pendientes, ni ingresadas al lote diario (Día 4 fue el de la venta P6 2000 que envió lo pendiente), y no figura ningún producto con stock bajo o sobrecomprometido.


#### Esto debería haber cubierto:
- validaciones en los textboxes de entrada 
- todas las interacciones esperables entre cada pantalla y parte del programa
- manejo de productos enviados días anteriores, siguientes y con reingreso de stock por reportes y aumento por pedidos a industrias, manteniendo consistencia a medida que se opera
- recepción selectiva de los pedidos a industrias
- pedir y recepcionar stock entre lotes sin enviar (P1 - P7), y luego (P9, P10)
- edición de la cantidad a pedir a industrias tanto por exceso de la condición sobrecomprometida, como por debajo habilitando pedidos sucesivos en el mismo día recepción mediante
- buen funcionamiento de los historiales contra los archivos generados en la carpeta de salida 
- validaciones de los reportes tanto inválidos como válidos
- funcionalidad de estado actual al operar de lotes y de pedidos
- ingreso de productos por múltiples ventas/pedidos al lote diario

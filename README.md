# FCE - UBA | CAI 02/2019 | Grupo2
## Trabajo Práctico - Comercio

### Descripción

Esta app implementa la sección de comercio de los lineamientos del trabajo práctico.

![Descripción](https://i.imgur.com/v1CuPCd.png)

- Recibe pedidos resultantes de ventas mediante los canales de venta online en la pantalla `Cargar ventas a lote diario`. Carga manual.
- En base a las ventas confecciona un lote de bultos para enviar a la empresa de logística para su distribución desde la pantalla `Enviar lote diario a logística`. Confirmación manual diaria.
- Desde la pantalla `Reportes de entrega` recibe de la empresa de logística un reporte de entrega de donde se puede reingresar el stock de los pedidos no entregados.
- Administra el stock inicial del comercio (ventas cargadas suman stock comprometido, envíos via logística bajan el stock real en las cantidades comprometidas), pudiendo hacer pedidos a las industrias para reposición en cantidades fijas (que pueden ser modificadas) desde la pantalla `Pedidos stock` si el stock real cae por debajo del punto de reposición con suficiente stock comprometido, o mediante ventas y envíos. Confirmación manual diaria.
- Los archivos generados y aceptados se condicen con el formato definido en los lineamientos.

### Uso

Clonando el repositorio y compilando, se puede empezar a usar. 

La pantalla de bienvenida da una clara idea de la funcionalidad de cada sección, aparte de tener un panel de ayuda en cada una con una breve guía de uso. El flujo entre pantallas está ilustrado en el diagrama más arriba.

### Pruebas y comportamiento esperado

#### Archivos escenciales para el funcionamiento del programa:
- `Stock.txt` contiene el stock inicial que se cargará. Su formato es el de líneas de números enteros delimitados por separadores ";" del tipo `IDproducto;StockReal;PuntoReposicion;StockComprometido;StockPendiente`. Contiene diez líneas, por ende diez productos a cargar al sistema.

- `DatosComercio.txt` contiene la información del comercio a cargar. Posee una sola línea de items delimitados por separadores ";" del tipo `CodigoComercio;RazonSocial;CUIT;DireccionEntregaPedidosStock;DireccionDevolucionLotesVentas`. Está validado por cantidad de líneas (debe ser una sola), cantidad de elementos en la línea (deben ser 5), el código de comercio debe consistir de la letra C + un número positivo, y el CUIT se valida por ser un número entero positivo y tener 11 dígitos.

- `CantidadesReposicionStock.txt` contiene las cantidades fijas a pedir a industrias por stock bajo que se cargan al iniciar el programa.

Estos tres archivos se copian siempre a la carpeta del ejecutable.

Todos los archivos de salida como se muestran en el diagrama se guardan en la carpeta Grupo2 ubicada en la raíz de la unidad C. Si existe, borra el contenido (de la última ejecución), sino la crea.


#### Pantalla "Control de stock":
- Tabla de control de stock de solo lectura para el usuario, se llena al iniciar el programa desde el archivo "Stock.txt" y se va modificando a medida que se realizan transacciones en la aplicación, como confirmar un pedido o emitir un lote.
- Si el stock real de algún producto baja lo suficiente para caer por debajo del punto de reposición, se marcará en negrita y con más tamaño de letra para alertar al usuario de que tiene que hacer un pedido prontamente.
- La tabla de reposición indica el valor que se pide a fábrica cuando el stock de ese producto queda por debajo del punto de reposición. Los ID de producto son de solo lectura, los campos con las cantidades están limitados a 5 caracteres, solo se pueden ingresar números salvo el 0, y no se puede dejar vacía (queda marcado un error que no desaparece hasta que se llene con algo correcto).
- Tabla entregas pendientes de recepción que permite visualizar los productos que se pidieron a industria y están pendientes de recepción. Se deben tildar los pedidos que han ingresado y confirmarlos mediante el botón `Cargar pedido pendiente`. Solo se puede tildar dicho campo, los otros son de solo lectura.

#### Pantalla "Cargar ventas a lote diario":
- El botón "Agregar item al pedido" no se habilita si no hay un ingreso en el campo "Cantidad". Este campo solo permite números enteros positivos de hasta cuatro dígitos, sin espacios ni ningún otro tipo de caracter.
- El botón "Confirmar pedido" se habilita si se ingresó un código de cliente (solo permite números enteros positivos de hasta 5 dígitos), una dirección de entrega (solo caracteres alfanuméricos) y si se ingresó al menos un producto para la venta a cargar.

#### Pantalla "Enviar lote diario a logística"
- No permite usar el botón "Enviar lote a logística" si no se ha cargado al menos un pedido anteriormente.
- Todos los campos y vistas previas en esta pantalla son de solo lectura, a modo informativo de los datos cargados desde "DatosComercio.txt" y del lote generado.

#### Pantalla "Pedidos Stock"
- No se habilita el botón "Confirmar Pedido de Stock a industrias" si no hay al menos un producto que tenga su stock por debajo del punto de reposición en la pantalla de stock. Se deshabilita en cuanto se hace el pedido a industrias. Se vuelve a habilitar si con sucesivas ventas y envíos a logística vuelve a caer el stock de algún producto.
- Permite encargar stock de un producto que ha comprometido el suficiente stock para bajar el real por debajo del punto de reposición, sin antes haberlo despachado via logística impactando en el real.
- Todos los campos y vistas previas en esta pantalla son de solo lectura, a modo informativo de los datos cargados desde "DatosComercio.txt" y del pedido generado.


#### Pantalla "Reportes de entrega":
- No se puede apretar el botón "Cargar stock de no entregados" si no se carga un reporte primero.

El archivo de entrada de logística está validado por contenido, por extensión y por nombre. En particular, de los archivos de prueba/ejemplo disponibles en la carpeta `EjemplosReportes`:
- `Entrega_C340_L643.txt` es aceptado, tiene su nombre y su contenido según el formato `[CodReferencia];[true/false]` (un juego por línea) acorde al archivo modelo en los lineamientos
- `Entrega_C332_L116.txt` falla por no respetar el separador ; entre código de referencia y estado del envío
- `Entrega_C332_L115.txt` contiene una frase que no coincide con el formato esperado
- Cualquier otro archivo con otro formato de nombre (sea su contenido válido como el del primero, o no) no es aceptado
- Cualquier otro archivo al que se le haya asignado un nombre correcto siendo cualquier otra cosa su contenido, no es aceptado

Una vez que haya cargado un archivo válido, se van a mostrar en ambos lados de la pantalla los pedidos enviados y no enviados pertenecientes a este reporte. Ahora puede reingresar los pedidos que no hayan sido entregados al stock real. 

Para probar esta funcionalidad:
1) Cargue algunas ventas en el sistema. 
2) Envíe la/s ventas que haya cargado. Necesita enviar al menos un lote con una venta (es decir, tener al menos un número de referencia). Revise el estado del stock antes de seguir.
3) En la carpeta de salida, haga una copia del archivo `Lote_(CodCliente)_(CodLote)_.txt` y cambie su nombre al formato esperado correspondiente, `Entrega_(CodCliente)_(CodLote)_.txt` usando el mismo código de cliente y el mismo código de lote.
4) Cambie el contenido del archivo para reflejar el estado de los números de referencia de los pedidos que se han entregado en el lote. Por ejemplo si su archivo de lote tiene un pedido con referencia `R1`, el contenido respectivo en el archivo de Entrega debe ser `R1;false` para denotar que no fue entregado, o `R1;true` para decir que fue entregado. Un número de referencia por renglón, como dice el archivo de ejemplo en los lineamientos.
5) Ingrese este archivo en la pantalla y luego apriete "Cargar stock de no entregados". Revise el estado del stock, verá las cantidades vendidas reingresadas al stock real y habiendo vuelto al estado anterior.

Validaciones de esta funcionalidad:

- Solo reingresa reportes que hayan sido emitidos por la aplicación (es decir, en base a lotes a logística que existan en la carpeta de salida)
- No permite reingresar más de una vez el mismo reporte
- No permite presionar el botón de reingresar stock si se cargó un reporte cualquiera, si no se emitió al menos un lote desde la aplicación

### Varios

Naturalmente es trabajo en progreso y es un concepto, no debería ser usado por ningún negocio real (momentaneamente)


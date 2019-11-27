# FCE - UBA | CAI 02/2019 | Grupo2
## Trabajo Práctico - Comercio

### Descripción

Esta app implementa la sección de comercio de los lineamientos del trabajo práctico.

![Descripción](https://i.imgur.com/Avfl25x.png)

- Recibe pedidos resultantes de ventas mediante los canales de venta online. Carga manual.
- En base a las ventas confecciona un lote de bultos para enviar a la empresa de logística para su distribución. Confirmación manual diaria.
- Recibe de la empresa de logística un reporte de entrega de donde se puede reingresar el stock de los pedidos no entregados.
- Administra el stock del comercio, haciendo pedidos a las industrias para reposición. Confirmación manual diaria.
- Los archivos generados tienen el formato definido en los lineamientos.

### Uso

Clonando el repositorio y compilando, se puede empezar a usar. 

La pantalla de bienvenida da una clara idea de la funcionalidad de cada sección, aparte de tener un panel de ayuda en cada una con una breve guía de uso. El flujo entre pantallas está ilustrado en el diagrama más arriba.

Viene con un stock inicial predefinido (Stock.txt), los datos del comercio a cargar (DatosComercio.txt) y archivos de prueba para la funcionalidad de entrada en reportes de entrega. Todos estos archivos se encuentran junto al archivo de solución. Los necesarios para el funcionamiento del programa compilado (stock y datos de comercio) se copian automáticamente a la carpeta \bin\debug.

Todos los archivos de salida como se muestran en el diagrama se guardan en la carpeta Grupo2 ubicada en la raíz de la unidad C. Es creada por el programa si no existe.

### Pruebas

Validaciones implementadas en cuanto a la interfaz de usuario y archivos necesarios:

#### Pantalla "Control de stock":
- Tabla de control de stock de solo lectura para el usuario, se llena al iniciar el programa desde el archivo "Stock.txt"

#### Pantalla "Cargar ventas a lote diario":
- El botón "Agregar item al pedido" no se habilita si no hay un ingreso en el campo "Cantidad". Este campo solo permite números enteros positivos de hasta seis dígitos, sin espacios ni ningún otro tipo de caracter
- El botón "Confirmar pedido" se habilita si se ingresó un código de cliente (solo permite números enteros positivos de hasta 5 dígitos), una dirección de entrega (solo caracteres alfanuméricos) y si se ingresó al menos un producto para la venta a cargar

#### Pantalla "Enviar lote diario a logística"
- No permite usar el botón "Enviar lote a logística" si no se ha cargado al menos un pedido anteriormente
- Todos los campos y vistas previas en esta pantalla son de solo lectura, a modo informativo de los datos cargados desde "DatosComercio.txt" y del lote generado

#### Pantalla "Pedidos Stock"
- Todos los campos y vistas previas en esta pantalla son de solo lectura, a modo informativo de los datos cargados desde "DatosComercio.txt" y del pedido generado

#### Pantalla "Reportes de entrega":
- No se puede apretar el botón "Cargar stock de no entregados" si no se carga un reporte primero.

El archivo de entrada de logística está validado por contenido, por extensión y por nombre. En particular, de los archivos de prueba/ejemplo:
- "Entrega_C340_L643.txt" es aceptado, tiene su nombre y su contenido según el formato [CodReferencia];[true/false] (un par por línea) acorde al archivo modelo en los lineamientos
- "Entrega_C332_L116.txt" falla por no respetar el separador ; entre código de referencia y estado del envío
- "Entrega_C332_L115.txt" contiene una frase que no coincide con el formato esperado
- Cualquier otro archivo con otro formato de nombre (sea su contenido válido como el del primero, o no) no es aceptado
- Cualquier otro archivo al que se le haya asignado un nombre correcto siendo cualquier otra cosa su contenido, no es aceptado

Para probar esta funcionalidad, cargue un pedido o dos. De esta forma tendrá al menos un código de referencia R0 y R1. Luego cree un archivo de texto con el formato descripto marcando R0 o R1 o ambos como no entregados (false). Los productos despachados para estos pedidos deberían volver al stock real y aumentarlo.


### Varios

Naturalmente es trabajo en progreso y es un concepto, no debería ser usado por ningún negocio real (momentaneamente)


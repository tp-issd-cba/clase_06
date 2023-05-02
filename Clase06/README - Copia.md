# Instituto Superior Santo Domingo 


## Clase 06: Control GridView.


### Objetivos:
- Utilizar el control GridView para visualizar datos.
- Edición, modificación y consulta de datos a partir de un GridView.
- Configuración del control GridView.

### Menu
- [Introducción](#introduccion)
- [GridView - Datos de una tabla](#gridview-datos-de-una-tabla)
- [Formato automático](#formato-automatico)
- [Propiedades](#propiedades)
- [Configuración inicial para Grid View](#configuracion-inicial-para-grid-view)
- [Propiedades de Grid View](#propiedades-de-grid-view)
- [Formato condicional en filas](#formato-condicional-en-filas)
- [Selección y extracción de datos](#seleccion-y-extraccion-de-datos)

### Introducción
El control GridView del ASP.Net permite visualizar datos en una tabla en pantalla, editar, modificar y borrar registros del mismo.

El GridView es un control extremadamente flexible para mostrar tablas multicolumna. Cada registro de una consulta del un select configurado en un SqlDataSource genera una fila en la grilla. Cada campo en el registro representa una columna en la grilla.

El GridView es el control más poderoso que provee el ASP.Net. Veremos que este control trae funcionalidades ya implementadas para paginación, ordenamiento y edición de sus datos.

### GridView - Datos de una tabla
Crearemos un proyecto para probar el control GridView y las diferentes opciones que nos brinda. Luego de crear el proyecto iremos al Explorador de servidores y seleccionaremos la tabla “rubros” y la arrastraremos al formulario web. Veremos que se generan dos objetos sobre la página:
- Un objeto de la clase GridView llamado GridView1.
- Un objeto de la clase SqlDataSource llamado SqlDataSource1.

Si seleccionamos el objeto SqlDataSource1 y observamos el contenido de la propiedad SelectQuery, veremos que ya está configurado el comando SELECT:
```
SELECT [codigo], [descripcion] FROM [rubros]
```
El comando SELECT indica rescatar todas las filas de la tabla rubros.

Podemos ver también que se han configurado automáticamente las propiedades InsertQuery, DeleteQuery y UpdateQuery con los valores:
```
INSERT INTO [rubros] ([descripcion]) VALUES (@descripcion)

DELETE FROM [rubros] WHERE [codigo] = @codigo

UPDATE [rubros] SET [descripcion] = @descripcion WHERE [codigo] = @codigo
```

Como podemos ver hasta este momento la herramienta Visual Studio .Net nos ha configurado en forma automática el control SqlDataSource1, solo nos queda configurar el control GridView1.

Seleccionamos el control GridView y presionamos el botón presente en la parte superior derecha, el mismo nos muestra una serie del funcionalidades básicas del control:

Como podemos ver ya está configurado el origen de datos con el objeto SqlDataSource1. Habilitemos la paginación, ordenamiento, edición y eliminación.

Ejecutemos el proyecto y comprobaremos que tenemos en la página los datos de la tabla “rubros” con la capacidad de modificar y borrar registros. Además está activa la paginación y ordenamiento por cualquiera de las dos columnas de la tabla.

Sin escribir una sola línea de código tenemos el mantenimiento de la tabla rubros (con la excepción del alta)

## Otras características del control GridView


### Formato automático

Para definir la presentación de la tabla con plantillas predefinidas de color y fuente, podemos utilizar la opción "Formato Automático" desde el botón ">" que se encuentra en la parte superior derecha del control `GridView`. Una vez seleccionado el esquema, presionamos aceptar y se definirá el nuevo formato de la grilla.

### Propiedades

Desde la ventana de propiedades podemos configurar otras propiedades, como:

- `Caption`: Es un título que aparece en la parte superior del `GridView`.
- `PageSize`: Cantidad de registros a mostrar por página.

### Uso con SqlDataSource

### Tablas

El proyecto utiliza dos tablas: `articulos` y `rubros`. La tabla `articulos` tiene los siguientes campos:
- `codigo` (int): Clave primaria e identidad.
- `descripcion` (varchar(50))
- `precio` (float)
- `codigorubro` (int)

La tabla `rubros` tiene los siguientes campos:
- `codigo` (int): Clave primaria e identidad
- `descripción` (varchar(50))

## Configuración inicial para Grid View

Para mostrar los datos, se genera un nuevo webform y se selecciona desde el Explorador de servidores la tabla `articulos` y se dispone dentro del webform. El entorno del Visual Studio .Net genera un objeto de la clase `GridView` y otro de la clase `SqlDataSource`.

1. Seleccionar el control `SqlDataSource1` y configurar la propiedad `SelectQuery` con el siguiente comando `SELECT ar.codigo, ar.descripcion as descriarticulo, precio, ru.descripcion as descrirubro from articulos as ar join rubros as ru on ru.codigo=ar.codigorubro`. Este comando rescata los datos haciendo el emparejamiento por la columna `codigorubro` de la tabla `articulos` y `codigo` de la tabla `rubros`.
2. Actualizar el esquema del `SqlDataSource1` seleccionando el objeto sobre el formulario y seleccionando la opción "Actualizar esquema". Con esto se logra que se refresquen las columnas a mostrar en el `GridView1`.

### Propiedades de Grid View

Para dar un formato y presentación más adecuado de los datos, se realizan los siguientes pasos:

1. Seleccionar el `GridView1` y mediante la opción "Formato automático...", se define el estilo de presentación de la grilla.
2. Seleccionar la opción "Editar columnas..." y seleccionar el campo a configurar. Cambiar los títulos de las columnas de cada campo (por ejemplo en el campo descriarticulo mostraremos el título “Descripción del Art.” Modificando la propiedad `HeaderText`.
3. Configurar la propiedad `Visible` de cada columna para mostrar o no la columna.
4. Configurar la propiedad `DataFormatString` para cambiar la apariencia de números y fechas.

Algunos ejemplos de formato de campos:

| Tipo | Formato | Ejemplo |
|------|---------|---------|
| Moneda | `{0:C}` | $120.50 |
| Porcentaje | `{0:P}` | 40% |
| Decimales fijos | `{0:F4}` | 12.4567 |
| Notación científica | `{0:E}` | 1.233E003 |
| Fecha corta | `{0:d}` | 12/25/2005 |

### Formato condicional en filas

Cuando se grafica una tabla, es posible capturar el evento `RowDataBound` y configurar cómo se debe graficar dicha fila de la tabla. A modo de ejemplo, podemos mostrar de color amarillo las filas de los artículos con precio superior a 5. Para lograr esto, codificamos el evento `RowDataBound` del `GridView1`:

```
protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow)
    {
        double precio;
        precio = (double)DataBinder.Eval(e.Row.DataItem, "precio");
        if (precio > 5)
        {
            e.Row.ForeColor = System.Drawing.Color.Red;
            e.Row.BackColor = System.Drawing.Color.Yellow;
            e.Row.Font.Bold = true;
        }
    }
}
```

Con el `if`, verificamos si el evento se disparó para una fila de datos de la grilla (ya que este método se dispara cuando dibuja la cabecera (`DataControlRowType.Header`), el pie de grilla (`DataControlRowType.Footer`), etc.). Luego, rescatamos el valor del campo precio y verificamos con un nuevo `if` si el precio supera 5. En caso afirmativo, modificamos el color de fondo (`BackColor`) y de frente de la fila.

### Selección y extracción de datos.

En muchas situaciones, es necesario que el usuario seleccione una fila de la grilla para reflejar dicho dato en otra parte de la página o hacer otra consulta. Para poder implementar esta característica del `GridView`, llevaremos a cabo los siguientes pasos:

1. Cambiaremos el valor de la propiedad `SelectedRowStyle.BackColor` por amarillo (es decir, que cuando seleccionemos la fila, el color de fondo de la misma se activará con este valor).
2. En el menú de opciones que se despliega en la parte derecha del `GridView1`, activaremos el `CheckBox` "Habilitar selección".
3. Dispondremos una `Label` en el webform para mostrar el valor seleccionado de la grilla (solo a modo de ejemplo).
4. Para el evento `SelectedIndexChanged` del `GridView1`, codificaremos el siguiente código:

```
protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
{
    this.Label1.Text = this.GridView1.Rows[this.GridView1.SelectedIndex].Cells[1].Text;
}
```

El objeto `Rows` del `GridView` almacena una colección de filas. Mediante el valor devuelto por la propiedad `SelectedIndex` de la grilla, podemos acceder a la celda que almacena el código del artículo. Esta información nos es muy útil para mostrar información adicional sobre el registro en otro control, por ejemplo.nder
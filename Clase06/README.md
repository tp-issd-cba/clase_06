# Clase 06

## Grid view

###  **Setup inicial**

#### Tabla simple

#### Tabla con relacion

### **Propiedades**

#### Paginado, orden, edicion, eliminacion

#### Formato automatico

#### Titulo de tabla (Caption)

#### Registros por pagina 

#### Configuracion por columnas 

#### Formato de campos

#### Formato condicional en fila

#### Selección de una fila del GridView 

#### Usar un dato de la fila seleccionada

```
protected void GridView1_SelectedIndexChanged(object sender, EventArgse)
{
	Label1.Text =
this.GridView1.Rows[this.GridView1.SelectedIndex].Cells[1].Text;
}
```
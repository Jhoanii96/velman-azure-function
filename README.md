# Velman Azure Functions

Coleccion de Azure Functions HTTP para conversion de archivos, integracion con FastField Forms y sincronizacion de datos Komatsu con Azure SQL.

El proyecto usa .NET 8, Azure Functions v4 en modo aislado. Las funciones estan pensadas para ser invocadas desde actividades Web de Azure Data Factory o desde cualquier cliente HTTP.

## Funciones

| Funcion | Metodo | Nivel de acceso | Entrada | Resultado |
|---|---|---|---|---|
| `ConvertToWord` | GET/POST | Anonimo | Markdown plano en el cuerpo de la solicitud | JSON con `fileUrl`, `fileName` y `contentType`; el archivo `.docx` se guarda en el contenedor `reports` de Azure Blob Storage |
| `ConvertToWordFile` | POST | Anonimo | Markdown plano en el cuerpo de la solicitud | Archivo `.docx` directamente como descarga |
| `ExcelToPdf` | POST | Function | JSON `{ "fileContent": "<base64>" }` | JSON con el PDF en Base64 |
| `ExcelToText` | POST | Function | JSON `{ "fileContent": "<base64>" }` | Texto plano con el nombre y las filas de cada hoja |
| `FastFieldGetSchema` | GET/POST | Anonimo | Parametro `form_name` y encabezado `schema` opcional | Busca el formulario en FastField y crea o completa las tablas SQL asociadas a su esquema |
| `FastFieldInsertData` | GET/POST | Anonimo | Parametro `FormName`; encabezados `Schema` y `DaysAgo` opcionales | Descarga envios de FastField e inserta o combina los datos en SQL Server |
| `MineraAndinaSol` | GET/POST | Anonimo | Encabezados con las rutas, credenciales y parametros del origen/destino | Obtiene equipos, series de tiempo, ubicaciones, combustible, horas, fallas y envia los datos a SQL Server |

## Parametros de `FastFieldGetSchema`

- Consulta: `form_name`: nombre exacto del formulario de FastField.
- Encabezado opcional: `schema`: esquema de destino en SQL Server. El valor predeterminado es `dbo`.

Ejemplo:

```text
GET https://<function-app>.azurewebsites.net/api/FastFieldGetSchema?form_name=<nombre>
schema: dbo
```

## Parametros de `FastFieldInsertData`

- Consulta: `FormName`: nombre exacto del formulario.
- Encabezado opcional: `Schema`: esquema de destino; predeterminado `dbo`.
- Encabezado opcional: `DaysAgo`: numero entero mayor o igual a `-1`.
  - `-1` descarga todos los registros historicos.
  - `0` o un numero positivo limita la descarga a los dias indicados.

Ejemplo:

```text
POST https://<function-app>.azurewebsites.net/api/FastFieldInsertData?FormName=<nombre>
Schema: dbo
DaysAgo: 30
```

## Parametros de `MineraAndinaSol`

Los encabezados siguientes pueden enviarse desde Azure Data Factory. Si no se envian, la funcion usa los valores equivalentes de la configuracion de la Function App o `local.settings.json`.

| Encabezado | Configuracion local equivalente | Uso |
|---|---|---|
| `SqlServername` | `AzuSQLServerName` | Servidor SQL |
| `SqlDatabase` | `AzuSQLDatabase` | Base de datos SQL |
| `SqlLogin` | `AzuSQLUser` | Usuario SQL |
| `SqlPassword` | `AzuSQLSecret` | Contrasena SQL |
| `UrlRoot` | `KomatsuUrlRoot` | URL inicial de la API Komatsu |
| `UrlBaseTimeSeries` | `KomatsuUrlBaseTimeSeries` | URL base para series de tiempo |
| `KomatsuId` | `KomatsuId` | Identificador Komatsu |
| `SchemaTable` | `KomatsuSchema` | Esquema SQL; predeterminado `kom` |
| `StartDate` | Ninguna | Primera fecha a consultar, formato `yyyy-MM-dd` |
| `EndDate` | Ninguna | Ultima fecha; si esta vacia usa la fecha actual |
| `TokenUrl` | `KomatsuTokenUrl` | URL de obtencion del token |
| `Username` | `KomatsuUsername` | Usuario OAuth |
| `Password` | `KomatsuPassword` | Contrasena OAuth |

La funcion solicita el token OAuth, obtiene el inventario de equipos y consulta las relaciones disponibles de cada equipo. Luego combina los datos de ubicaciones, horas operativas, combustible, relacion de combustible restante, horas improductivas e fallas en SQL Server.

## Configuracion local

1. Copie [local.settings.example.json](local.settings.example.json) como `local.settings.json`.
2. Sustituya todos los placeholders por valores reales.
3. No agregue credenciales ni cadenas de conexion al codigo fuente.
4. Ejecute `func start` desde la raiz del proyecto.

El almacenamiento local predeterminado usa `UseDevelopmentStorage=true`. Para pruebas con Blob Storage real configure `AzureWebJobsStorage` con una cadena de conexion administrada de manera segura.

## Configuracion en Azure

Configure estas claves en **Function App > Configuracion > Configuracion de la aplicacion**:

- `AzureWebJobsStorage`
- `FastFieldUser`
- `FastFieldPassword`
- `FastFieldAPIKey`
- `AzuSQLServerName`
- `AzuSQLDatabase`
- `AzuSQLUser`
- `AzuSQLSecret`
- `KomatsuUsername`
- `KomatsuPassword`
- `KomatsuTokenUrl`
- `KomatsuUrlRoot`
- `KomatsuUrlBaseTimeSeries`
- `KomatsuId`
- `KomatsuSchema`

Para la seguridad de produccion se recomienda:

- Crear las credenciales como secretos de Azure Key Vault y referenciarlos desde la configuracion de la Function App.
- Habilitar Managed Identity y limitar el acceso a los servicios necesarios.
- Cambiar desde el nivel `Anonymous` a `Function` o `Admin` las funciones que manejan datos sensibles.
- Crear secretos nuevos y revocar los anteriores si alguna credencial fue expuesta antes de esta revision.
- Activar HTTPS y restringir los orígenes de red cuando sea posible.
- Habilitar Application Insights para monitoreo.

## Integracion desde Azure Data Factory

Para cada funcion use una actividad **Web**:

1. Configure **URL** con `https://<function-app>.azurewebsites.net/api/<nombre-de-funcion>`.
2. Seleccione el metodo indicado en la tabla de funciones.
3. Para `ExcelToPdf` y `ExcelToText`, envíe JSON con el contenido Base64.
4. Para `ConvertToWord` y `ConvertToWordFile`, configure `Content-Type: text/plain` o `text/markdown` y envíe Markdown en el cuerpo.
5. Para `FastFieldGetSchema` y `FastFieldInsertData`, configure las consultas y encabezados necesarios.
6. Para `MineraAndinaSol`, configure los encabezados de origen, destino y OAuth.
7. En Azure Data Factory use Linked Services y Azure Key Vault para referencias de secretos; no escriba contrasenas visibles en los datasets o tuberias.

Las funciones con nivel `Function` requieren pasar la clave de funcion como parametro de consulta `code` o en el encabezado `x-functions-key`. Las demas pueden invocarse sin clave.

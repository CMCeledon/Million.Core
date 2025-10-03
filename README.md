# Autor CARLOS MARIO CELEDÓN RODELO:
Desarrollado por: 
[![www.cmceledon.com](https://www.cmceledon.com/Recursos/assets/img/vegas-logo.png)](https://www.cmceledon.com/)

- 🔗 `www.cmceledon.com`
----
# Sumérgete en la nueva era digital
```sh
- CMCELEDON S.A.S es una empresa de tecnología que desarrolla sistema de información. 

Contribuimos a la transformación digital de las empresas.

DESARROLLO DE SOFTWARE

Abordamos todas las fases del ciclo de vida de cualquier tipo de sistemas de información. Software a la medida.

MISIÓN
Crear herramientas digitales para resolver problemas de gestión en las organizaciones y en la sociedad,
procurando generar participación, competitividad e interconexión de las personas, las empresas y sus entornos.
- 
```
---

### Notas importantes (Desarrollo y RoadMap):
- ✔️ **FrontEnd**: React - Desplegado! -> Ver en: 🔗 https://million.cmceledon.com/ 
- ✔️ propertyService.js
- ✔️ Método 1: fetchPagedProperties
- ✔️ Método 2: fetchPropertyDetail
- ✔️ Buenas prácticas
  -  --
- ✔️ **Backend**: .Net 9 - Desplegado! -> Ver en: 🔗 https://apimillion.cmceledon.com/ 
- ✔️ Patrón de Diseño: Arquitectura DDD
- ✔️ Servicio Web Api
- ✔️ Encapsulamiento
- ✔️ Automaper (librerías de mapeo)
- ✔️ Clases Base y también interfaz
- ✔️ Estandarización de respuestas
- ✔️ Inyección de dependencia
- ✔️ Documentación para la Api (Swagger)
- ✔️ Buenas prácticas
## RoadMap de Desarrollo y Buenas Prácticas

| Categoría | Tecnología/Práctica | Estado | Beneficio Clave |
| :--- | :--- | :--- | :--- |
| **FRONTEND (React)** | | | |
| Estructura y Componentes | ReactJS, Hooks, Componentes Funcionales | ✔️ | Reutilización y manejo eficiente del estado. |
| URL Base Centralizada | Variable BASE_URL en propertyService.js | ✔️ | Facilita el cambio entre entornos (Dev/Prod). |
| Tipado JSDoc | Tipado de PropertyFilters, Property, PagedResponse | ✔️ | Mejora la mantenibilidad y la detección de errores en JavaScript. |
| Diseño Profesional | Tailwind CSS, Dark Theme, Diseño Full Screen | ✔️ | Experiencia de usuario fluida y adaptable (responsive). |
| propertyService.js | Método: fetchPagedProperties (POST) | ✔️ | Consumo correcto del endpoint de búsqueda paginada. |
| propertyService.js | Método: fetchPropertyDetail (GET) | ✔️ | Consumo del endpoint para detalles individuales. |
| **Backend: .NET 9** | | | |
| Arquitectura | Patrón de Diseño: Arquitectura DDD (Layers) | ✔️ | Separación clara de responsabilidades y enfoque en el dominio. |
| Persistencia | **MongoDB.Driver** (Driver Oficial) | ✔️ | Mayor rendimiento y eficiencia nativa con MongoDB. |
| Control de Flujo | Inyección de Dependencia (builder.Services.AddScoped) | ✔️ | Código desacoplado y facilidad para realizar pruebas (Moq). |
| Mapeo | AutoMapper (AutoMapperConfig.Initialize) | ✔️ | Elimina código repetitivo (boilerplate) y centraliza la lógica de conversión. |
| Comunicación | Estandarización de respuestas (ResponseServices<T>) | ✔️ | Consistencia en la respuesta de la API para el frontend. |
| Documentación | Documentación para la Api (Swagger/OpenAPI) | ✔️ | Facilita la exploración y el uso de la API por parte de clientes. |

  -  --
  
  # Diseño de Base de Datos para Propiedades Inmobiliarias

Este documento describe el diseño de la base de datos MongoDB para un sistema de gestión de propiedades inmobiliarias. Se utilizan dos colecciones principales: **Properties** (colección central con incrustaciones) y **Owners** (colección de referencia).

## 1. Colección Principal: Properties

Esta será la colección central, donde incrustaremos las imágenes y las trazas.

| Campo       | Tipo              | Notas                                                                 |
|-------------|-------------------|-----------------------------------------------------------------------|
| _id        | ObjectId         | PK (Clave Primaria automática de MongoDB).                            |
| Name       | String           | Nombre de la Propiedad.                                               |
| Address    | String           | Dirección de la Propiedad.                                            |
| Price      | Decimal          | Precio.                                                               |
| CodeInternal | String        | Código interno.                                                       |
| Year       | Int32            | Año de construcción/registro.                                         |
| IdOwner    | ObjectId         | (Referencia/FK): ID del propietario de la colección Owners.           |
| Images     | Array of Documents | (Incrustación/Embedding): Lista de objetos de PropertyImage.         |
| Traces     | Array of Documents | (Incrustación/Embedding): Lista de objetos de PropertyTrace.         |

### Estructura del Documento Property (Ejemplo)

```json
{
  "_id": ObjectId("..."),
  "Name": "Casa Bonita Centro",
  "Address": "Calle 10 # 5-20",
  "Price": 250000.00,
  "CodeInternal": "CB-001-2025",
  "Year": 2005,
  "IdOwner": ObjectId("60d5ec49b14f826071f4672e"), // Referencia al Owner
  "Images": [
    {
      "file": "house_front.jpg",
      "Enabled": true
    },
    {
      "file": "house_back.jpg",
      "Enabled": false
    }
  ],
  "Traces": [
    {
      "DateSale": ISODate("2024-06-01T10:00:00Z"),
      "Name": "Evaluación Inicial",
      "Value": 250000.00,
      "Tax": 0
    }
    // ... más trazas
  ]
}
```

## 2. Colección de Referencia: Owners

Esta colección almacena la información del propietario. Usamos su `_id` para referenciarlo desde la colección Properties.

| Campo     | Tipo    | Notas                                                                 |
|-----------|---------|-----------------------------------------------------------------------|
| _id      | ObjectId | PK (Clave Primaria automática de MongoDB).                            |
| Name     | String  | Nombre del Propietario.                                               |
| Address  | String  | Dirección.                                                            |
| Photo    | String  | Ruta o Binario (depende de tu necesidad, String para ruta es común).  |
| Birthday | Date    | Fecha de nacimiento.                                                  |

### Ejemplo de Documento Owner

```json
{
  "_id": ObjectId("60d5ec49b14f826071f4672e"),
  "Name": "Carlos Mendoza",
  "Address": "Carrera 5 # 22-01",
  "Photo": "owner_carlos.png",
  "Birthday": ISODate("1985-11-15T00:00:00Z")
}
```

## ⚠️ ¿Por qué usar Incrustación (Embedding) para Imágenes y Trazas?

La incrustación es ideal en este caso porque:

- **Imágenes (PropertyImage)**: Una imagen es un dato que siempre está ligado a una propiedad específica y no se consulta por separado. Incrustarlas reduce el número de consultas necesarias (solo una lectura para obtener la Propiedad y sus imágenes).

- **Trazas (PropertyTrace)**: Aunque podrían crecer, si la cantidad de trazas por propiedad es razonable (no miles), incrustarlas mantiene la atomicidad y la coherencia de los datos de la propiedad en un solo documento.
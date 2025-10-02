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
  
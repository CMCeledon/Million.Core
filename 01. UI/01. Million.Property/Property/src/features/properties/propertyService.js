// Define la interfaz de la respuesta estandarizada de la API
// T es el tipo de dato real que viene en el campo 'info'
/**
 * @typedef {object} ResponseServices
 * @property {boolean} state
 * @property {string} message
 * @property {T | null} info
 * @property {string} [type]
 * @property {string} [warning]
 * @property {string} [transactionId]
 * @template T
 */

// Define la estructura de datos para una sola propiedad (basada en PropertyDto)
/**
 * @typedef {object} Property
 * @property {string} id
 * @property {string} name
 * @property {string} address
 * @property {number} price
 * @property {number} year
 * @property {string} codeInternal
 * @property {string} idOwner
 * @property {PropertyImage[]} images
 * @property {PropertyTrace[]} traces
 */

/**
 * @typedef {object} PagedResponse
 * @property {Property[]} data
 * @property {number} pageNumber
 * @property {number} pageSize
 * @property {number} totalItems
 * @property {number} totalPages
 */

/**
 * @typedef {object} PropertyFilters
 * @property {string} [name]
 * @property {string} [address]
 * @property {number} [minPrice]
 * @property {number} [maxPrice]
 * @property {number} [pageNumber=1]
 * @property {number} [pageSize=12]
 */

// --- D E F I N I C I Ó N   C E N T R A L   D E   L A   U R L ---
const BASE_URL = 'http://localhost:5000/api/property';
// ----------------------------------------------------------------

// --- UTILITY FUNCTIONS ---

/**
 * Función genérica para manejar la respuesta HTTP y el ResponseServices<T>.
 * @template T
 * @param {Response} response
 * @returns {Promise<T>}
 */
const handleApiResponse = async (response) => {
    if (!response.ok) {
        throw new Error('Error de red al conectar con la API.');
    }
    
    /** @type {ResponseServices<T>} */
    const apiResponse = await response.json();
    
    if (!apiResponse.state) {
        const errorMsg = apiResponse.message || apiResponse.warning || 'Operación fallida por lógica de negocio.';
        throw new Error(errorMsg);
    }

    return apiResponse.info; 
};

// --- API SERVICE FUNCTIONS ---

/**
 * Obtiene la lista COMPLETA de propiedades (sin paginación ni filtros).
 * @returns {Promise<Property[]>}
 */
export const fetchProperties = async () => {
  // Uso de la variable central: BASE_URL + /getAllPropertiesAsync
  const url = `${BASE_URL}/getAllPropertiesAsync`;
  const response = await fetch(url);
  
  return handleApiResponse(response); 
};


/**
 * Obtiene una lista paginada de propiedades con filtros usando el método POST.
 * @param {PropertyFilters} filters
 * @returns {Promise<PagedResponse>}
 */
export const fetchPagedProperties = async (filters) => {
  // Uso de la variable central: BASE_URL + /getPagedAsync
  const url = `${BASE_URL}/getPagedAsync`;
  
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    },
    body: JSON.stringify(filters), 
  });
  
  return handleApiResponse(response); 
};

/**
 * Fetches detail for a single property.
 * @param {string} id
 * @returns {Promise<Property>}
 */
export const fetchPropertyDetail = async (id) => {
  // Uso de la variable central: BASE_URL + / + id
  const url = `${BASE_URL}/${id}`; 
  const response = await fetch(url);

  return handleApiResponse(response); 
};
using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private const string CollectionName = "Properties";
        // IMongoCollection tipado para esta clase, para consultas más avanzadas
        private readonly IMongoCollection<Property> _propertiesCollection;

        public PropertyRepository()
        {
            // Inicializa la colección para consultas directas y LINQ
            _propertiesCollection = MongoDBHelper.Instance.GetCollection<Property>(CollectionName);
        }

        /// <summary>
        /// Obtiene todas las propiedades.
        /// </summary>
        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await MongoDBHelper.Instance.GetAll<Property>(CollectionName);
        }

        /// <summary>
        /// Obtiene una propiedad por su ID.
        /// </summary>
        public async Task<List<Property>> GetPropertiesByOwnerAsync(string ownerId)
        {
            // Crea un filtro: IdOwner es igual al ObjectId del dueño.
            var filter = Builders<Property>.Filter.Eq(p => p.IdOwner, ownerId);

            // Ejecuta la consulta directamente sobre la colección tipada
            return await _propertiesCollection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Obtiene todas las propiedades con un precio mayor o igual al valor dado.
        /// </summary>
        public async Task<List<Property>> GetPropertiesByMinPriceAsync(decimal minPrice)
        {
            // Crea un filtro: Price es mayor o igual al precio mínimo.
            var filter = Builders<Property>.Filter.Gte(p => p.Price, minPrice);

            return await _propertiesCollection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Crea una nueva propiedad.
        /// </summary>
        public async Task<string> CreatePropertyAsync(Property newProperty)
        {
            return await MongoDBHelper.Instance.InsertOne(CollectionName, newProperty);
        }

        public async Task<IEnumerable<Property>> SearchAsync(string name, string address, decimal? minPrice, decimal? maxPrice)
        {
            var filters = new List<FilterDefinition<Property>>();

            // 1. Filtro por Nombre (Búsqueda parcial, insensible a mayúsculas)
            if (!string.IsNullOrEmpty(name))
            {
                // Usa un Regex para buscar 'name' en el campo 'Name' (equivalente a SQL LIKE '%name%')
                filters.Add(Builders<Property>.Filter.Regex(p => p.Name, new BsonRegularExpression(name, "i")));
            }

            // 2. Filtro por Dirección
            if (!string.IsNullOrEmpty(address))
            {
                filters.Add(Builders<Property>.Filter.Regex(p => p.Address, new BsonRegularExpression(address, "i")));
            }

            // 3. Filtro por Rango de Precio
            if (minPrice.HasValue)
            {
                filters.Add(Builders<Property>.Filter.Gte(p => p.Price, minPrice.Value)); // Greater Than or Equal
            }
            if (maxPrice.HasValue)
            {
                filters.Add(Builders<Property>.Filter.Lte(p => p.Price, maxPrice.Value)); // Less Than or Equal
            }

            // Combina todos los filtros con AND
            var finalFilter = filters.Any()
                ? Builders<Property>.Filter.And(filters)
                : Builders<Property>.Filter.Empty; // Si no hay filtros, devuelve todos

            return await _propertiesCollection.Find(finalFilter).ToListAsync();
        }
        /// <summary>
        /// Obtiene una propiedad por su ID.
        /// </summary>
        public async Task<Property> GetPropertyByIdAsync(string id)
        {
            return await MongoDBHelper.Instance.GetById<Property>(CollectionName, id);
        }

        public async Task<Tuple<IEnumerable<Property>, long>> GetPagedAsync(
            string name,
            string address,
            decimal? minPrice,
            decimal? maxPrice,
            int pageNumber,
            int pageSize)
        {
            // --- 1. CONSTRUCCIÓN DEL FILTRO ---
            // Le pasamos los parámetros individuales al método BuildFilter.
            var finalFilter = BuildFilter(name, address, minPrice, maxPrice);

            // --- 2. CONTEO TOTAL ---
            long totalCount = await _propertiesCollection.CountDocumentsAsync(finalFilter);

            // --- 3. CÁLCULO DE PAGINACIÓN ---
            // Usamos los parámetros de paginación individuales
            int skip = (pageNumber - 1) * pageSize;

            // --- 4. OBTENCIÓN DE RESULTADOS PAGINADOS ---
            var pagedResults = await _propertiesCollection
                .Find(finalFilter)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();

            // --- 5. RETORNO ---
            return new Tuple<IEnumerable<Property>, long>(pagedResults, totalCount);
        }

        /// <summary>
        /// Método privado que construye el filtro de búsqueda dinámicamente.
        /// Ahora acepta parámetros individuales.
        /// </summary>
        private FilterDefinition<Property> BuildFilter(
            string name,
            string address,
            decimal? minPrice,
            decimal? maxPrice)
        {
            var filtersList = new List<FilterDefinition<Property>>();

            // 1. Filtro por Nombre
            if (!string.IsNullOrEmpty(name))
            {
                filtersList.Add(Builders<Property>.Filter.Regex(p => p.Name, new BsonRegularExpression(name, "i")));
            }

            // 2. Filtro por Dirección
            if (!string.IsNullOrEmpty(address))
            {
                filtersList.Add(Builders<Property>.Filter.Regex(p => p.Address, new BsonRegularExpression(address, "i")));
            }

            // 3. Filtro por Rango de Precio
            if (minPrice.HasValue)
            {
                filtersList.Add(Builders<Property>.Filter.Gte(p => p.Price, minPrice.Value));
            }
            if (maxPrice.HasValue)
            {
                filtersList.Add(Builders<Property>.Filter.Lte(p => p.Price, maxPrice.Value));
            }

            return filtersList.Any()
                ? Builders<Property>.Filter.And(filtersList)
                : Builders<Property>.Filter.Empty;
        }
    }
}
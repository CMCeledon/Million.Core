using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository.Helpers
{
    /// <summary>
    /// Class MongoDBHelper: Proporciona métodos básicos para interactuar con MongoDB Atlas.
    /// </summary>
    public class MongoDBHelper
    {
        #region Singleton
        private MongoClient _client;
        private readonly string _databaseName;
        // Propiedad que retorna la única instancia del Helper.
        public static MongoDBHelper Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new MongoDBHelper());
                }
            }
        }

        #endregion Singleton

        #region Propiedades Privadas y Públicas

        private static MongoDBHelper _instance;
        private static readonly object Padlock = new object();

        #endregion Propiedades Privadas y Públicas

        // Constructor privado para el patrón Singleton
        private MongoDBHelper()
        {
            // Nota: Aquí deberías obtener el Connection String de una fuente segura (ej: appsettings.json)

            // 1. Obtiene la URI de conexión de la clase de configuración
            string connectionUri = MongoDBCommonHelpers.Instance.MongoDBConnectionUri;

            // 2. Obtiene el nombre de la base de datos
            _databaseName = MongoDBCommonHelpers.Instance.DatabaseName;

            // 3. Inicializa el cliente
            _client = new MongoClient(connectionUri);
        }

        /// <summary>
        /// Obtiene una referencia a la colección especificada.
        /// </summary>
        /// <typeparam name="TDocument">El tipo de la entidad (e.g., Property, Owner).</typeparam>
        /// <param name="collectionName">El nombre de la colección en la base de datos.</param>
        /// <returns>IMongoCollection&lt;TDocument&gt;.</returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
        {
            var database = _client.GetDatabase(_databaseName);
            return database.GetCollection<TDocument>(collectionName);
        }

        #region Métodos Básicos de CRUD (Similares a Dapper)

        /// <summary>
        /// Obtiene todos los documentos de una colección (equivalente a GetAll/ExecuteQuerySelect).
        /// </summary>
        public async Task<IEnumerable<TDocument>> GetAll<TDocument>(string collectionName)
        {
            var collection = GetCollection<TDocument>(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Obtiene un documento por su ID (equivalente a Get por PK).
        /// </summary>
        public async Task<TDocument> GetById<TDocument>(string collectionName, string id)
        {
            var collection = GetCollection<TDocument>(collectionName);
            var filter = Builders<TDocument>.Filter.Eq("_id", new ObjectId(id));
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserta un documento y devuelve el ID generado.
        /// </summary>
        public async Task<string> InsertOne<TDocument>(string collectionName, TDocument document)
        {
            var collection = GetCollection<TDocument>(collectionName);
            await collection.InsertOneAsync(document);

            // Para devolver el ID, asumimos que la entidad TDocument tiene una propiedad 'Id'
            // que se actualizó con el ObjectId generado.
            var idProperty = typeof(TDocument).GetProperty("Id");
            return idProperty?.GetValue(document)?.ToString();
        }

        /// <summary>
        /// Actualiza un documento existente por su ID (equivalente a Update).
        /// </summary>
        public async Task<bool> UpdateOne<TDocument>(string collectionName, string id, TDocument document)
        {
            var collection = GetCollection<TDocument>(collectionName);
            var filter = Builders<TDocument>.Filter.Eq("_id", new ObjectId(id));
            var result = await collection.ReplaceOneAsync(filter, document);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        /// <summary>
        /// Elimina un documento por su ID (equivalente a Delete).
        /// </summary>
        public async Task<bool> DeleteOne<TDocument>(string collectionName, string id)
        {
            var collection = GetCollection<TDocument>(collectionName);
            var filter = Builders<TDocument>.Filter.Eq("_id", new ObjectId(id));
            var result = await collection.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        #endregion Métodos Básicos de CRUD
    }
}
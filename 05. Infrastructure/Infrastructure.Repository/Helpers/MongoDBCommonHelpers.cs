// ***********************************************************************
// Assembly         : CMCELEDON.Practical.Comun
// Author           : Carlos Mario Celedón Rodelo - cmceledon
// Created          : [Fecha Actual]
//
// Last Modified By : Carlos Mario Celedón Rodelo - cmceledon
// Last Modified On : [Fecha Actual]
// ***********************************************************************
// <copyright file="MongoDBCommonHelpers.cs" company="OlimpiaIt">
//     Copyright ©  [Año Actual]
// </copyright>
// <summary>Helper para almacenar las configuraciones de conexión de MongoDB.</summary>
// ***********************************************************************
using System;
using System.Text;

namespace Infrastructure.Repository.Helpers
{
    /// <summary>
    /// Class MongoDBCommonHelpers. Gestiona la configuración de conexión de MongoDB.
    /// </summary>
    public class MongoDBCommonHelpers
    {
        #region Singleton

        /// <summary>
        /// Obtiene la instancia Singleton.
        /// </summary>
        /// <value>The instance.</value>
        public static MongoDBCommonHelpers Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new MongoDBCommonHelpers());
                }
            }
        }

        #endregion Singleton

        #region Propiedades Privadas

        /// <summary>
        /// La instancia
        /// </summary>
        private static MongoDBCommonHelpers _instance;

        /// <summary>
        /// El padlock para el Singleton
        /// </summary>
        private static readonly object Padlock = new object();

        #endregion Propiedades Privadas

        #region Propiedades Públicas

        /// <summary>
        /// URI de Conexión a MongoDB Atlas (ej: mongodb+srv://...)
        /// Es el equivalente a ProjectEntitiesConnection para SQL.
        /// </summary>
        public string MongoDBConnectionUri { get; set; }

        /// <summary>
        /// Nombre de la base de datos de MongoDB (ej: million).
        /// </summary>
        public string DatabaseName { get; set; }

        #endregion Propiedades Públicas
    }
}
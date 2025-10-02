// ***********************************************************************
// Assembly         : CMCELEDON.Practical.Comun
// Author           : Carlos Mario Celedón Rodelo - cmceledon
// Created          : 24-10-2021
//
// Last Modified By : Carlos Mario Celedón Rodelo - cmceledon
// Last Modified On : 24-10-2021
// ***********************************************************************
// <copyright file="CommonHelpers.cs" company="OlimpiaIt">
//     Copyright ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text;
using System.Text.Json;

namespace Transversal.Helpers
{
    /// <summary>
    /// Class CommonHelpers.
    /// </summary>
    public class TransversalHelpers
    {
        #region Singleton

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TransversalHelpers Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new TransversalHelpers());
                }
            }
        }

        #endregion Singleton

        #region Propiedades Privadas

        /// <summary>
        /// The instance
        /// </summary>
        private static TransversalHelpers _instance;

        /// <summary>
        /// The padlock
        /// </summary>
        private static readonly object Padlock = new object();

        /// <summary>
        /// Gets or sets the serialize options.
        /// </summary>
        /// <value>
        /// The serialize options.
        /// </value>
        private JsonSerializerOptions _serializeOptions { get; set; }

        #endregion Propiedades Privadas

        #region Propiedades Públicas

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public string TransactionId { get; set; }
       
        /// <summary>
        /// Gets or sets the BLOB storage connection.
        /// </summary>
        /// <value>
        /// The BLOB storage connection.
        /// </value>
        public string originDefault { get; set; } = "*";

        /// <summary>
        /// Gets the serialize options.
        /// </summary>
        /// <value>
        /// The serialize options.
        /// </value>
        public JsonSerializerOptions SerializeOptions
        {
            get
            {
                if (_serializeOptions == null)
                    _serializeOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                return _serializeOptions;
            }
        }


        #endregion Propiedades Públicas

    }
}

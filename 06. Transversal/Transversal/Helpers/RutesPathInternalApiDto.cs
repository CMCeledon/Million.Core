// ***********************************************************************
// Assembly         : CMCELEDON.Practical.Comun
// Author           : Carlos Mario Celedón
// Created          : 11-11-2019
//
// Last Modified By : Carlos Mario Celedón
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="ResultadosGeneralJsonDto.cs" company="AppComun">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Transversal.Helpers
{
    /// <summary>
    /// Class RutesPathInternalApiDto.
    /// </summary>
    public class RutesPathInternalApiDto
    {
        /// <summary>
        /// The controller
        /// </summary>
        public const string Controller = "Controller";
        /// <summary>
        /// The action
        /// </summary>
        public const string Action = "Action";

        /// <summary>
        /// 
        /// </summary>
        public static class Index
        {
            /// <summary>
            /// The index
            /// </summary>
            public const string index = "index";
            /// <summary>
            /// The swagger
            /// </summary>
            public const string swagger = "swagger";
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Property
        {
            /// <summary>
            /// The get all
            /// </summary>
            public const string GetAllPropertiesAsync = "api/property/getAllPropertiesAsync";
            public const string GetPagedAsync = "api/property/getPagedAsync";
            public const string GetPropertyByIdAsync = "api/property/{id}";
        }

    }
}

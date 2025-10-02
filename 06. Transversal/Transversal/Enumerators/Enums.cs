// ***********************************************************************
// Assembly         :  CMCELEDON.Servicios.Transversal.Enums
// Author           : cmceledon (Carlos Mario Celedón Rodelo)
// Created          : 06-11-2019
//
// Last Modified By : cmceledon (Carlos Mario Celedón Rodelo)
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="Enums.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Transversal.Enumerators
{
    /// <summary>
    /// Clase Enums
    /// </summary>
    public class Enums
    {
        
        /// <summary>
        /// Enum Mensajes Respuesta General
        /// </summary>
        public enum MensajeRespuesta
        {
            /// <summary>
            /// The ok
            /// </summary>
            [StringValue("Correcto")]
            Ok,

            /// <summary>
            /// The si
            /// </summary>
            [StringValue("Si")]
            Si,
            /// <summary>
            /// The sin datos
            /// </summary>
            [StringValue("Sin Información")]
            SinDatos,

            /// <summary>
            /// The no
            /// </summary>
            [StringValue("No")]
            No,
            /// <summary>
            /// The no
            /// </summary>
            [StringValue("Consulta")]
            Consulta,

            
        }

      

    }
}

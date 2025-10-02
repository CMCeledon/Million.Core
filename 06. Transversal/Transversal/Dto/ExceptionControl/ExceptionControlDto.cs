
// ***********************************************************************
// Assembly         : OlimpiaIt.Servicios.Modelo.Entities
// Author           : www.cmceledon.com (Carlos Mario Celedón Rodelo)
// Created          : 2020-02-18 16:38:36.163
//
// Last Modified By : www.cmceledon.com (Carlos Mario Celedón Rodelo)
// Last Modified On : 2020-02-18 16:38:36.163
// ***********************************************************************
// <copyright file="ExceptionControl.cs" company="OlimpiaIt">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Transversal.Dto
{
    /// <summary>
    /// Clase: ExceptionControl. (Entidad de Dominio)
    /// </summary>
    public class ExceptionControlDto
    {
        /// <summary>
        /// Atributo:  ExceptionId, de la entidad ExceptionControl.
        /// </summary>
        public int ExceptionId { get; set; }

        /// <summary>
        /// Atributo:  ExceptionMessage, de la entidad ExceptionControl.
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Atributo:  ControllerName, de la entidad ExceptionControl.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Atributo:  ActionName, de la entidad ExceptionControl.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Atributo:  ExceptionStackTrack, de la entidad ExceptionControl.
        /// </summary>
        public string ExceptionStackTrack { get; set; }

        /// <summary>
        /// Atributo:  ExceptionLogTime, de la entidad ExceptionControl.
        /// </summary>
        public DateTime ExceptionLogTime { get; set; }

        /// <summary>
        /// Atributo:  IdUsuario, de la entidad ExceptionControl.
        /// </summary>
        public string IdUsuario { get; set; }

    }
}
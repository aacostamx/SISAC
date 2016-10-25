//-----------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Volaris">
// Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------
namespace VOI.SISAC.Web
{
    using System.Web.Mvc;

   /// <summary>
   /// Clase para la configuración de los filtros
   /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registra los filtros globales
        /// </summary>
        /// <param name="filters">Parámetro que contiene los filtros a registrar</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

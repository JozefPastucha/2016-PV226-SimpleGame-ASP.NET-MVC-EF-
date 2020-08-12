using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Resources;

namespace BL.Services.Resources
{
    public interface IResourceService
    {
        /// <summary>
        /// Lists all resources of a village
        /// </summary>
        /// <param name="villageId">villageId</param>
        /// <returns>All resources by the village</returns>
        IEnumerable<ResourceDTO> ListResourcesByVillage(int villageId);

        /// <summary>
        /// Adds resources harvested in timeSpan
        /// </summary>
        /// <param name="timeSpan">timeSpan in which resources were harvested</param>
        /// <param name="villageId">villageId</param>
        void AddResources(TimeSpan timeSpan, int villageId);
    }
}

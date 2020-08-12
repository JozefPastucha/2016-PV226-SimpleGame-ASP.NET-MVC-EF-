using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Resources;
using BL.DTOs.Resources;

namespace BL.Facades
{
    public class ResourceFacade
    {
        private readonly IResourceService resourceService;

        public ResourceFacade(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        /// <summary>
        /// Lists all resources of a village
        /// </summary>
        /// <param name="villageId">villageId</param>
        /// <returns>All resources by the village</returns>
        public IEnumerable<ResourceDTO> ListResourcesByVillage(int villageId)
        {
            return resourceService.ListResourcesByVillage(villageId);
        }

        /// <summary>
        /// Adds resources harvested in timeSpan
        /// </summary>
        /// <param name="timeSpan">timeSpan in which resources were harvested</param>
        /// <param name="villageId">villageId</param>
        public void AddResources(TimeSpan timeSpan, int villageId)
        {
            resourceService.AddResources(timeSpan, villageId);
        }
       
    }
}

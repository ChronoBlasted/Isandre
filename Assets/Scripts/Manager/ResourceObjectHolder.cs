using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BaseTemplate.Behaviours;

public class ResourceObjectHolder : MonoSingleton<ResourceObjectHolder>
{
    public List<ResourceObject> resources;

    public ResourceObject GetResourceByType(ResourceType resourceType)
    {
        return resources.First(resource => resource.type == resourceType);
    }
}

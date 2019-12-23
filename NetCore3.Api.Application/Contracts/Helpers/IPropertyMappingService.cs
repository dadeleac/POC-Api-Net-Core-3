using NetCore3.Api.Application.Helpers.OrderMapping;
using System.Collections.Generic;

namespace NetCore3.Api.Application.Contracts.Helpers
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistFor<TSource, TDestination>(string fields); 
    }
}
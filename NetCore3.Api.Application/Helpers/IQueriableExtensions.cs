using NetCore3.Api.Application.Helpers.OrderMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace NetCore3.Api.Application.Helpers
{
    public static class IQueriableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source)); 
            }

            if(mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary)); 
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                return source;
            }
            
            var orderByAfterSplit = orderBy.Split(','); 

            foreach(var order in orderByAfterSplit.Reverse())
            {
                var trimmOrder = order.Trim();
                var orderDescending = trimmOrder.EndsWith(" desc");

                var indexOfFirstSpace = trimmOrder.IndexOf(" ");

                var propertyName = indexOfFirstSpace == -1 ?
                    trimmOrder : trimmOrder.Remove(indexOfFirstSpace);

                var propertyMappingValue = mappingDictionary[propertyName]; 

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing"); 
                }

                foreach(var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    source = source.OrderBy(destinationProperty + (orderDescending ? " descending" : " ascending"));
                }
            }

            return source; 
        }
    }
}

﻿using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Application.Helpers.OrderMapping;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.Domain.Models.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore3.Api.Application.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" }) },
                { "Job", new PropertyMappingValue(new List<string>() { "Job" }) },
                { "Age", new PropertyMappingValue(new List<string>() { "DateOfBirth" }, true) },
                { "Name", new PropertyMappingValue(new List<string>() { "Name", "Surname" }) }
            };


        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<AuthorModel, Author>(_authorPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>");

        }
    }
}

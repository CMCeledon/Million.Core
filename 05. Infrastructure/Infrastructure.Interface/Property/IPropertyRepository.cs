
using Domain.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transversal.Dto;

namespace Infrastructure.Interface
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property> GetPropertyByIdAsync(string id);
        Task<List<Property>> GetPropertiesByOwnerAsync(string ownerId);
        Task<string> CreatePropertyAsync(Property newProperty);
        Task<List<Property>> GetPropertiesByMinPriceAsync(decimal minPrice);
        Task<IEnumerable<Property>> SearchAsync(string name, string address, decimal? minPrice, decimal? maxPrice);
        Task<Tuple<IEnumerable<Property>, long>> GetPagedAsync(
        string name,
        string address,
        decimal? minPrice,
        decimal? maxPrice,
        int pageNumber,
        int pageSize
    );
    }
}

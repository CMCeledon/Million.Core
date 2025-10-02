using Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transversal.Dto;

namespace Application.Abstract
{
    public  interface IPropertyService
    {
        Task<ResponseServices<PropertyDto>> GetPropertyByIdAsync(string id);
        Task<ResponseServices<IEnumerable<PropertyDto>>> GetAllPropertiesAsync();
        Task<ResponseServices<IEnumerable<PropertyDto>>> GetPropertiesByOwnerAsync(string ownerId);
        Task<ResponseServices<string>> CreatePropertyAsync(PropertyDto property);
        Task<ResponseServices<bool>> UpdatePropertyAsync(PropertyDto property);
        Task<ResponseServices<IEnumerable<PropertyDto>>> SearchPropertiesAsync(PropertyFilterDto filters);
        Task<ResponseServices<PagedResponseDto<PropertyDto>>> GetPagedAsync(PropertyFilterDto filters);
    }
}
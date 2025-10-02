using Application.Abstract;
using Application.Helpers;
using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Transversal.Dto;
using Transversal.Enumerators;
namespace Application.Implements
{
    public class PropertyService(IPropertyRepository propertyRepository) : IPropertyService
    {
        public Task<ResponseServices<string>> CreatePropertyAsync(PropertyDto property)
        {
            throw new System.NotImplementedException();
        }

     
        public Task<ResponseServices<IEnumerable<PropertyDto>>> GetPropertiesByOwnerAsync(string ownerId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseServices<PropertyDto>> GetPropertyByIdAsync(string id)
        {
            var result = await propertyRepository.GetPropertyByIdAsync(id);
            if (result != null)
            {
                var mapAdaptadorResult = AutoMapperConfig.Mapper.Map<PropertyDto>(result);
                return ResponseService.GenericResponse<PropertyDto>(mapAdaptadorResult, Enums.MensajeRespuesta.Consulta);
            }
            return ResponseService.GenericResponse<PropertyDto>(null, Enums.MensajeRespuesta.SinDatos);
        }

        public Task<ResponseServices<bool>> UpdatePropertyAsync(PropertyDto property)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseServices<IEnumerable<PropertyDto>>> GetAllPropertiesAsync()
        {
            var resultList = await propertyRepository.GetAllPropertiesAsync();
            var mapAdaptadorResult = AutoMapperConfig.Mapper.Map<IEnumerable<PropertyDto>>(resultList);
            return ResponseService.GenericResponse(mapAdaptadorResult, Enums.MensajeRespuesta.SinDatos);
        }

        public async Task<ResponseServices<IEnumerable<PropertyDto>>> SearchPropertiesAsync(PropertyFilterDto filters)
        {
                var resultList = await propertyRepository.SearchAsync(
                    filters.Name, filters.Address, filters.MinPrice, filters.MaxPrice
                );

                var mappedResult = AutoMapperConfig.Mapper.Map<IEnumerable<PropertyDto>>(resultList);

            return ResponseService.GenericResponse(
                    mappedResult,
                    Enums.MensajeRespuesta.SinDatos
                );
        }
        public async Task<ResponseServices<PagedResponseDto<PropertyDto>>> GetPagedAsync(PropertyFilterDto filters)
        {
            var repoResult = await propertyRepository.GetPagedAsync(
                filters.Name, filters.Address, filters.MinPrice, filters.MaxPrice,filters.PageNumber,filters.PageSize
            );

            var resultList = repoResult.Item1;
            var totalCount = repoResult.Item2; 

            var mappedData = AutoMapperConfig.Mapper.Map<List<PropertyDto>>(resultList);

            var pagedResponseDto = new PagedResponseDto<PropertyDto>
            {
                Data = mappedData,
                TotalItems = totalCount,
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize
            };

            return ResponseService.GenericResponse(
                pagedResponseDto,
                Enums.MensajeRespuesta.SinDatos
            );
        }
    }
}

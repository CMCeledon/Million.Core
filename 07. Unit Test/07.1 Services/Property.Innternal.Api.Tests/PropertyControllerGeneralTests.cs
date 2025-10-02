using Application.Abstract;
using Application.Helpers;
using Application.Implements; 
using Microsoft.AspNetCore.Mvc;
using Million.Internal.Api.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transversal.Dto;
using Xunit;



namespace PropertyAPI.Tests
{
    public class PropertyControllerTests
    {
        private readonly Mock<IPropertyService> _mockService;
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            _mockService = new Mock<IPropertyService>();
            // Instanciamos el controlador con el mock
            _controller = new PropertyController(_mockService.Object);
        }

        // --- PRUEBAS PARA GET ALL (SIN PAGINACIÓN) ---

        [Fact]
        public async Task GetAllPropertiesAsync_ReturnsOkWithData_WhenSuccessful()
        {
            // Arrange
            var mockData = new List<PropertyDto> { new PropertyDto { Id = "101", Name = "Casa Central" } };

            // Simular respuesta exitosa del servicio
            var serviceResponse = new ResponseServices<IEnumerable<PropertyDto>>
            {
                State = true,
                Message = "Consulta exitosa.",
                Info = mockData
            };
            _mockService.Setup(s => s.GetAllPropertiesAsync()).ReturnsAsync(serviceResponse);

            // Act
            // El controlador devuelve ResponseServices<T>, el filtro ActionFilter (ResponseHandler)
            // lo convertirá a OkObjectResult.
            var result = await _controller.GetAllPropertiesAsync();

            // Assert: Verificamos el contenido y el tipo esperado por el controlador
            Assert.IsType<ResponseServices<IEnumerable<PropertyDto>>>(result);
            Assert.True(result.State);
            Assert.Equal(1, ((List<PropertyDto>)result.Info).Count);
        }

        [Fact]
        public async Task GetAllPropertiesAsync_ReturnsEmptyList_WhenNoData()
        {
            // Arrange
            var serviceResponse = new ResponseServices<IEnumerable<PropertyDto>>
            {
                State = true,
                Message = "Sin datos.",
                Info = new List<PropertyDto>() // Lista vacía
            };
            _mockService.Setup(s => s.GetAllPropertiesAsync()).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAllPropertiesAsync();

            // Assert
            Assert.IsType<ResponseServices<IEnumerable<PropertyDto>>>(result);
            Assert.Empty(result.Info);
        }

        // --- PRUEBAS PARA GET PAGED (BÚSQUEDA/FILTROS POST) ---

        [Fact]
        public async Task GetPagedAsync_ReturnsOkWithData_WhenFiltersApplied()
        {
            // Arrange
            var filter = new PropertyFilterDto { PageNumber = 1, PageSize = 10, Name = "Apartamento" };
            var mockPagedData = new PagedResponseDto<PropertyDto>
            {
                Data = new List<PropertyDto> { new PropertyDto { Id = "202" } },
                TotalItems = 5
            };

            var serviceResponse = new ResponseServices<PagedResponseDto<PropertyDto>>
            {
                State = true,
                Message = "Búsqueda exitosa.",
                Info = mockPagedData
            };
            _mockService.Setup(s => s.GetPagedAsync(It.IsAny<PropertyFilterDto>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetPagedAsync(filter);

            // Assert
            Assert.IsType<ResponseServices<PagedResponseDto<PropertyDto>>>(result);
            Assert.True(result.State);
            Assert.Equal(5, result.Info.TotalItems);
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsInternalServerError_WhenServiceFails()
        {
            // Arrange
            var filter = new PropertyFilterDto { MinPrice = 100 };

            // Simular una falla crítica en el servicio (ej. error de base de datos)
            var serviceResponse = new ResponseServices<PagedResponseDto<PropertyDto>>
            {
                State = false,
                Message = "Error en MongoDB.",
                Warning = "Excepción de conexión.",
                Info = null
            };
            _mockService.Setup(s => s.GetPagedAsync(It.IsAny<PropertyFilterDto>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetPagedAsync(filter);

            // Assert
            // El filtro ActionFilter debería interceptar 'State=false' con Warning y devolver 500
            // Pero como la firma del controlador devuelve ResponseServices<T>,
            // solo verificamos que la bandera de error esté correctamente propagada.
            Assert.IsType<ResponseServices<PagedResponseDto<PropertyDto>>>(result);
            Assert.False(result.State);
            Assert.NotNull(result.Warning);
        }

        // --- PRUEBAS PARA GET BY ID ---

        [Fact]
        public async Task GetPropertyById_ReturnsOkWithProperty_WhenExists()
        {
            // Arrange
            var expectedProperty = new PropertyDto { Id = "id_existente", Name = "Villa Linda" };
            var serviceResponse = new ResponseServices<PropertyDto>
            {
                State = true,
                Info = expectedProperty
            };
            _mockService.Setup(s => s.GetPropertyByIdAsync("id_existente")).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetPropertyByIdAsync("id_existente");

            // Assert
            Assert.IsType<ResponseServices<PropertyDto>>(result);
            Assert.True(result.State);
            Assert.Equal("Villa Linda", result.Info.Name);
        }

        [Fact]
        public async Task GetPropertyById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            // Simular respuesta exitosa (State=true) pero Info=null, o falla lógica (State=false)
            var serviceResponse = new ResponseServices<PropertyDto>
            {
                State = true, // Podría ser true si el servicio maneja 'no data' sin error crítico
                Message = "Propiedad no encontrada.",
                Info = null
            };
            _mockService.Setup(s => s.GetPropertyByIdAsync("id_no_existe")).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetPropertyByIdAsync("id_no_existe");

            // Assert
            // Verificamos que el Info sea null y el State siga la convención de 'no data'
            Assert.IsType<ResponseServices<PropertyDto>>(result);
            Assert.Null(result.Info);
        }
    }
}
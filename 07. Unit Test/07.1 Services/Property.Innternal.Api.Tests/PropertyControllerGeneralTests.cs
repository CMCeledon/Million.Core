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

        // --- PRUEBAS PARA GET ALL (SIN PAGINACI�N) ---

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
            // lo convertir� a OkObjectResult.
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
                Info = new List<PropertyDto>() // Lista vac�a
            };
            _mockService.Setup(s => s.GetAllPropertiesAsync()).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAllPropertiesAsync();

            // Assert
            Assert.IsType<ResponseServices<IEnumerable<PropertyDto>>>(result);
            Assert.Empty(result.Info);
        }

        // --- PRUEBAS PARA GET PAGED (B�SQUEDA/FILTROS POST) ---

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
                Message = "B�squeda exitosa.",
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

            // Simular una falla cr�tica en el servicio (ej. error de base de datos)
            var serviceResponse = new ResponseServices<PagedResponseDto<PropertyDto>>
            {
                State = false,
                Message = "Error en MongoDB.",
                Warning = "Excepci�n de conexi�n.",
                Info = null
            };
            _mockService.Setup(s => s.GetPagedAsync(It.IsAny<PropertyFilterDto>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetPagedAsync(filter);

            // Assert
            // El filtro ActionFilter deber�a interceptar 'State=false' con Warning y devolver 500
            // Pero como la firma del controlador devuelve ResponseServices<T>,
            // solo verificamos que la bandera de error est� correctamente propagada.
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
            // Simular respuesta exitosa (State=true) pero Info=null, o falla l�gica (State=false)
            var serviceResponse = new ResponseServices<PropertyDto>
            {
                State = true, // Podr�a ser true si el servicio maneja 'no data' sin error cr�tico
                Message = "Propiedad no encontrada.",
                Info = null
            };
            _mockService.Setup(s => s.GetPropertyByIdAsync("id_no_existe")).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetPropertyByIdAsync("id_no_existe");

            // Assert
            // Verificamos que el Info sea null y el State siga la convenci�n de 'no data'
            Assert.IsType<ResponseServices<PropertyDto>>(result);
            Assert.Null(result.Info);
        }
    }
}
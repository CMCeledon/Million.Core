namespace Million.Internal.Api.Controllers;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
public class PropertyController : ControllerBase
{
    /// <summary>
    /// The service
    /// </summary>
    private readonly IPropertyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyController"/> class.
    /// </summary>
    /// <param name="service">The service.</param>
    public PropertyController(IPropertyService service) => _service = service;

    /// <summary>
    /// Todas las propiedades
    /// </summary>
    /// <returns></returns>
    [HttpGet(RutesPathInternalApiDto.Property.GetAllPropertiesAsync)]
    public async Task<ResponseServices<IEnumerable<PropertyDto>>> GetAllPropertiesAsync() => await _service.GetAllPropertiesAsync();

    /// <summary>
    /// Obtener propiedad por Id
    /// </summary>
    /// <returns></returns>
    [HttpGet(RutesPathInternalApiDto.Property.GetPropertyByIdAsync)]
    public async Task<ResponseServices<PropertyDto>> GetPropertyByIdAsync(string id) => await _service.GetPropertyByIdAsync(id);

    /// <summary>
    /// Lista paginada de propiedades
    /// </summary>
    /// <param name="propertyFilterDto">The usuario.</param>
    /// <returns></returns>
    [HttpPost(RutesPathInternalApiDto.Property.GetPagedAsync)]
    public async Task<ResponseServices<PagedResponseDto<PropertyDto>>> GetPagedAsync(PropertyFilterDto propertyFilterDto) => await _service.GetPagedAsync(propertyFilterDto);


}

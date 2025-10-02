
using System;
using System.Collections.Generic;

namespace Transversal.Dto;

public class PropertyDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Price { get; set; }
    public string CodeInternal { get; set; }
    public int Year { get; set; }

    public string IdOwner { get; set; }

    // Incluirá las imágenes y trazas
    public List<PropertyImageDto> Images { get; set; }
    public List<PropertyTraceDto> Traces { get; set; }

}

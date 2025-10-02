
using System;
using System.Collections.Generic;

namespace Transversal.Dto;

public class PropertyFilterDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25; 
}


using System;
using System.Collections.Generic;

namespace Transversal.Dto;

public class PagedResponseDto<T>
{
    public IEnumerable<T> Data { get; set; } 
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalItems { get; set; } 
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.QueryAttributesDtos
{
    public class PagingDto
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}

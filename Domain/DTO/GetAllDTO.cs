using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class GetAllDTO
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool IsAscending { get; set; }
        public bool IsApproved { get; set; }
    }
}

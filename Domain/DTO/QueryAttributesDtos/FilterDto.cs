﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.QueryAttributesDtos
{
    public class FilterDto
    {
        public bool? IsAscending { get; set; }
        public bool? IsApproved { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.ResponseDtos
{
    public class BaseResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class BaseResponseDto<T> where T : class
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Content { get; set; }
    }
}
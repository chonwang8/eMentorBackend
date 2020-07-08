using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.ResponseDtos
{
    public class SharingResponseDto<T> where T : class
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Content { get; set; }
    }

    public class SharingResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}

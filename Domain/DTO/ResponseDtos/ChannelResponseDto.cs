using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.ResponseDtos
{
    public class ChannelResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class ChannelResponseDto<T> where T : class
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Content { get; set; }
    }
}

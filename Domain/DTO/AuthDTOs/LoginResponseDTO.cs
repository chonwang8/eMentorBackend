using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.AuthDTOs
{
    public class LoginResponseDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }

        public LoginResponseDTO() { }

        //public LoginResponseDTO(bool isValid, string message, Object result)
        //{
        //    if (isValid == true)
        //    {
        //        LoginResponseDTO response = new LoginResponseDTO();
        //        response.Status = 1;
        //        response.Message = (message.Length > 0 ? message : "Success");
        //        response.Result = result;

        //        return Response.status(Status.OK).entity(response).build();

        //    }
        //    else
        //    {
        //        LoginResponseDTO response = new LoginResponseDTO();
        //        response.Status = 1;
        //        response.Message = (message.Length > 0 ? message : "Failed");
        //        response.Result = result;

        //        return Response.status(Status.OK).entity(response).build();
        //    }
        //}
    }
}

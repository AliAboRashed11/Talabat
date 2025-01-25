
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Talabat.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massage { get; set; }

        public ApiResponse(int statusCode , string? massage=null)
        {
            StatusCode = statusCode;
            Massage = massage?? GetDefultMessageForStatusCode(statusCode);
        }

        private string? GetDefultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.Hate leads to career thange",
                _ => null

            };
        }
    }
}

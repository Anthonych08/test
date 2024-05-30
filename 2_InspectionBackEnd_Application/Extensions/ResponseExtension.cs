using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_InspectionBackEnd_Shared.Responses;

namespace _2_InspectionBackEnd_Application.Extensions
{
    public static class ResponseExtension
    {

        public static ResponseBuilder<T> ResponseCreate<T>(this T response, bool isPlural = false)
        {

            return new ResponseBuilder<T>
            {
                //Message = isPlural ? ResponseLang.Response_CreatePlural : ResponseLang.Response_Create,
                Data = response,
            };
        }

        public static ResponseBuilder<T> ResponseRemove<T>(this T response)
        {

            return new ResponseBuilder<T>
            {
                //Message = ResponseLang.Response_Delete,
                Data = response,
            };
        }

        public static ResponseBuilder<T> ResponseChange<T>(this T response)
        {

            return new ResponseBuilder<T>
            {
                //Message = ResponseLang.Response_Update,
                Data = response,
            };
        }

        public static ResponseBuilder<T> ResponseRead<T>(this T response)
        {
            return new ResponseBuilder<T>
            {
                Data = response,
            };
        }
        public static ResponseBuilder<T> ResponseRead<T>(this T response, string message)
        {
            return new ResponseBuilder<T>
            {
                Message = message,
                Data = response,
            };
        }


        public static ResponseBuilder<T> Response<T>(this T response)
        {
            return new ResponseBuilder<T>
            {
                Data = response,
            };
        }

    }
}

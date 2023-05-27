using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<string> Errors { get; set; }
		public string Message { get; set; }


        //burada uygulanan metotlara static factory metot denir.direkt newlemek yerine returnle aynı sınıf icerisinde
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data=data, StatusCode=statusCode };
        }

		public static CustomResponseDto<T> Success(int statusCode)
		{
			return new CustomResponseDto<T> { StatusCode=statusCode};
		}

		public static CustomResponseDto<T> Fail(int statusCode,List<string> error)
		{
			return new CustomResponseDto<T> { StatusCode=statusCode, Errors=error  };
		}

		public static CustomResponseDto<T> Fail(int statusCode,string message)
		{
			return new CustomResponseDto<T> { StatusCode=statusCode,Message=message };
		}
	}
}

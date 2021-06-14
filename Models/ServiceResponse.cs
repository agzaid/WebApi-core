using project.Dtos.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;

        //public static implicit operator ServiceResponse<T>(ServiceResponse<GetCharacterDto> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

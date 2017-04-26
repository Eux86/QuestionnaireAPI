using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Api.Models.DTO
{
    public class UserDTO
    {
        public int Id {get;set;}
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public int RoleId { get; set; }

        public RoleDTO Role { get; set; }
    }
}
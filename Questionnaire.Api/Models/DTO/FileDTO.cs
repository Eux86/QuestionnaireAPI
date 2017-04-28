using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Api.Models.DTO
{
    public class FileDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
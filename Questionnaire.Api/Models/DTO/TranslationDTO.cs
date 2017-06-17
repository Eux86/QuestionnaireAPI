using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Api.Models.DTO
{
    public class TranslationDTO
    {
        public int Id;
        public string Key;
        public string Value;
        public DateTime LatestUpdate;
    }
}
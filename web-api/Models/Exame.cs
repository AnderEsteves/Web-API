using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_api.Models
{
    public class Exame
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public DateTime Data { get; set; }
    }
}
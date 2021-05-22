using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Avansight.Domain.Entities
{
    public class Patient
    {
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Avansight.Domain.Entities
{
    public class TreatmentReading
    {
        //[Key]
        //public int TreatmentReadingId { get; set; }
        [Display(Name = "Visit Week")]
        public string VisitWeek { get; set; }
        [Display(Name = "Reading")]
        public double Reading { get; set; }
        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }
        //[ForeignKey("PatientId")]
        //public virtual Patient Patient { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eGo.ScrumMolder.Web.Models
{
    public class TimeStepper
    {
        [Range(0, 8, ErrorMessage = @"Hey dudde, how you did it? There is only 9 hours in 1 working day!")]
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eGo.ScrumMolder.Web.Models
{
    public class DailyProjectScrum
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [HiddenInput]
        public DateTime? UpdateDate { get; set; }

        // In minutes
        [Required]
        [Display(Name = "Spent time")]
        [DataType(DataType.Text)]
        [UIHint("TimeStepper")]
        public TimeStepper SpentTimeStepper
        {
            get
            {
                return new TimeStepper { Hours = SpentTime.Hours, Minutes = SpentTime.Minutes};   
            }
            set { SpentTime = new TimeSpan(0, value.Hours, value.Minutes, 0); }
        }

        public TimeSpan SpentTime { get; set; }
        
        [Required]
        [Display(Name = "What I have done yesterday")]
        [DataType(DataType.MultilineText)]
        public string WhatDoneLastTime { get; set; }

        [Required]
        [Display(Name = "What problems I have got")]
        [DataType(DataType.MultilineText)]
        public string WhatProblems { get; set; }

        [Required]
        [Display(Name = "What I will do today")]
        [DataType(DataType.MultilineText)]
        public string WhatToDoNext { get; set; }

    }
}
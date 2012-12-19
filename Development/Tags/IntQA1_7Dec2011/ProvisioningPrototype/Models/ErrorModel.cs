using System;
using System.ComponentModel.DataAnnotations;

namespace ProvisioningPrototype.Models
{
    public class ErrorModel
    {
        [Display(Name = "Exception Message")]
        public Exception Exception { get; set; }

        [Display(Name = "Suggestion")]
        public string Suggestion { get; set; }
    }
}
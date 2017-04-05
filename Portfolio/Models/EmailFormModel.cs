using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your Name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Email"), EmailAddress]
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        //[Required]
        public string Body { get; set; }
        public HttpPostedFileBase Upload { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api.Models
{
    public class LoginViewModel
    {
        [Required,StringLength(maximumLength:20,MinimumLength =6)]
        public string LoginName { get; set; }
        [Required,StringLength(maximumLength:20,MinimumLength =6)]
        public string LoginPwd { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XMLApplication.Models
{
    public class ProjectModels
    {
        public int Id { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string Location { get; set; }

        public bool IsEdit { get; set; }

    }
}
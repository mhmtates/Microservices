using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Models.Catalog
{
    public class FeatureViewModel
    {

        [Display(Name="Süre")]
        public int Duration { get; set; }
    }
}

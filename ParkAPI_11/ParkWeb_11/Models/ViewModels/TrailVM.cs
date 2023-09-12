using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Models.ViewModels
{
    public class TrailVM
    {
        public Trail Trail { get; set; }

        public IEnumerable<SelectListItem> nationalParkList { get; set; }
    }
}

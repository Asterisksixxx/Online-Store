using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class DeleteSectionViewModel
    {
        public List<SubSection> SubSections { get; set; }=new List<SubSection>();
        public Section Section { get; set; }
    }
}

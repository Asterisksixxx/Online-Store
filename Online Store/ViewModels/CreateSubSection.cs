using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class CreateSubSection
    {
        public List<Section> Sections { get; set; }=new List<Section>();
        public SubSection SubSection { get; set; }

    }
}

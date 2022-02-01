using System.Collections.Generic;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class IndexViewModel
    {
        public List<Product>  Product { get; set; }=new List<Product>();
   
        public List<Section> Section { get; set; }=new List<Section>();
        public List<SubSection> SubSections { get; set; }=new List<SubSection>();
    }
}

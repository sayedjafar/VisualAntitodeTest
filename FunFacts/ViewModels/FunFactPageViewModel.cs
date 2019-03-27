using FunFacts.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace FunFacts.ViewModels
{
    public class FunFactPageViewModel
    {
        [DisplayName("Show only recent")]
        public bool IsMostRecent { get; set; }
        public IEnumerable<FunFact> FunFacts { get; set; }

    }
}

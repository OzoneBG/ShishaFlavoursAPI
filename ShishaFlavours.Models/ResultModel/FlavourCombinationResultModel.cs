using ShishaFlavoursAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShishaFlavours.API.ResultModel
{
    public class FlavourCombinationResultModel
    {
        public FlavourCombinationResultModel()
        {
            this.flavours = new List<string>();
        }

        public string Name { get; set; }

        public string UserName { get; set; }
        //ne e 
        public List<string> flavours { get; set; }
    }
}

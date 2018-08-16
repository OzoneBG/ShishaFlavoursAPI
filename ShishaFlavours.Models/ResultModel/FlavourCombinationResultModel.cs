namespace ShishaFlavours.API.ResultModel
{
    using System.Collections.Generic;

    public class FlavourCombinationResultModel
    {
        public FlavourCombinationResultModel()
        {
            this.flavours = new List<string>();
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public List<string> flavours { get; set; }
    }
}

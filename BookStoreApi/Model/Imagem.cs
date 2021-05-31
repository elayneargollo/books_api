using System.ComponentModel.DataAnnotations.Schema;
using Solutis.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Solutis.Model
{
    public class Imagem 
    {
        [NotMapped]
        public string smallThumbnail { set; get; }
        [NotMapped]

        public string thumbnail { set; get; }
    }
}
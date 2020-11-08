using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdeaEvaluation.Model
{
    public class IdeaModel
    {
       
        public int IdeaId { get; set; }
        
      
        public string IdeaName { get; set; }
        
       
        public string Description { get; set; }

         public bool IsEvaluate { get; set; }


        
    }
}
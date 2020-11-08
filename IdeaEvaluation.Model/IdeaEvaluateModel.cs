using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdeaEvaluation.Model
{
    public class IdeaEvaluateModel
    {
       
        public int UserId { get; set; }
        public int IdeaId { get; set; }
        
    }
}
using System;
using System.Collections.Generic;

namespace IdeaEvaluation.DataAccess.Models
{
    public partial class Idea
    {
        public Idea()
        {
            IdeaEvaluationHistory = new HashSet<IdeaEvaluationHistory>();
        }

        public long IdeaId { get; set; }
        public string IdeaName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<IdeaEvaluationHistory> IdeaEvaluationHistory { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace IdeaEvaluation.DataAccess.Models
{
    public partial class IdeaEvaluationHistory
    {
        public long EvaluationId { get; set; }
        public long? IdeaId { get; set; }
        public long? UserId { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual User User { get; set; }
    }
}

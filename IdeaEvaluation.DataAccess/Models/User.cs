using System;
using System.Collections.Generic;

namespace IdeaEvaluation.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            IdeaEvaluationHistory = new HashSet<IdeaEvaluationHistory>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<IdeaEvaluationHistory> IdeaEvaluationHistory { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IdeaEvaluation.Model;
using IdeaEvaluation.DataAccess.Models;

namespace IdeaEvaluation.Service
{
    public interface IIdeaEvaluationService
    {
      Task<User> ValidateUserDetailAsync(UserModel userDetail);

       Task<User> GetUserDetailAsync(int userId);

      Task<IEnumerable<IdeaModel>> GetIdeaListAsync(int userId);
      
      Task<IdeaEvaluateModel> EvaliateIdeaAsync(IdeaEvaluateModel IdeaEvaluateDetail);
    }
}
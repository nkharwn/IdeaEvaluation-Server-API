using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdeaEvaluation.Model;
using IdeaEvaluation.DataAccess.Repository;
using IdeaEvaluation.DataAccess.Models;
namespace IdeaEvaluation.Service
{
    public class IdeaEvaluationService : IIdeaEvaluationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Idea> _ideaRepository;
        private readonly IRepository<IdeaEvaluationHistory> _ideaEvaluationRepository;
        public IdeaEvaluationService(IRepository<User> userRepository, IRepository<Idea> ideaRepository,
         IRepository<IdeaEvaluationHistory> ideaEvaluationRepository)
        {
            _userRepository = userRepository;
            _ideaRepository = ideaRepository;
            _ideaEvaluationRepository = ideaEvaluationRepository;
        }
        public async Task<User> ValidateUserDetailAsync(UserModel userDetail)
        {
            return await _userRepository.GetAsync(predicate: p => p.UserName == userDetail.UserName && p.Password == userDetail.Password);
        }

        public async Task<IEnumerable<IdeaModel>> GetIdeaListAsync(int userId)
        {
            int evaluationCount = 3;
            List<IdeaModel> ideaList = new List<IdeaModel>();
            var totalIdeas = (await _ideaRepository.GetListAsync()).Count();
            var totalUsers = (await _userRepository.GetListAsync()).Count();
            var availableIdeaList=await _ideaRepository.GetListAsync(predicate: p => p.IdeaEvaluationHistory.Count() < evaluationCount
             && p.IdeaEvaluationHistory.Where(history => history.UserId == userId).Count() <= 0);
            var usersEvaluatedIdeas = await _ideaRepository.GetListAsync(predicate: p => p.IdeaEvaluationHistory.Where(history => history.UserId == userId).Count() > 0);
            if (totalIdeas > 0 && totalUsers > 0)
            {
                int totalEvaluations = totalIdeas * evaluationCount;
                int maxSize = (int)Math.Ceiling(totalEvaluations / (double)totalUsers);
                if (maxSize > 0 && totalIdeas >= maxSize)
                {
                    ideaList = availableIdeaList.Select(idea => new IdeaModel
                    {
                        IdeaId = (int)idea.IdeaId,
                        IdeaName = idea.IdeaName,
                        Description = idea.Description,
                        IsEvaluate = false

                    }).Take((maxSize - usersEvaluatedIdeas.Count())).ToList();
                }
            }

            if (usersEvaluatedIdeas != null && usersEvaluatedIdeas.Count() > 0)
            {
                ideaList.AddRange(usersEvaluatedIdeas.Select(idea => new IdeaModel
                {
                    IdeaId = (int)idea.IdeaId,
                    IdeaName = idea.IdeaName,
                    Description = idea.Description,
                    IsEvaluate = true

                }).ToList());
            }

            return ideaList.OrderBy(ideaList=>ideaList.IdeaId);
        }


        public async Task<User> GetUserDetailAsync(int userId)
        {

            return await _userRepository.GetAsync(predicate: p => p.UserId == userId);
        }


        public async Task<IdeaEvaluateModel> EvaliateIdeaAsync(IdeaEvaluateModel IdeaEvaluateDetail)
        {
            var ideaEvaluate = new IdeaEvaluationHistory
            {
                IdeaId = IdeaEvaluateDetail.IdeaId,
                UserId = IdeaEvaluateDetail.UserId
            };
            _ideaEvaluationRepository.Create(ideaEvaluate);
            var result = await _ideaEvaluationRepository.SaveChangesAsync();
            if (result > 0)
                return IdeaEvaluateDetail;
            else
                return null;

        }

    }
}
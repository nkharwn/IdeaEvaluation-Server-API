using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using IdeaEvaluation.Service;
using IdeaEvaluation.Model;

namespace IdeaEvaluation.Api.Controllers
{
    [Route("api/idea")]
    public class IdeaController : ControllerBase
    {

        private readonly IIdeaEvaluationService _ideaEvaluationService;
        public IdeaController(IIdeaEvaluationService ideaEvaluationService)
        {
            _ideaEvaluationService = ideaEvaluationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var user = await _ideaEvaluationService.GetUserDetailAsync(userId);
                if (user != null)
                {
                    var ideaList = await _ideaEvaluationService.GetIdeaListAsync(userId);
                    return Ok(ideaList);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (System.Exception)
            {
                return BadRequest();
            }


        }

        [HttpPost("evaluate")]
        public async Task<IActionResult> IdeaEvaluate([FromBody] IdeaEvaluateModel evaluateModel)
        {
            try
            {
                if (evaluateModel != null && evaluateModel.IdeaId > 0 && evaluateModel.UserId > 0)
                {
                    var result = await _ideaEvaluationService.EvaliateIdeaAsync(evaluateModel);
                    if (result != null)
                    {

                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {

                    return Unauthorized();
                }

            }
            catch (System.Exception)
            {
                return BadRequest();
            }


        }



    }
}

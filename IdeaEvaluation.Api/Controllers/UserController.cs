using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using IdeaEvaluation.Model;
using IdeaEvaluation.Service;

namespace IdeaEvaluation.Api.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly IIdeaEvaluationService _ideaEvaluationService;

        public UserController(IIdeaEvaluationService ideaEvaluationService)
        {
            _ideaEvaluationService=ideaEvaluationService;
        }

        

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] UserModel user)
        {
            var existingUser= await _ideaEvaluationService.ValidateUserDetailAsync(user);
            
             if(existingUser!=null){
               var userModel= new UserModel{UserId=(int)existingUser.UserId,UserName=existingUser.UserName};
                  return Ok(userModel);
             }
             else
                  return Unauthorized();
        }
    }
}

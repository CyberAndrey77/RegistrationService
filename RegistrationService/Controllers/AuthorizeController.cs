using RegistrationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RegistrationService.Services.Interface;

namespace RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly Services.Interface.IAuthorizationService _authorization;
        private readonly IVerifyService _verifyService;
        private readonly IPasswordService _passwordService;
        //private readonly ISendMessageService _sendMessageService;
        public AuthorizeController(Services.Interface.IAuthorizationService authorization, 
            IVerifyService verifyService, IPasswordService passwordService)
        {
            _authorization = authorization;
            _verifyService = verifyService;
            _passwordService = passwordService;
            //_sendMessageService = sendMessageService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<DataTokens>> Login(LoginModel model)
        {
            try
            {
                List<string> tokens = await _authorization.Login(model);
                //первым элементом идет токен доступа, втрым в списке идт токен перевыска
                var dataTokens = new DataTokens() { AccessToken = tokens[0], RefreshToken = tokens[1], ErrorMessage = string.Empty };
                return Ok(dataTokens);
            }
            catch (Exception ex)
            {
                return BadRequest(new DataTokens() { AccessToken = string.Empty, RefreshToken = string.Empty, ErrorMessage = ex.Message });
            }
        }

        [HttpPost("regitration")]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            try
            {
                return Ok(await _authorization.Registration(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            try
            {
                return Ok(await _verifyService.VerifyEmail(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return Ok(await _passwordService.ResetPasswordAsync(resetPasswordModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                return Ok(await _authorization.CreateVerificationPasswordToken(email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<DataTokens>> RefreshToken(RefreshTokenModel model)
        {
            try
            {
                List<string> tokens = await _authorization.RefreshToken(model);
                //первым элементом идет токен доступа, втрым в списке идт токен перевыска
                var dataTokens = new DataTokens() { AccessToken = tokens[0], RefreshToken = tokens[1], ErrorMessage = string.Empty};
                return Ok(dataTokens);
            }
            catch (Exception ex)
            {

                return BadRequest(new DataTokens() { AccessToken = string.Empty, RefreshToken = string.Empty, ErrorMessage = ex.Message });
            }
        }

        //[HttpPost("send-message")]
        //public async Task<IActionResult> RemoveAccount(MessageModel model)
        //{
        //    try
        //    {
        //        var answer = await _sendMessageService.SendMessage(model.Message);

        //        return Ok(answer);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}

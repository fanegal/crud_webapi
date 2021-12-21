using crud_webapi.Auth;
using crud_webapi.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business;

namespace crud_webapi.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IJwtAuthenticationService _authService;

        public DefaultController(ILogger<DefaultController> logger, IJwtAuthenticationService authService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet]
        public object Get()
        {
            string responseObject;
            B_Property neg;
            try
            {
                neg = new B_Property();
                neg.nuevo();
                if (neg.bit_error) { throw neg.error; }
                responseObject = "Insertado";
            }
            catch (Exception ex)
            {
                responseObject = ex.ToString();
            }


            _logger.LogInformation($"Status: {responseObject}");

            return responseObject;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthInfo user)
        {
            var token = _authService.Authenticate(user.Username, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }


        
    }
}
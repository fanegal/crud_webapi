using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestasController : ControllerBase
    {
        [HttpGet("{id}")]
        public string get(int id)
        {
            return "Pagina en construcción parametro recibido :" + id.ToString();
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using TinyErpWebService.Models;

namespace TinyErpWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ItemController : ControllerBase
    {
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(id);
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Deu bom");
    }
}
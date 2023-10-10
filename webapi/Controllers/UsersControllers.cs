﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly DataContext _context;

		public UsersController(DataContext context)
		{
			_context = context;
		}

		[HttpPost("authenticate")]

		public async Task<IActionResult> Authentice([FromBody] Users userObj)
		{
			if (userObj == null)
			{
				return BadRequest();
			}
			var users = await _context.users.FirstOrDefaultAsync(x => x.email == userObj.email && x.password == userObj.password);
			if (users == null)
			{
				return NotFound(new {message = "User Not Found"});
			}
			return Ok(new 
			{
				message = "Login Success!!!"
			});	
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser([FromBody] Users userObj)
		{
			if(userObj == null)
			{
				return BadRequest();
			}
			await _context.users.AddAsync(userObj);
			await _context.SaveChangesAsync();
			return Ok(new
			{
				Message = "User Registered!"
			});
		}
	}
}

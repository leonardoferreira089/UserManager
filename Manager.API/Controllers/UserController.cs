using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Domain.Exceptions;
using Manager.Service.DTO;
using Manager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel viewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(viewModel);
                var userCreated = await _userService.CreateUserAsync(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário criado com sucesso!",
                    Sucess = true,
                    Data = userCreated,
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }                    
    }
}

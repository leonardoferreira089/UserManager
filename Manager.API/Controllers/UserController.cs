using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Domain.Exceptions;
using Manager.Service.DTO;
using Manager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        [Route("/api/v1/users/all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _userService.GetAllAsync();

                if (allUsers.Count == 0)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário encontrado",
                        Sucess = true,
                        Data = null
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuários encontrados com sucesso!",
                    Sucess = true,
                    Data = allUsers
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

        [HttpGet]
        [Route("/api/v1/users/get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o ID informado.",
                        Sucess = true,
                        Data = user
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = user
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

        [HttpPut]
        [Route("/api/v1/users/update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);

                var userUpdated = await _userService.UpdateUserAsync(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário atualizado com sucesso!",
                    Sucess = true,
                    Data = userUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("/api/v1/users/get-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetByEmail(email);

                if (user == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado.",
                        Sucess = true,
                        Data = user
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = user
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

        [HttpGet]
        [Route("/api/v1/users/search-by-email")]
        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            try
            {
                var allUsers = await _userService.SearchByEmail(email);

                if (allUsers.Count == 0)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado.",
                        Sucess = true,
                        Data = null
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = allUsers
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

        [HttpGet]
        [Route("/api/v1/users/search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var allUsers = await _userService.SearchByName(name);

                if (allUsers.Count == 0)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o nome informado",
                        Sucess = true,
                        Data = null
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return Ok(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Route("/api/v1/users/remove/{id}")]
        public async Task<IActionResult> RemoveUser(long id)
        {
            try
            {
                await _userService.RemoveUserAsync(id);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário removido com sucesso!",
                    Sucess = true,
                    Data = null
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

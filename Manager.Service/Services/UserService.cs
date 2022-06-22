using AutoMapper;
using Manager.Domain.Entities;
using Manager.Domain.Exceptions;
using Manager.Infra.Interfaces;
using Manager.Service.DTO;
using Manager.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);
            if(userExists != null)
            {
                throw new DomainException("Já existe um usuário cadastrado com o email informado.");
            }

            var user = _mapper.Map<User>(userDTO);
            user.Validate();

            var userCreated = await _userRepository.CreateAsync(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            
            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            return _mapper.Map<UserDTO>(user);
        }

        public async Task RemoveUserAsync(long id)
        {
            await _userRepository.RemoveAsync(id);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var allUsers = await _userRepository.GetSearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            var allUsers = await _userRepository.SearchByName(name);
            
            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByIdAsync(userDTO.Id);

            if (userExists == null)
            {
                throw new DomainException("Não existe nenhum usuário com o id informado!");
            }
                
            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            var userUpdated = await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dtos.Character;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace project.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newChar)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newChar);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.characters.AddAsync(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _context.characters.Where(c=>c.UserId==GetUserId()).Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

    

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacter =await _context.characters.Where(s=>s.User.Id==GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.characters.FirstOrDefaultAsync(s => s.Id == id && s.User.Id==GetUserId());
            serviceResponse.Data =_mapper.Map<GetCharacterDto>( dbCharacter );
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacterDto(UpdateCharacterDto updatechar)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
            Character character =await _context.characters.Include(v=>v.User).FirstOrDefaultAsync(f => f.Id == updatechar.Id);
            if (character.User.Id==GetUserId())
                {
                character.Name = updatechar.Name;
                character.Class = updatechar.Class;
                character.Defense = updatechar.Defense;
                character.HitPoints = updatechar.HitPoints;
                character.Intelligence = updatechar.Intelligence;
                character.Strength = updatechar.Strength;

                _context.characters.Update(character);
                await _context.SaveChangesAsync();
                 serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                 else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not Found" + _context.characters.Include(s => s.User).Count();// remove count
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character =await _context.characters.FirstOrDefaultAsync(f => f.Id == id && f.User.Id==GetUserId());
                if (character!=null)
                {
                _context.characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.characters.Where(c=>c.User.Id==GetUserId()).Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();

                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not Found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

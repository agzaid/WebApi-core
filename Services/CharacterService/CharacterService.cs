using AutoMapper;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dtos.Character;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newChar)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newChar);

            await _context.characters.AddAsync(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _context.characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

    

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacter =await _context.characters.Where(s=>s.User.Id==userId).ToListAsync();
            serviceResponse.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.characters.FirstOrDefaultAsync(s => s.Id == id);
            serviceResponse.Data =_mapper.Map<GetCharacterDto>( dbCharacter );
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacterDto(UpdateCharacterDto updatechar)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
            Character character =await _context.characters.FirstOrDefaultAsync(f => f.Id == updatechar.Id);
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
                Character character =await _context.characters.FirstAsync(f => f.Id == id);
                _context.characters.Remove(character);

                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();

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

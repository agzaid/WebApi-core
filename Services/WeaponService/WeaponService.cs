using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Dtos.Character;
using project.Dtos.Weapon;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace project.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext Context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _Context = Context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _Context.characters.FirstOrDefaultAsync(x => x.Id == newWeapon.CharacterId &&
                 x.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (character==null)
                {
                    response.Success = false;
                    response.Message = "Character not Found";
                    return response;
                }
                Weapon weapon = new Weapon
                {
                    Name=newWeapon.Name,
                    Damage=newWeapon.Damage,
                    CharacterId=newWeapon.CharacterId  //in video character not characterId
                };
                await _Context.Weapons.AddAsync(weapon);
                await _Context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

using AutoMapper;
using project.Dtos.Character;
using project.Dtos.skill;
using project.Dtos.Weapon;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>().ForMember(dto=>dto.Skills, c=>c.MapFrom(s=>s.CharacterSkills.Select(cs=>cs.Skill)));
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}

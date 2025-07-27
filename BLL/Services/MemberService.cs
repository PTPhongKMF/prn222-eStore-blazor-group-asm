using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services {
    public class MemberService {
        private readonly MemberRepository memberRepository;
        private readonly IMapper mapper;

        public MemberService(MemberRepository memberRepository, IMapper mapper) {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        public async Task<MemberDTO?> Login(MemberDTO member) {
            Member? user = await memberRepository.Login(mapper.Map<Member>(member));

            return mapper.Map<MemberDTO>(user);
        }

        public async Task<MemberDTO?> Register(MemberDTO member) {
            Member? user = await memberRepository.Register(mapper.Map<Member>(member));

            return mapper.Map<MemberDTO>(user);
        }
    }
}

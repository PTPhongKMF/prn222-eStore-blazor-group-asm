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

        public async Task<List<MemberDTO>> GetAll() {
            var users = await memberRepository.GetAll();
            return users.Select(u => mapper.Map<MemberDTO>(u)).ToList();
        }

        public async Task<MemberDTO?> GetById(int memberId) {
            var user = await memberRepository.GetById(memberId);
            return mapper.Map<MemberDTO>(user);
        }

        public async Task Add(MemberDTO member) {
            var entity = mapper.Map<Member>(member);
            await memberRepository.Add(entity);
        }

        public async Task Update(MemberDTO member) {
            var entity = mapper.Map<Member>(member);
            await memberRepository.Update(entity);
        }

        public async Task Delete(int memberId) {
            await memberRepository.Delete(memberId);
        }

        public async Task<MemberDTO?> Register(MemberDTO member) {
            Member? user = await memberRepository.Register(mapper.Map<Member>(member));

            return mapper.Map<MemberDTO>(user);
        }
    }
}

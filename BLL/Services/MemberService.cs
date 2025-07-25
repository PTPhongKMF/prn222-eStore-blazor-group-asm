using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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

        public async Task<(bool Success, string Message, MemberDTO? Member)> UpdateMemberAsync(MemberDTO memberDto)
        {
            var validationContext = new ValidationContext(memberDto);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(memberDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                return (false, errorMessages, null);
            }

            var memberEntity = mapper.Map<Member>(memberDto);
            var updatedMember = await memberRepository.UpdateMemberAsync(memberEntity);
            if (updatedMember == null)
                return (false, "Member not found or update failed.", null);

            return (true, "Profile updated successfully.", mapper.Map<MemberDTO>(updatedMember));
        }

        // Example stub for loading current member (replace with your logic)
        public async Task<MemberDTO?> GetCurrentMemberAsync()
        {
            int currentMemberId = 1; 
            var member = await memberRepository.GetMemberByIdAsync(currentMemberId);
            return member == null ? null : mapper.Map<MemberDTO>(member);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories {
    public class MemberRepository {
        private readonly EStoreDatabaseContext dbContext;

        public MemberRepository(EStoreDatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<Member?> Login(Member member) {
            Member? user = await dbContext.Member
                .FirstOrDefaultAsync(m => 
                    m.Email.ToLower() == member.Email.ToLower() && 
                    m.Password == member.Password
                );

            return user;
        }

        public async Task<Member?> UpdateMemberAsync(Member member)
        {
            var existingMember = await dbContext.Member.FindAsync(member.MemberId);
            if (existingMember == null) return null;

            existingMember.Email = member.Email;
            existingMember.CompanyName = member.CompanyName;
            existingMember.City = member.City;
            existingMember.Country = member.Country;
            existingMember.Password = member.Password;

            await dbContext.SaveChangesAsync();
            return existingMember;
        }

        public async Task<Member?> GetMemberByIdAsync(int memberId)
        {
            return await dbContext.Member.FindAsync(memberId);
        }
    }
}

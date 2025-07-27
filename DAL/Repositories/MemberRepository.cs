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

        public async Task<Member?> Register(Member member) {
            bool emailExists = await dbContext.Member
                .AnyAsync(m => m.Email.ToLower() == member.Email.ToLower());

            if (emailExists) {
                return null;
            }

            await dbContext.Member.AddAsync(member);
            await dbContext.SaveChangesAsync();

            return member;
        }
    }
}

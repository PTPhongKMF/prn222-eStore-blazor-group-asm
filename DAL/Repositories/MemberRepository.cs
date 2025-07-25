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

        // CREATE
        public async Task<Member> Add(Member member)
        {
            dbContext.Member.Add(member);
            await dbContext.SaveChangesAsync();
            return member;
        }

        // READ ALL
        public async Task<List<Member>> GetAll()
        {
            return await dbContext.Member.ToListAsync();
        }

        // READ BY ID
        public async Task<Member?> GetById(int memberId)
        {
            return await dbContext.Member.FindAsync(memberId);
        }

        // UPDATE
        public async Task Update(Member member)
        {
            var trackedEntity = dbContext.ChangeTracker.Entries<Member>()
                .FirstOrDefault(e => e.Entity.MemberId == member.MemberId);

            if (trackedEntity != null)
            {
                // Detach the already tracked entity
                trackedEntity.State = EntityState.Detached;
            }

            dbContext.Member.Attach(member);
            dbContext.Entry(member).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }

        // DELETE
        public async Task Delete(int memberId)
        {
            var member = await dbContext.Member.FindAsync(memberId);
            if (member != null)
            {
                dbContext.Member.Remove(member);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

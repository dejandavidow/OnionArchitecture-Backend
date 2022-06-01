using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Contracts.Exceptions;

internal sealed class MemberRepository : IMemberRepository
{
    private readonly RepositoryDbContext _dbContext;
    public MemberRepository(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SearchCountAsync(string search)
    {
        if(String.IsNullOrEmpty(search))
        {
            return await _dbContext.Members.CountAsync();
        }
        return await _dbContext.Members.Where(x => x.Name.Contains(search)).CountAsync();
    }
    public async Task<int> FilterCountAsync(string letter)
    {
        return await _dbContext.Members.Where(x => x.Name.StartsWith(letter)).CountAsync();
    }
    public async Task<IEnumerable<Member>> FilterAsync(MemberParams memberParams, string letter)
    {
        return (await _dbContext.Members.Where(x => x.Name.StartsWith(letter)).AsNoTracking()
         .OrderBy(x => x.Name)
         .Skip((memberParams.PageNumber - 1) * memberParams.PageSize)
         .Take(memberParams.PageSize)
         .ToListAsync())
         .Select(member => new Member(member.Id, member.Name, member.Username, member.Email, member.Hours, member.Status, member.Role));
    }
    public async Task<IEnumerable<Member>> SearchAsync(MemberParams memberParams, string search)
    {
        return (await _dbContext.Members.Where(x => x.Name.Contains(search)).AsNoTracking()
         .OrderBy(x => x.Name)
         .Skip((memberParams.PageNumber - 1) * memberParams.PageSize)
         .Take(memberParams.PageSize)
         .ToListAsync())
         .Select(member => new Member(member.Id, member.Name, member.Username, member.Email, member.Hours, member.Status, member.Role));
    }
    public async Task<Member> GetMemberById(Guid id, CancellationToken cancellationToken = default)
    {
       var member = await _dbContext.Members.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
       if(member == null)
       {
           throw new NotFoundException("Member not found");
       }
       return new Member(member.Id,member.Name,member.Username,member.Email,member.Hours,member.Status,member.Role);
    }

    public async Task<IEnumerable<Member>> GetMembersAsync(MemberParams memberParams,CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Members.AsNoTracking()
            .OrderBy(x => x.Name)
            .Skip((memberParams.PageNumber - 1) * memberParams.PageSize)
            .Take(memberParams.PageSize)
            .ToListAsync(cancellationToken))
            .Select(member => new Member(member.Id,member.Name,member.Username,member.Email,member.Hours,member.Status,member.Role));
    }

    public async Task InsertMember(Member member, CancellationToken cancellationToken)
    {
        var checkmember = await _dbContext.Members.AsNoTracking().FirstOrDefaultAsync(x => x.Id == member.Id,cancellationToken);
        if(checkmember != null)
        {
            throw new EntityAlreadyExists("Member already exists.");
        }
        await _dbContext.Members.AddAsync(new Persistence.Models.PersistenceMember(){
            Id = member.Id,
            Name = member.Name,
            Username = member.Username,
            Email = member.Email,
            Hours = member.Hours,
            Status = member.Status,
            Role = member.Role
        });
    }
    public void RemoveMember(Member member)
    {
        _dbContext.Members.Remove(new Persistence.Models.PersistenceMember(){
            Id = member.Id,
            Name = member.Name,
            Username = member.Username,
            Email = member.Email,
            Hours = member.Hours,
            Status = member.Status,
            Role = member.Role
        });
    }

    public async Task UpdateMember(Member member,CancellationToken cancellationToken)
    {
        var memberforupdate = await _dbContext.Members.FirstOrDefaultAsync( x => x.Id == member.Id,cancellationToken);
        memberforupdate.Id = member.Id;
        memberforupdate.Name = member.Name;
        memberforupdate.Username = member.Username;
        memberforupdate.Email = member.Email;
        memberforupdate.Hours = member.Hours;
        memberforupdate.Status = member.Status;
        memberforupdate.Role = member.Role;
    }
}
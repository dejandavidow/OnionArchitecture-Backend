
using Contracts.Exceptions;
using Domain.Entities;
using Domain.Pagination;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

internal sealed class MemberRepository : IMemberRepository
{
    private readonly RepositoryDbContext _dbContext;
    public MemberRepository(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Member> GetMemberWithIdAndCheckPassword(string id, string password)
    {
        var memberPW = await  _dbContext.Members.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        if(memberPW.Password != password)
        {
            throw new NotFoundException("Old password is wrong");
        }
        return new Member(memberPW.Id, memberPW.Name, memberPW.Username, memberPW.Email, memberPW.Hours, memberPW.Status, memberPW.Role, memberPW.Password);
    }
    public async Task UpdatePasswordsAsync(string token,string password)
    {
        var memberup = await _dbContext.Members.FirstOrDefaultAsync(x => x.ResetToken == token);
        memberup.Password = password;
        memberup.ResetToken = null;
    }
    public async Task<Member> GetMemberWithToken(string token)
    {
        var tokenmember = await _dbContext.Members.FirstOrDefaultAsync(x => x.ResetToken == token);
        if(tokenmember == null)
        {
            throw new NotFoundException("Bad token");
        }
        return new Member(tokenmember.Id, tokenmember.Name, tokenmember.Username, tokenmember.Email, tokenmember.Hours, tokenmember.Status, tokenmember.Role, tokenmember.Password);
    }
    public async Task UpdateWithToken(Member member,string token)
    {
        var membertoken = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == member.Id);
        membertoken.ResetToken = token;
    }
    public async Task UpdatePasswordAsync(Member member)
    {
       var memberforChange = await _dbContext.Members.FirstOrDefaultAsync(x => x.Email == member.Email);
        memberforChange.Password = member.Password;
    }
    public async Task<Member> GetMemberByEmail(string email)
    {
        var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Email == email);
        if(member == null)
        {
            throw new NotFoundException("Bad email adress.");
        }
        return new Member(member.Id, member.Name, member.Username, member.Email, member.Hours, member.Status, member.Role, member.Password);
    }
    public string Generate(Member member)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:44381/",
            audience: "https://localhost:44381/",
            claims: new[] 
            {
            new Claim(ClaimTypes.NameIdentifier,member.Username),
            new Claim(ClaimTypes.Name,member.Name),
            new Claim(ClaimTypes.Role,member.Role)
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signinCredentials

        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }
    public async Task<Member> Authenticate(string username, string password)
    {
        var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        if (member != null)
        {
            return new Member(member.Id, member.Name, member.Username, member.Email, member.Hours, member.Status, member.Role, member.Password);
        }
        throw new AuthException("Wrong username or password!");
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
         .Select(member => new Member(member.Id, member.Name, member.Username, member.Email, member.Hours, member.Status, member.Role,member.Password));
    }
    public async Task<IEnumerable<Member>> SearchAsync(MemberParams memberParams, string search)
    {
        return (await _dbContext.Members.Where(x => x.Name.Contains(search)).AsNoTracking()
         .OrderBy(x => x.Name)
         .Skip((memberParams.PageNumber - 1) * memberParams.PageSize)
         .Take(memberParams.PageSize)
         .ToListAsync())
         .Select(member => new Member(member.Id, member.Name, member.Username, member.Email, member.Hours, member.Status, member.Role, member.Password));
    }
    public async Task<Member> GetMemberById(Guid id, CancellationToken cancellationToken = default)
    {
       var member = await _dbContext.Members.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
       if(member == null)
       {
           throw new NotFoundException("Member not found");
       }
       return new Member(member.Id,member.Name,member.Username,member.Email,member.Hours,member.Status,member.Role, member.Password);
    }

    public async Task<IEnumerable<Member>> GetMembersAsync(MemberParams memberParams,CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Members.AsNoTracking()
            .OrderBy(x => x.Name)
            .Skip((memberParams.PageNumber - 1) * memberParams.PageSize)
            .Take(memberParams.PageSize)
            .ToListAsync(cancellationToken))
            .Select(member => new Member(member.Id,member.Name,member.Username,member.Email,member.Hours,member.Status,member.Role, member.Password));
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
            Role = member.Role,
            Password = member.Password
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
            Role = member.Role,
            Password = member.Password
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
        memberforupdate.Password = member.Password;
    }
}
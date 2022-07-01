
using Contracts.Auth;
using Contracts.DTOs;
using Contracts.Exceptions;
using Contracts.ResetPassword;
using Domain.Entities;
using Domain.Pagination;
using Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Services
{
    internal sealed class MemberService : IMemberService
    {
        private readonly IRepositoryManager _repositoryManager;
      
        public MemberService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task UpdatePassword(ResetPasswordModel model,CancellationToken cancellationToken)
        {
            try
            {
                var member = (await _repositoryManager.MemberRepository.GetMemberByEmail(model.Email))
                    .UpdatePassword(model.Password);
                await _repositoryManager.MemberRepository.UpdatePasswordAsync(member);
                await _repositoryManager.SaveChangesAsync(cancellationToken);

            }
            catch (NotFoundException)
            {
                throw;
            }
        }
        public async Task<GetMemberDTO> GetMemberByEmail(string email)
        {
            try
            {
                var member = await _repositoryManager.MemberRepository.GetMemberByEmail(email);
                return new GetMemberDTO()
                {
                    Id = member.Id.ToString(),
                    Name = member.Name,
                    Username = member.Username,
                    Email = member.Email,
                    Hours = member.Hours,
                    Status = member.Status,
                    Role = member.Role
                };

            }
            catch (NotFoundException)
            {
                throw;
            }
        }
        public async Task<AuthResponse> Authenticate(string username,string password)
        {
            try
            {
                var member = await  _repositoryManager.MemberRepository.Authenticate(username, password);
                return new AuthResponse
                {
                    Name = member.Name,
                    Role = member.Role,
                    AccessToken = _repositoryManager.MemberRepository.Generate(member)
                };
            }
            catch(AuthException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<int> SearchCountAsync(string search)
        {
                return await _repositoryManager.MemberRepository.SearchCountAsync(search);
        }
        public async Task<int> FilterCountAsync(string letter)
        {
            return await  _repositoryManager.MemberRepository.FilterCountAsync(letter);
        }
        public async Task<IEnumerable<GetMemberDTO>> FilterAsync(MemberParams memberParams, string letter)
        {
            var result = (await _repositoryManager.MemberRepository.FilterAsync(memberParams, letter)).Select(member => new GetMemberDTO()
            {
                Id = member.Id.ToString(),
                Name = member.Name,
                Username = member.Username,
                Email = member.Email,
                Hours = member.Hours,
                Status = member.Status,
                Role = member.Role
            });
            return result;
        }
        public async Task<IEnumerable<GetMemberDTO>> SearchAsync(MemberParams memberParams,string search)
        {
            var result = (await _repositoryManager.MemberRepository.SearchAsync(memberParams,search)).Select(member => new GetMemberDTO()
            {
                Id = member.Id.ToString(),
                Name = member.Name,
                Username = member.Username,
                Email = member.Email,
                Hours = member.Hours,
                Status = member.Status,
                Role = member.Role
            });
            return result;
        }
        public async Task CreateAsync(MemberDTO memberDTO, CancellationToken cancellationToken = default)
        {
              try
            {
            var member = new Member(Guid.NewGuid(),memberDTO.Name,memberDTO.Username,memberDTO.Email,memberDTO.Hours,memberDTO.Status,memberDTO.Role,memberDTO.Password); 
            await _repositoryManager.MemberRepository.InsertMember(member, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(EntityAlreadyExists)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try{
            var result = await _repositoryManager.MemberRepository.GetMemberById(id,cancellationToken);
            _repositoryManager.MemberRepository.RemoveMember(result);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetMemberDTO>> GetAllAsync(MemberParams memberParams,CancellationToken cancellationToken = default)
        {
            var result = (await _repositoryManager.MemberRepository.GetMembersAsync(memberParams,cancellationToken)).Select(member => new GetMemberDTO() {
            Id = member.Id.ToString(),
            Name = member.Name,
            Username = member.Username,
            Email = member.Email,
            Hours = member.Hours,
            Status = member.Status,
            Role = member.Role
        });
            return result;
        }

        public async Task<GetMemberDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
            var result = await _repositoryManager.MemberRepository.GetMemberById(id,cancellationToken);
            var memberDTO = new GetMemberDTO()
            {
                Id = result.Id.ToString(),
                Name = result.Name,
                Username = result.Username,
                Email = result.Email,
                Hours = result.Hours,
                Status = result.Status,
                Role = result.Role
            };
            return memberDTO;
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task UpdateAsync(Guid id,UpdateMemberDTO memberDTO,CancellationToken cancellationToken = default)
        {
            try
            {
                //var checkhours = memberDTO.Hours > 0 ? memberDTO.Hours : 0;
                var memberforupdate = (await _repositoryManager.MemberRepository.GetMemberById(id, cancellationToken))
                .UpdateName(memberDTO.Name)
                .UpdateEmail(memberDTO.Email)
                .UpdateHours(memberDTO.Hours)
                .UpdateUserName(memberDTO.Username)
                .UpdateStatus(memberDTO.Status)
                .UpdateRole(memberDTO.Role)
                .UpdatePassword(memberDTO.Password);
            await _repositoryManager.MemberRepository.UpdateMember(memberforupdate,cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
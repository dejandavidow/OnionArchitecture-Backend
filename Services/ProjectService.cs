
using Contracts.DTOs;
using Contracts.Exceptions;
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
    internal sealed class ProjectService : IProjectService
    {
        private readonly IRepositoryManager _repositoryManager;
        public ProjectService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<int> CountSearchProjects(string search)
        {
            return await  _repositoryManager.ProjectRepository.CountSearchProjects(search);
        }

        public async Task<int> CountFilterProjects(string letter)
        {
            return await _repositoryManager.ProjectRepository.CountFilterProjects(letter);
        }
        public async Task<IEnumerable<ProjectDTO>> SearchProjects(ProjectParams projectParams, string search)
        {
            return (await _repositoryManager.ProjectRepository.FilterProjects(projectParams, search)).Select(project => new ProjectDTO()
            {
                Id = project.Id.ToString(),
                ProjectName = project.ProjectName,
                Description = project.Description,
                Archive = project.Archive,
                Status = project.Status,
                ClientDTO = new ClientDTO { Id = project.Client.Id.ToString(), ClientName = project.Client.ClientName, Adress = project.Client.Adress, City = project.Client.City, Country = project.Client.Country, PostalCode = project.Client.PostalCode },
                MemberDTO = new GetMemberDTO { Id = project.Member.Id.ToString(),Name = project.Member.Name,Username = project.Member.Username,Email = project.Member.Email,Status = project.Member.Status, Role = project.Member.Role,Hours = project.Member.Hours}
            });
        }
        public async Task<IEnumerable<ProjectDTO>> FilterProjects(ProjectParams projectParams, string letter)
        {
            return (await _repositoryManager.ProjectRepository.FilterProjects(projectParams,letter)).Select(project => new ProjectDTO()
            {
                Id = project.Id.ToString(),
                ProjectName = project.ProjectName,
                Description = project.Description,
                Archive = project.Archive,
                Status = project.Status,
                ClientDTO = new ClientDTO { Id = project.Client.Id.ToString(), ClientName = project.Client.ClientName, Adress = project.Client.Adress, City = project.Client.City, Country = project.Client.Country, PostalCode = project.Client.PostalCode },
                MemberDTO = new GetMemberDTO { Id = project.Member.Id.ToString(), Name = project.Member.Name, Username = project.Member.Username, Email = project.Member.Email, Status = project.Member.Status, Role = project.Member.Role, Hours = project.Member.Hours }
            });
        }
    
        public async Task CreateAsync(PostProjectDTO projectDTO, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = projectDTO.ClientId != null ? (await _repositoryManager.ClientRepository.GetByIdAsync(Guid.Parse(projectDTO.ClientId), cancellationToken)) : null;
                var member = projectDTO.MemberId!= null ? (await _repositoryManager.MemberRepository.GetMemberById(Guid.Parse(projectDTO.MemberId), cancellationToken)) : null;
                var domainproject = new Project(Guid.NewGuid(),projectDTO.ProjectName,projectDTO.Description,projectDTO.Archive,projectDTO.Status,client,member);
                await _repositoryManager.ProjectRepository.InsertProject(domainproject,cancellationToken);
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
            var projectfordelete = await _repositoryManager.ProjectRepository.GetProjectById(id,cancellationToken);
            _repositoryManager.ProjectRepository.RemoveProject(projectfordelete);
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

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync(ProjectParams projectParams,CancellationToken cancellationToken = default)
        {
        return (await _repositoryManager.ProjectRepository.GetProjectAsync(projectParams,cancellationToken)).Select(project => new ProjectDTO(){
           Id = project.Id.ToString(),
           ProjectName = project.ProjectName,
           Description = project.Description,
           Archive = project.Archive,
           Status = project.Status,
           ClientDTO = new ClientDTO { Id = project.Client.Id.ToString(), ClientName = project.Client.ClientName, Adress = project.Client.Adress, City = project.Client.City, Country = project.Client.Country, PostalCode = project.Client.PostalCode },
            MemberDTO = new GetMemberDTO { Id = project.Member.Id.ToString(), Name = project.Member.Name, Username = project.Member.Username ,Email = project.Member.Email, Status = project.Member.Status, Role = project.Member.Role, Hours = project.Member.Hours }
        });
        }

        public async Task<ProjectDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
            var project = await _repositoryManager.ProjectRepository.GetProjectById(id,cancellationToken);
            var projectDTO = new ProjectDTO()
            {
           Id = project.Id.ToString(),
           ProjectName = project.ProjectName,
           Description = project.Description,
           Archive = project.Archive,
           Status = project.Status,
           ClientDTO = new ClientDTO { Id = project.Client.Id.ToString(), ClientName = project.Client.ClientName, Adress = project.Client.Adress, City = project.Client.City, Country = project.Client.Country, PostalCode = project.Client.PostalCode },
           MemberDTO = new GetMemberDTO { Id = project.Member.Id.ToString(), Name = project.Member.Name, Username = project.Member.Username, Email = project.Member.Email, Status = project.Member.Status, Role = project.Member.Role, Hours = project.Member.Hours }
            };
            return projectDTO;
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
        public async Task UpdateAsync(Guid id,PostProjectDTO projectDTO,CancellationToken cancellationToken = default)
        {
            try
            {
                var client = projectDTO.ClientId != null?(await _repositoryManager.ClientRepository.GetByIdAsync(Guid.Parse(projectDTO.ClientId), cancellationToken)): null;
                var member = projectDTO.MemberId!= null?(await _repositoryManager.MemberRepository.GetMemberById(Guid.Parse(projectDTO.MemberId), cancellationToken)): null;
                var result = (await _repositoryManager.ProjectRepository.GetProjectById(id,cancellationToken)).
                 UpdatePname(projectDTO.ProjectName)
                .UpdateDescription(projectDTO.Description).
                 UpdateArchive(projectDTO.Archive)
                .UpdateStatus(projectDTO.Status).
                 UpdateClient(client)
                .UpdateMember(member);
                await _repositoryManager.ProjectRepository.UpdateProject(result,cancellationToken);
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
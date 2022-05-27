using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions;
using System.Collections.Generic;
using Domain;
using Contracts;
using Contracts.Exceptions;
using System.Linq;

namespace Services
{
    internal sealed class ProjectService : IProjectService
    {
        private readonly IRepositoryManager _repositoryManager;
        public ProjectService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task CreateAsync(ProjectDTO projectDTO, CancellationToken cancellationToken = default)
        {
            try{
                var domainproject = new Project(Guid.NewGuid(),projectDTO.ProjectName,projectDTO.Description,projectDTO.Archive,projectDTO.Archive,
                new Client(Guid.Parse(projectDTO.ClientDTO.Id),projectDTO.ClientDTO.ClientName,projectDTO.ClientDTO.Adress,projectDTO.ClientDTO.City,projectDTO.ClientDTO.PostalCode,projectDTO.ClientDTO.Country),
                new Member(Guid.Parse(projectDTO.MemberDTO.Id),projectDTO.MemberDTO.Name,projectDTO.MemberDTO.Username,projectDTO.MemberDTO.Email,projectDTO.MemberDTO.Hours,
                projectDTO.MemberDTO.Status,projectDTO.MemberDTO.Role
                ));
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

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
        return (await _repositoryManager.ProjectRepository.GetProjectAsync(cancellationToken)).Select(project => new ProjectDTO(){
           Id = project.Id.ToString(),
           ProjectName = project.ProjectName,
           Description = project.Description,
           Archive = project.Archive,
           Status = project.Status,
           ClientDTO = new ClientDTO()
           {
               Id = project.Id.ToString(),
               ClientName = project.Client.ClientName,
               Adress = project.Client.Adress,
               City = project.Client.City,
               PostalCode = project.Client.PostalCode,
               Country = project.Client.Country
           },
           MemberDTO = new MemberDTO()
           {
               Name = project.Member.Name,
               Username = project.Member.Username,
               Email = project.Member.Email,
               Hours = project.Member.Hours,
               Status = project.Member.Status,
               Role = project.Member.Role
           }
        });
        }

        public async Task<ProjectDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
            var result = await _repositoryManager.ProjectRepository.GetProjectById(id,cancellationToken);
            var projectDTO = new ProjectDTO()
            {
           Id = result.Id.ToString(),
           ProjectName = result.ProjectName,
           Description = result.Description,
           Archive = result.Archive,
           Status = result.Status,
           ClientDTO = new ClientDTO()
           {
               Id = result.Id.ToString(),
               ClientName = result.Client.ClientName,
               Adress = result.Client.Adress,
               City = result.Client.City,
               PostalCode = result.Client.PostalCode,
               Country = result.Client.Country
           },
           MemberDTO = new MemberDTO(){
               Id = result.Member.Id.ToString(),
               Name = result.Member.Name,
               Username = result.Member.Username,
               Email = result.Member.Email,
               Hours = result.Member.Hours,
               Status = result.Member.Status,
               Role = result.Member.Role
           }
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
        public async Task UpdateAsync(Guid id,ProjectDTO projectDTO,CancellationToken cancellationToken = default)
        {
            try
            {
                var client = projectDTO.ClientDTO!= null?(await _repositoryManager.ClientRepository.GetByIdAsync(Guid.Parse(projectDTO.ClientDTO.Id), cancellationToken)): null;
                var member = projectDTO.MemberDTO!= null?(await _repositoryManager.MemberRepository.GetMemberById(Guid.Parse(projectDTO.MemberDTO.Id), cancellationToken)): null;
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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain;

namespace Services.Abstractions
{
public interface IMemberService
    {
        Task<MemberDTO> Authenticate(string username,string password);
        Task<int> SearchCountAsync(string search);
        Task<int> FilterCountAsync(string letter);
        Task<IEnumerable<GetMemberDTO>> FilterAsync(MemberParams memberParams, string letter);
        Task<IEnumerable<GetMemberDTO>> SearchAsync(MemberParams memberParams, string search);
        Task<IEnumerable<GetMemberDTO>> GetAllAsync(MemberParams memberParams,CancellationToken cancellationToken = default);
        Task<GetMemberDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task CreateAsync(MemberDTO memberDTO,CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id,UpdateMemberDTO memberDTO,CancellationToken cancellationToken = default);
}

} 
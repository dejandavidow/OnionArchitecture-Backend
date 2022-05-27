using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
namespace Services.Abstractions
{
public interface IMemberService
{
Task<IEnumerable<MemberDTO>> GetAllAsync(CancellationToken cancellationToken = default);
Task<MemberDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
Task CreateAsync(MemberDTO memberDTO,CancellationToken cancellationToken = default);
Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
Task UpdateAsync(Guid id,MemberDTO memberDTO,CancellationToken cancellationToken = default);
}

} 
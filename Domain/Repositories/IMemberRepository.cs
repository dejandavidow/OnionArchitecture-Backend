using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

public interface IMemberRepository
{
Task<IEnumerable<Member>> GetMembersAsync(CancellationToken cancellationToken = default);
Task<Member> GetMemberById(Guid id,CancellationToken cancellationToken = default);
Task InsertMember(Member member,CancellationToken cancellationToken);
void RemoveMember(Member member);
Task UpdateMember(Member member,CancellationToken cancellationToken);
}

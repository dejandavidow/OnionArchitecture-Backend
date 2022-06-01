using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

public interface IMemberRepository
{
    Task<int> SearchCountAsync(string search);
    Task<int> FilterCountAsync(string letter);
    Task<IEnumerable<Member>> FilterAsync(MemberParams memberParams, string letter);
    Task<IEnumerable<Member>> SearchAsync(MemberParams memberParams,string search);
    Task<IEnumerable<Member>> GetMembersAsync(MemberParams memberParams,CancellationToken cancellationToken = default);
Task<Member> GetMemberById(Guid id,CancellationToken cancellationToken = default);
Task InsertMember(Member member,CancellationToken cancellationToken);
void RemoveMember(Member member);
Task UpdateMember(Member member,CancellationToken cancellationToken);
}

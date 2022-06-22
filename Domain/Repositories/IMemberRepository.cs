using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities;
using Domain.Pagination;

namespace Domain.Repositories
{
    public interface IMemberRepository
    {
        public string Generate();
        Task<Member> Authenticate(string username, string password);
        Task<int> SearchCountAsync(string search);
        Task<int> FilterCountAsync(string letter);
        Task<IEnumerable<Member>> FilterAsync(MemberParams memberParams, string letter);
        Task<IEnumerable<Member>> SearchAsync(MemberParams memberParams, string search);
        Task<IEnumerable<Member>> GetMembersAsync(MemberParams memberParams, CancellationToken cancellationToken = default);
        Task<Member> GetMemberById(Guid id, CancellationToken cancellationToken = default);
        Task InsertMember(Member member, CancellationToken cancellationToken);
        void RemoveMember(Member member);
        Task UpdateMember(Member member, CancellationToken cancellationToken);
    }

}
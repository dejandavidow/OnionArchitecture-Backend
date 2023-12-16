namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        ITimeSheetRepository TimeSheetRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IClientRepository ClientRepository { get; }
        IProjectRepository ProjectRepository { get; }
        void SaveChanges();
    }
}
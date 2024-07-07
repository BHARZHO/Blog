namespace BlogApplication.DAL.Contracts
{
    public interface IBase<TEntity, TCreateEntity, TEditEntity>
    {
        Task<TEntity?> GetById(Guid Id);
        Task<IEnumerable<TEntity>?> GetAll();
        Task<bool> Update(TEditEntity editEntity);
        Task<bool> Delete(Guid id);
        Task Create(TCreateEntity createEntity);

    }
}

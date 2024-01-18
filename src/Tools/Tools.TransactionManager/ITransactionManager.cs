namespace Tools.TransactionManager;

public interface ITransactionManager
{
    Task BeginTransactionAsync(Guid userId);
    Task BeginTransactionAsync(Guid userId, params Type[] contextName);
    /// <summary>
    /// Use
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<TransactionInfo> CommitTransactionAsync(Guid userId);
    Task<TransactionInfo> CommitTransactionAsync(Guid userId, params Type[] contextName);
    Task<TransactionInfo> RollbackTransactionAsync(Guid userId, Exception e);
    Task<TransactionInfo> RollbackTransactionAsync(Guid userId, Exception e, params Type[] contextName);
}

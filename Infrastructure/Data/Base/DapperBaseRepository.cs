// using System.Data;
// using AikoLearning.Core.Domain.Base;

// namespace AikoLearning.Infrastructure.Data.Base;

// public class DapperBaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel>
// {
//     private readonly IDbConnection dbConnection;

//     public DapperGenericRepository(IDbConnection dbConnection)
//     {
//         this.dbConnection = dbConnection;
//     }

//     public async Task<int> CountAsync()
//     {
//         var totalCountQuery = $"SELECT COUNT(1) FROM {typeof(TEntity).Name}";
//         return await dbConnection.ExecuteScalarAsync<int>(totalCountQuery);
//     }
// }
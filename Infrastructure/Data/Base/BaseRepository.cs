using AikoLearning.Core.Domain.Base;
using AikoLearning.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Base;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    #region Properties
    protected readonly ApplicationDbContext context;
    protected readonly DbSet<T> dbSet;
    #endregion

    #region Constructor
    public BaseRepository(ApplicationDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }
    #endregion

    #region Methods

    public async Task<IEnumerable<T>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T> Get(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<T> Insert(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        dbSet.Update(entity);
        return entity;
    }

    public async Task<T> Delete(int id)
    {
        var entity = await dbSet.FindAsync(id);
        
        dbSet.Remove(entity);
        return entity;
    }
    #endregion
}


// public abstract class Repository<T> : IRepository<T>
//     where T : class, IIdentifiable
// {
//     protected IUnitOfWork unitOfWork;
//     protected EntityMapper entityMapper;
//     protected bool isAutoIncrement = true;

//     public Repository(IUnitOfWork unitOfWork)
//     {
//         this.unitOfWork = unitOfWork;
//         this.entityMapper = EntityMapper.Get(typeof(T));
//     }

//     public virtual T Get(long id)
//     {
//         return BaseQuery($"t.{this.entityMapper.KeyColumn.ColumnName} = @ID", param: new { id }).FirstOrDefault();
//     }

//     public async Task<T> GetAsync(long id) =>
//         (await BaseQueryAsync($"t.{this.entityMapper.KeyColumn.ColumnName} = @ID", param: new {id}))
//             .FirstOrDefault();

//     public virtual List<T> GetAll() => BaseQuery();

//     public virtual Task<List<T>> GetAllAsync() => BaseQueryAsync();

//     public virtual List<T> GetAllByIDs(IEnumerable<long> ids)
//     {
//         if (ids.Any())
//         {
//             return BaseQuery($"t.id in ({string.Join(",", ids)})");
//         }
//         else
//         {
//             return new List<T>();
//         }
//     }

//     public virtual async Task<List<T>> GetAllByIDsAsync(IEnumerable<long> ids)
//     {
//         if (ids.Any())
//         {
//             return await BaseQueryAsync($"t.id in ({string.Join(",", ids)})");
//         }
//         else
//         {
//             return new List<T>();
//         }
//     }

//     public virtual List<T> GetAllByProperty(string propertyName, object value)
//     {
//         var columnInfo = this.entityMapper.Columns[propertyName.ToLower()];

//         if (value == null)
//         {
//             return BaseQuery($"t.{columnInfo.ColumnName} is null");
//         }
//         else
//         {
//             return BaseQuery($"t.{columnInfo.ColumnName} = @value", param: new { value });
//         }
//     }

//     public T GetFirstByProperty(string propertyName, object value, bool ignoreDeleted)
//     {
//         var columnInfo = entityMapper.Columns[propertyName.ToLower()];

//         if (value == null)
//         {
//             return ignoreDeleted ? BaseQuery($"t.{columnInfo.ColumnName} IS NULL AND deletedat IS NULL").FirstOrDefault() : BaseQuery($"t.{columnInfo.ColumnName} IS NULL").FirstOrDefault();
//         }
        
//         return ignoreDeleted ? BaseQuery($"t.{columnInfo.ColumnName} = @value AND deletedat IS NULL", param: new {value}).FirstOrDefault() : BaseQuery($"t.{columnInfo.ColumnName} = @value", param: new { value }).FirstOrDefault();
//     }

//     private async Task<long> InsertInternalAsync(T entity, bool returnId, bool sync)
//     {
//         var valueColumns = this.entityMapper.Columns.Values.Where(p => p.Insertable);

//         if (isAutoIncrement)
//         {
//             valueColumns = valueColumns.Where(p => !p.IsKey);
//         }

//         string query = $"insert into {this.entityMapper.TableName} ({string.Join(",", valueColumns.Select(p => p.ColumnName))}) values ({string.Join(",", valueColumns.Select(p => "@" + p.PropertyName))})";

//         if (returnId)
//         {
//             query += $" returning {this.entityMapper.KeyColumn.ColumnName}";
//             entity.ID = sync ? this.unitOfWork.Connection.ExecuteScalar<long>(query, entity) :
//                 await this.unitOfWork.Connection.ExecuteScalarAsync<long>(query, entity);
//             this.AfterInsert(entity);
//             return entity.ID.Value;
//         }
//         else
//         {
//             long rowsAffected = 0;
//             if (sync)
//             {
//                 rowsAffected = this.unitOfWork.Connection.Execute(query, entity);
//             }
//             else
//             {
//                 rowsAffected = await this.unitOfWork.Connection.ExecuteAsync(query, entity);
//             }
//             this.AfterInsert(entity);
//             return rowsAffected;
//         }
//     }
    
//     public virtual long Insert(T entity, bool returnId = true) => InsertInternalAsync(entity, returnId, true).GetAwaiter().GetResult();
//     public virtual Task<long> InsertAsync(T entity, bool returnId = true) => InsertInternalAsync(entity, returnId, false);

//     public virtual List<T> BulkInsert(List<T> entities)
//     {
//         var valueColumns = this.entityMapper.Columns.Values.Where(p => p.Insertable);

//         if (isAutoIncrement)
//         {
//             valueColumns = valueColumns.Where(p => !p.IsKey);
//         }

//         string query = $"insert into {this.entityMapper.TableName} ({string.Join(",", valueColumns.Select(p => p.ColumnName))}) " +
//                         $"values ({string.Join(",", valueColumns.Select(p => "@" + p.PropertyName))}) " +
//                         $"returning {this.entityMapper.KeyColumn.ColumnName}";

//         foreach (var entity in entities)
//         {
//             entity.ID = this.unitOfWork.Connection.Query<long>(query, entity).First();
//             AfterInsert(entity);
//         }

//         return entities;
//     }

//     private async Task UpdatePropertiesInternalAsync(T entity, HashSet<string> properties, bool sync)
//     {
//         var valueColumns = this.entityMapper.Columns.Values.Where(p => !p.IsKey && p.Updatable && (properties == null || properties.Contains(p.PropertyName)));

//         var sql = $"update {this.entityMapper.TableName} set {string.Join(",", valueColumns.Select(p => p.ColumnName + "= @" + p.PropertyName))} where {this.entityMapper.KeyColumn.ColumnName} = @ID";

//         if (sync)
//         {
//             this.unitOfWork.Connection.Execute(sql, entity);
//         }
//         else
//         {
//             await this.unitOfWork.Connection.ExecuteAsync(sql, entity);
//         }

//         this.AfterUpdate(entity);
//     }
    
//     public virtual void UpdateProperties(T entity, HashSet<string> properties = null) =>
//         UpdatePropertiesInternalAsync(entity, properties, true).GetAwaiter().GetResult();
//     public virtual Task UpdatePropertiesAsync(T entity, HashSet<string> properties = null) =>
//         UpdatePropertiesInternalAsync(entity, properties, true);

//     public void Update(T entity)
//     {
//         this.UpdateProperties(entity);
//     }

//     public Task UpdateAsync(T entity) => UpdatePropertiesAsync(entity);

//     public void UpdateProperties2(T entity, params string[] properties)
//     {
//         this.UpdateProperties(entity, new HashSet<string>(properties));
//     }

//     public virtual void Delete(long id)
//     {
//         this.unitOfWork.Connection.ExecuteScalar(
//             $"delete from {this.entityMapper.TableName} WHERE {this.entityMapper.KeyColumn.ColumnName} = @ID",
//             new { ID = id });

//         this.AfterDelete(id);
//     }

//     public virtual void BulkDelete(List<long> ids)
//     {
//         this.unitOfWork.Connection.ExecuteScalar(
//             $"delete from {this.entityMapper.TableName} WHERE {this.entityMapper.KeyColumn.ColumnName} IN ({string.Join(", ", ids)})");

//         foreach (var id in ids)
//         {
//             this.AfterDelete(id);
//         }
//     }

//     protected virtual string SelectFrom
//     {
//         get
//         {
//             return $"select {string.Join(",", this.entityMapper.Columns.Select(p => p.Value.ColumnName))} from {this.entityMapper.TableName} t";
//         }
//     }
    
    
//     protected async Task<List<T>> BaseQueryAsync(string where = null, string orderBy = null, int? offset = null, int? limit = null, object param = null)
//     {
//         var entities = await BaseQueryInternalAsync(where, orderBy, offset, limit, param);
//         entities.ForEach(AfterSelect);
//         return entities.ToList();
//     }

//     protected virtual List<T> BaseQuery(string where = null, string orderBy = null, int? offset = null, int? limit = null, object param = null)
//     {
//         var entities = BaseQueryInternal(where, orderBy, offset, limit, param);
//         entities.ForEach(AfterSelect);
//         return entities.ToList();
//     }

//     protected virtual async Task<IEnumerable<T>> BaseQueryInternalAsync(string where = null, string orderBy = null, int? offset = null, int? limit = null, object param = null)
//     {
//         string query = SelectQuery(where, orderBy, offset, limit);
//         return await this.unitOfWork.Connection.QueryAsync<T>(query, param);
//     }

//     protected virtual IEnumerable<T> BaseQueryInternal(string where = null, string orderBy = null, int? offset = null, int? limit = null, object param = null)
//     {
//         string query = SelectQuery(where, orderBy, offset, limit);
//         return this.unitOfWork.Connection.Query<T>(query, param);
//     }

//     protected virtual string SelectQuery(string where = null, string orderBy = null, int? offset = null, int? limit = null, string groupBy = null)
//     {
//         StringBuilder sql = new StringBuilder(SelectFrom);

//         if (!string.IsNullOrEmpty(where))
//         {
//             if (SelectFrom.Contains("{where}")) 
//             {
//                 sql.Replace("{where}", where);
//             }
//             else
//             {
//                 sql.Append(" where ").Append(where);
//             }
//         }

//         if (!string.IsNullOrEmpty(groupBy))
//         {
//             sql.Append(" group by ").Append(groupBy);
//         }

//         if (!string.IsNullOrEmpty(orderBy))
//         {
//             sql.Append(" order by ").Append(orderBy);
//         }

//         if(offset.HasValue)
//         {
//             sql.Append(" offset ").Append(offset);
//         }

//         if (limit.HasValue)
//         {
//             sql.Append(" limit ").Append(limit);
//         }

//         return sql.ToString();
//     }

//     protected Dictionary<ColumnInfo, object> GetChanges(T entity, bool isNew)
//     {
//         Dictionary<ColumnInfo, object> values = new Dictionary<ColumnInfo, object>();
        
//         T originalEntity = isNew ? default(T) : this.Get(entity.ID.Value);

//         foreach (var column in this.entityMapper.Columns.Values.Where(p => p.IsTemporal))
//         {
//             object newValue;
//             if (!CompareProperty(originalEntity, entity, column.PropertyInfo, out newValue))
//             {
//                 values.Add(column, newValue);
//             }
//         }

//         return values;
//     }

//     protected virtual void AfterSelect(T entity)
//     {
//     }

//     protected virtual void AfterInsert(T entity)
//     {
//     }

//     protected virtual void AfterUpdate(T entity)
//     {
//     }

//     protected virtual void AfterDelete(long id)
//     {
//     }

//     private bool CompareProperty(T originalEntity, T entity, PropertyInfo property, out object newValue)
//     {
//         newValue = property.GetValue(entity);

//         if (originalEntity == null)
//         {
//             return false;
//         }
//         else
//         {
//             object oldValue = property.GetValue(originalEntity);
//             return object.Equals(oldValue, newValue);
//         }            
//     }
    
//     protected void PersistManyToMany<TChild>(T entity, string property, IEnumerable<TChild> children)
//     {
//         var childMapper = EntityMapper.Get(typeof(TChild));
//         var manyToMany = typeof(T).GetProperty(property).GetCustomAttribute<ManyToManyAttribute>();
//         var sourceColumn = childMapper.Columns[manyToMany.SourceProperty.ToLower()];
//         var childColumns = childMapper.Columns.Values;

//         this.unitOfWork.Connection.ExecuteScalar(
//             $"delete from {childMapper.TableName} WHERE {sourceColumn.ColumnName} = @ID",
//             new { ID = entity.ID });

//         foreach (var child in children)
//         {
//             sourceColumn.PropertyInfo.SetValue(child, entity.ID);

//             string query =
//                 $"insert into {childMapper.TableName} " +
//                 $"({string.Join(",", childColumns.Where(p => p.Insertable).Select(p => p.ColumnName))}) " +
//                 $"values ({string.Join(",", childColumns.Where(p => p.Insertable).Select(p => "@" + p.PropertyName))})";

//             this.unitOfWork.Connection.Execute(query, child);
//         }
//     }
// }

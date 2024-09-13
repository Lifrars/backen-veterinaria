using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using Npgsql;

namespace ApiVeterinaria.Repository;

public  class RepositoryGeneric<T>
{
    protected readonly string? _connectionString;
    public RepositoryGeneric(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("PostgreSQLConnection");

    public async Task<IEnumerable<T>> GetAll()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        await using var conn = new NpgsqlConnection(_connectionString);
        var tableName = GetTableName();
        var query = $"SELECT * FROM {tableName}";
        var entities = await conn.QueryAsync<T>(query);
        return entities.ToList();
    }
    public async Task<IEnumerable<T>> GetById(int id)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        await using var conn = new NpgsqlConnection(_connectionString);
        var tableName = GetTableName();
        var query = $"SELECT * FROM {tableName} WHERE id = @Id";
        var parameters = new { Id = id };
        var entities = await conn.QueryAsync<T>(query,parameters);
        return entities.ToList();
    }
    
    public async Task<int> GetCount()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        await using var conn = new NpgsqlConnection(_connectionString);
        var tableName = GetTableName();
        var query = $"SELECT COUNT(*) FROM {tableName} ";
        var count = await conn.ExecuteScalarAsync<int>(query);
        return count;
    }
    
    private string GetTableName()
    {
        var type = typeof(T);
        var tableAttribute = type.GetCustomAttribute<TableAttribute>();
        return tableAttribute != null ? tableAttribute.Name : type.Name;
    }

}

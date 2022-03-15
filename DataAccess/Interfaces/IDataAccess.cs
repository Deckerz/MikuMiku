namespace DataAccess.Interfaces;

public interface IDataAccess
{
    string ConnectionStringName { get; }
    Task<List<T>> LoadData<T>(string sql);
    Task<List<T>> LoadData<T, TU>(string sql, TU parameters);
    Task<T> LoadDataSingle<T>(string sql);
    Task<T> LoadDataSingle<T, TU>(string sql, TU parameters);
    Task<bool> SaveData<T>(string sql, T parameters);
}
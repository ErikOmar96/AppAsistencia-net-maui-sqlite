// Agregar Modelos, Utilidades y  Microsoft.EF
using AppAsistencia.Modelos;
using SQLite;
using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;

namespace AppAsistencia.DataAccess
{
    public class AsistenciaDBContext : IAsyncDisposable
    {
        // Constante para nombre de base de datos
        private const string DbName = "AsistenciaDB.db3";
        // Ruta para guardar el archivo de la base de datos
        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);
        // Propiedades
        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database => 
            (_connection ??= new SQLiteAsyncConnection(DbPath, 
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

        // Métodos Auxiliares
        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return Database.Table<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        // Métodos Públicos para Operaciones CRUD
        public async Task<IEnumerable<TTable>> GetFilteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
        {         
            await CreateTableIfNotExists<TTable>();
            return await action();
        }

        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            return await Execute<TTable, TTable>(async () => await Database.GetAsync<TTable>(primaryKey));
        }

        // Add
        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await Database.InsertAsync(item) > 0);
        }

        // Update
        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.UpdateAsync(item) > 0;
        }

        // Delete
        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync<TTable>(primaryKey) > 0;
        }

        // Métodos para manejar la relación Usuario - Asistencia
        public async Task<IEnumerable<Asistencia>> GetAsistenciasByUsuarioIdAsync(int usuarioId)
        {
            return await GetFilteredAsync<Asistencia>(a => a.IdUsuario == usuarioId);
        }

        public async Task<bool> AddUsuarioWithAsistenciasAsync(Usuario usuario, List<Asistencia> asistencias)
        {
            bool usuarioAdded = await AddItemAsync(usuario);
            if (usuarioAdded)
            {
                foreach (var asistencia in asistencias)
                {
                    asistencia.IdUsuario = usuario.IdUsuario;
                    await AddItemAsync(asistencia);
                }
            }
            return usuarioAdded;
        }

        // Método para obtener un usuario por nombre de usuario y clave
        public async Task<Usuario?> GetUsuarioAsync(string nombreUsuario, string clave)
        {
            var usuarios = await GetFilteredAsync<Usuario>(u => u.NombreUsuario == nombreUsuario && u.ClaveUsuario == clave);
            return usuarios.FirstOrDefault();
        }

        // Obtener asistencias por rango de fechas
        public async Task<List<Asistencia>> GetAsistenciasPorFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await Database.Table<Asistencia>()
            .Where(a => a.FechaAsistencia >= fechaInicio && a.FechaAsistencia <= fechaFin)
            .ToListAsync();
        }

        public async ValueTask DisposeAsync() => await _connection?.CloseAsync();

    }
}

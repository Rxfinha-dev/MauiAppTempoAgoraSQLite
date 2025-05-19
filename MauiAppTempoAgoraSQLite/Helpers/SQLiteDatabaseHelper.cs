using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppTempoAgoraSQLite.Models;
using SQLite;

namespace MauiAppTempoAgoraSQLite.Helpers
{
    public class SQLiteDatabaseHelper
    {
        // Conexão assíncrona com o banco de dados SQLite
        readonly SQLiteAsyncConnection _conn;

        // Construtor que recebe o caminho do banco de dados
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            // Cria a tabela Produto caso ainda não exista
            _conn.CreateTableAsync<Tempo>().Wait();
        }

        // Método para inserir um novo produto na tabela
        public Task<int> Insert(Tempo t)
        {
            return _conn.InsertAsync(t);
        }



        // Método para excluir um produto pelo ID
        public Task<int> Delete(int id)
        {
            return _conn.Table<Tempo>().DeleteAsync(i => i.Id == id);
        }

        // Método para obter todos os produtos cadastrados
        public Task<List<Tempo>> GetAll()
        {
            return _conn.Table<Tempo>().ToListAsync();
        }

        // Método para buscar produtos com base na descrição (filtro de texto)
        public Task<List<Tempo>> Search(string q)
        {
            string sql = "SELECT * FROM Tempo WHERE description LIKE '%" + q + "%'";

            return _conn.QueryAsync<Tempo>(sql);
        }
    }
}

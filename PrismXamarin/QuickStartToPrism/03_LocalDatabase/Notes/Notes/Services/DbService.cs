using Notes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Notes.Services
{
	public class DbService : IDbService
	{
		readonly SQLiteAsyncConnection _database;

		public DbService()
		{
			_database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
			_database.CreateTableAsync<Note>().Wait();
		}

		public Task<List<Note>> GetNotesAsync()
		{
			return _database.Table<Note>().ToListAsync();
		}

		public Task<Note> GetNoteAsync(int id)
		{
			return _database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
		}

		public Task<int> SaveNoteAsync(Note note)
		{
			if (note.ID != 0)
			{
				return _database.UpdateAsync(note);
			}
			else
			{
				return _database.InsertAsync(note);
			}
		}

		public Task<int> DeleteNoteAsync(Note note)
		{
			return _database.DeleteAsync(note);
		}

	}
}

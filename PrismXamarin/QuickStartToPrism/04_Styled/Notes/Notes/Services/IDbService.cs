using Notes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Services
{
	public interface IDbService
	{
		Task<List<Note>> GetNotesAsync();

		Task<Note> GetNoteAsync(int id);

		Task<int> SaveNoteAsync(Note note);

		Task<int> DeleteNoteAsync(Note note);
	}
}

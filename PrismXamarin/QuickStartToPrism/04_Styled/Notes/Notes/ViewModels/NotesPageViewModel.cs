using Notes.Models;
using Notes.Services;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;

namespace Notes.ViewModels
{
	public class NotesPageViewModel : ViewModelBase,IPageLifecycleAware
	{
		private INavigationService _navigationService;
		private IDbService _dbService;
		private List<Note> _notes;
		public List<Note> Notes
		{
			get { return _notes; }
			set { SetProperty(ref _notes, value); }
		}

		public DelegateCommand NoteAddCommand { get; private set; }
		public DelegateCommand<Note> ItemSelectedCommand { get; private set; }
		public NotesPageViewModel(INavigationService navigationService, IDbService dbService):base(navigationService)
		{
			_navigationService = navigationService;
			_dbService = dbService;
			NoteAddCommand = new DelegateCommand(NoteAdd);
			ItemSelectedCommand = new DelegateCommand<Note>(ItemSelected);

		}

		public async void OnAppearing()
		{
			//_notes = new List<Note>();

			//var files = Directory.EnumerateFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)), "*.notes.txt");
			//foreach (var filename in files)
			//{
			//	_notes.Add(new Note()
			//	{
			//		Filename = filename,
			//		Text = File.ReadAllText(filename),
			//		Date = File.GetCreationTime(filename)
			//	});
			//}
			Notes = await _dbService.GetNotesAsync();
			
		}

		public void OnDisappearing()
		{
		}

		private async void NoteAdd()
		{
			var navigationParams = new NavigationParameters();
			navigationParams.Add("Note", new Note());
			await _navigationService.NavigateAsync("NoteEntryPage", navigationParams);
		}

		private async void ItemSelected(Note note)
		{
			if(note != null)
			{
				var navigationParams = new NavigationParameters();
				navigationParams.Add("Note", note);
				await _navigationService.NavigateAsync("NoteEntryPage", navigationParams);
			}
		}
	}
}

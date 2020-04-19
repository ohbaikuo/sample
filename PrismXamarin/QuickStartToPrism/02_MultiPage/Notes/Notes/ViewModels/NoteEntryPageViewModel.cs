using Notes.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.IO;

namespace Notes.ViewModels
{
	public class NoteEntryPageViewModel : BindableBase, INavigationAware
	{
		private INavigationService _navigationService;
		private Note _note;

		public Note Note
		{
			get { return _note; }
			set { SetProperty(ref _note, value); }
		}
		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand DeleteCommand { get; private set; }
		public NoteEntryPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			SaveCommand = new DelegateCommand(Save);
			DeleteCommand = new DelegateCommand(Delete);
		}

		private async void Save()
		{
			if(string.IsNullOrWhiteSpace(_note.Filename))
			{
				var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName() }.notes.txt");
				File.WriteAllText(fileName, _note.Text);
			}
			else
			{
				File.WriteAllText(_note.Filename, _note.Text);
			}
			await _navigationService.GoBackAsync();
		}

		private async void Delete()
		{
			if (_note != null && File.Exists(_note.Filename))
			{
				File.Delete(_note.Filename);
			}
			await _navigationService.GoBackAsync();

		}

		public void OnNavigatedFrom(INavigationParameters parameters)
		{
		}

		public void OnNavigatedTo(INavigationParameters parameters)
		{
			var note = parameters["Note"] as Note;
			this.Note = note ?? new Note();
		}
	}
}

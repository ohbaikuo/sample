using Prism.Commands;
using Prism.Navigation;
using System;
using System.IO;

namespace Notes.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

		string _text;
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		public DelegateCommand SaveCommand { get; set; }
		public DelegateCommand DeleteCommand { get; set; }
		public MainPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Title = "Main Page";
			SaveCommand = new DelegateCommand(Save);
			DeleteCommand = new DelegateCommand(Delete);

			if (File.Exists(_fileName))
			{
				Text = File.ReadAllText(_fileName);
			}
		}

		private void Save()
		{
			File.WriteAllText(_fileName, Text);
		}

		private void Delete()
		{
			if(File.Exists(_fileName))
			{
				File.Delete(_fileName);
			}
			Text = string.Empty;
		}
	}
}

using Notes.Services;
using Notes.ViewModels;
using Notes.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Notes
{
	public partial class App
	{
		/* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
		public App() : this(null) { }

		public App(IPlatformInitializer initializer) : base(initializer) { }

		protected override async void OnInitialized()
		{
			InitializeComponent();

			await NavigationService.NavigateAsync("NavigationPage/NotesPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<NotesPage, NotesPageViewModel>();
			containerRegistry.RegisterForNavigation<NoteEntryPage, NoteEntryPageViewModel>();

			containerRegistry.Register<IDbService, DbService>();
		}
	}
}

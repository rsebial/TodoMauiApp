using TodoMauiApp.Pages;

namespace TodoMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ManageToDoPage), typeof(ManageToDoPage));
	}
}

using System.Diagnostics;
using TodoMauiApp.DataServices;
using TodoMauiApp.Models;
using TodoMauiApp.Pages;

namespace TodoMauiApp;

public partial class MainPage : ContentPage
{
    private readonly IRestDataServices _dataService;
    int count = 0;

	public MainPage(IRestDataServices restDataServices)
	{
		InitializeComponent();
		_dataService = restDataServices;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

        collectionView.ItemsSource = await _dataService.GetAllTodosAsync();
	}

	async void OnAddTodoClicked(object send, EventArgs e)
	{
		Debug.WriteLine("----> Add Button Clicked");

		var navigationParameter = new Dictionary<string, object>
		{
			{nameof(ToDo), new ToDo() }
		};

		await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
	}

	async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        Debug.WriteLine("----> Item Changed!");

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo }
        };

        await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
    }
	
}


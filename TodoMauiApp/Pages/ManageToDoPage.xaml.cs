using System.Diagnostics;
using TodoMauiApp.DataServices;
using TodoMauiApp.Models;

namespace TodoMauiApp.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
    private readonly IRestDataServices _restDataServices;
    ToDo _todo;
    bool _isNew;

        public ToDo ToDo
        {
            get => _todo;
            set
            {
                _isNew = IsNew(value);
                _todo = value;
                OnPropertyChanged();
            }
            
        }


        public ManageToDoPage(IRestDataServices restDataServices)
	    {
		
		    InitializeComponent();
            _restDataServices = restDataServices;
            BindingContext = this;
        }

        bool IsNew(ToDo toDo)
        {
            if(toDo.Id == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    async void OnCancelButtonClicked(object send, EventArgs e)
    {
        
        await Shell.Current.GoToAsync("..");
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if(_isNew)
        {
            Debug.WriteLine("----> Add new Item");

            await _restDataServices.AddTodoAsync(ToDo);
        }
        else{
            Debug.WriteLine("----> Update new Item");

            await _restDataServices.UpdateTodoAsync(ToDo);
        }
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClicked(object send, EventArgs e)
    {
        Debug.WriteLine("----> Delete Item");

        await _restDataServices.DeleteTodoAsync(ToDo.Id);
        await Shell.Current.GoToAsync(".."); 
    }
}

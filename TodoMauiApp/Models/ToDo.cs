using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMauiApp.Models
{
    public class ToDo : INotifyPropertyChanged
    {
        int _id;

        public int Id {
            get=> _id;
            set
            {
                if (_id == value)
                {
                    return;
                }
                else
                {
                    _id = value;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }


        string _todoname;

        public string ToDoName { get=>_todoname; set
        {
                if (_todoname == value) {
                    return;
                }
                else
                {
                    _todoname = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}

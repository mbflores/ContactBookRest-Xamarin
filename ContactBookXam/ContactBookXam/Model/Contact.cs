using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ContactBookXam.Model
{
    public class Contact:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        public int Id { get; set; }

        private string _firstName;
        private string _lastName;
        public string FirstName
        {
            get { return _firstName;}
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        
    }
        public string LastName
        {
            get { return _lastName;}
            set
            {
                _lastName=value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

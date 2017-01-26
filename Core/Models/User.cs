using System.Runtime.Serialization;
using MugenMvvmToolkit.Models;

namespace Core.Models
{
    [DataContract]
    [KnownType(typeof(User))]
    public class User : NotifyPropertyChangedBase
    {
        private string _firstname;
        private string _lastname;

        [DataMember]
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (Equals(Lastname, value)) return;
                _lastname = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                if (Equals(Firstname, value)) return;
                _firstname = value;
                OnPropertyChanged();
            }
        }
    }
}

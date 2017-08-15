using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace favour.ViewModel
{
    public class viewModelFirst : ViewModelBase
    {
        public viewModelFirst()
        {
            Words = "Hello";
            
            Reverse = new RelayCommand(() =>
            {
                Words = new string(Words.ToCharArray().Reverse().ToArray());
            });
        }

        private string _words;
        public RelayCommand Reverse { get; set; }

        public string Words
        {
            get
            {
                return _words;
            }
            set
            {
                Set(() => Words, ref _words, value);
            }
        }
    }
}

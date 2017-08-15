using System;
using System.ComponentModel;

namespace favour.Model
{
    public class modelFirst : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;
        public int getID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            //    RaisePropertyChanged("Count");
            }
        }

        public void goTo()
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(_id.ToString()));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

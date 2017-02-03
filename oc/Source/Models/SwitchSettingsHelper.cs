using System.ComponentModel;

namespace oc.Source.Models
{
    class SwitchSettingsHelper : INotifyPropertyChanged
    {
        private bool _status;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Text => Status ? "I would like to receive emails related to my OneCommunity program" : "Unsubscribe me from all OneCommunity program-related emails";

        public bool Status
        {
            get { return _status; }
            set {
                if (_status != null)
                    _status = value;
                OnPropertyChanged("Text");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
      }
}

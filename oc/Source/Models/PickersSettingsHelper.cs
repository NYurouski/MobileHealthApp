using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using oc.Source.Constants;
using oc.Source.Models.DataModel;

namespace oc.Source.Models
{
    class PickersSettingsHelper : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> _programNameList = new ObservableCollection<string>();

        public ObservableCollection<string> ProgramNameList
        {
            get { return _programNameList; }
            set
            {
                _programNameList = value;
                OnPropertyChanged("ProgramNameList");
            }
        }

        private ObservableCollection<string> _statesList = new ObservableCollection<string>();

        public ObservableCollection<string> StatesList
        {
            get { return _statesList; }
            set
            {
                _statesList = value;
                OnPropertyChanged("StatesList");
            }
        }

        public string Text => Status ? "I would like to receive emails related to my OneCommunity program" : "Unsubscribe me from all OneCommunity program-related emails";

        public bool Status
        {
            get { return Model.EmailMarketing == 1; }
            set
            {
                Model.EmailMarketing = value ? 1 : 0;
                OnPropertyChanged("Text");
            }
        }

        public string State
        {
            get
            {
                return ApiConstants.listStates.FirstOrDefault(listState => listState.Value == Model.State).Key;
            }
            set
            {
               Model.State = ApiConstants.listStates.FirstOrDefault(listState => listState.Key == value).Value;
               OnPropertyChanged("State");
            }
        }

        private string _program;
        public string Program
        {
            get { return _program; }
            set
            {
                _program = value;
                OnPropertyChanged("Program");
            }
        }

        private SettingsDataRequestModel _model;
        public SettingsDataRequestModel Model
        {
            get { return _model; }
            set
            {
               _model = value;
                OnPropertyChanged("Model");
            }
        }

        public void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

    }
}


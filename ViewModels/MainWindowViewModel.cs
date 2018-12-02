using MyPeopleApp.Command;
using MyPeopleApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyPeopleApp.Interfaces;
using MyPeopleApp.Services;
using System.Windows;

namespace MyPeopleApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IXmlDataService _dataservices;
 
        public MainWindowViewModel()
        {
            _dataservices =new XmlDataService();
            GetDataFromXml();
        }

        #region Properties

        private ObservableCollection<Person> _savePerson;
        public ObservableCollection<Person> Peoples
        {
            get => _savePerson;
            set { SetProperty(ref _savePerson, value); }
        }

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get {
                if (_SelectedPerson == null)
                    _SelectedPerson = new Person();
                return _SelectedPerson;
                }
            set { SetProperty(ref _SelectedPerson, value); }
        }

        private string _age;
        public string Age
        {
            get => _age;
            set { 
                    SetProperty(ref _age, value);
                }
        }

        private bool _isEnableEditDisable;
        public bool IsEnableEditButton
        {
            get => _isEnableEditDisable;
            set
            {
                SetProperty(ref _isEnableEditDisable, value);
            }
        }

        private bool _isEnableSaveButton;
        public bool IsEnableSaveButton
        {
            get => _isEnableSaveButton;
            set
            {
                SetProperty(ref _isEnableSaveButton, value);
            }
        }
        #endregion properties
        
        #region Commands

        private ICommand _getCommand;
        private ICommand _saveCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;
        private ICommand _buttonStatusCommand;
        private ICommand _TextChangeCommand;


        public ICommand GetCommand
        {
            get
            {
                if (_getCommand == null)
                {
                    _getCommand = new RelayCommand(
                        param => GetDataFromXml()
                    );
                }
                return _getCommand;
            }
        }
       
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => SaveData()
                    );
                }
                return _saveCommand;
            }
        }
        
        public ICommand EditCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new RelayCommand(
                        param => EditData()
                    );
                }
                return _updateCommand;
            }
        }
        
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => DeleteData()
                    );
                }
                return _deleteCommand;
            }
        }
        
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        param => ButtonStatusClearTextBox()
                    );
                }
                return _cancelCommand;
            }
        }

        public ICommand GridSelectionChangedCommand
        {
            get
            {
                if (_buttonStatusCommand == null)
                {
                    _buttonStatusCommand = new RelayCommand(
                        param => ButtonStatusCommand()
                    );
                }
                return _buttonStatusCommand;
            }
        }

        
        public ICommand TextChangedCommand
        {
            get
            {
                if (_TextChangeCommand == null)
                {
                    _TextChangeCommand = new RelayCommand(
                        param => TextChangeCommand()
                    );
                }
                return _TextChangeCommand;
            }
        }
        private void ButtonStatusCommand()
        {
            IsEnableEditButton = false;
            IsEnableSaveButton = false;
        }
        private void TextChangeCommand()
        {
            IsEnableEditButton = true;
            IsEnableSaveButton = false;
        }

        private void ButtonStatusClearTextBox()
        {
            GetDataFromXml();
        }
        private void SaveData()
        {
            _dataservices.InsertData(new Person
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = SelectedPerson.FirstName,
                LastName = SelectedPerson.LastName,
                ApartmentNumber = SelectedPerson.ApartmentNumber,
                DayOfBirth = SelectedPerson.DayOfBirth,
                HouseNumber = SelectedPerson.HouseNumber,
                PhoneNumber = SelectedPerson.PhoneNumber,
                PostalCode = SelectedPerson.PostalCode,
                StreetName = SelectedPerson.StreetName,
                Age = CalculateAge(SelectedPerson.DayOfBirth)

            });
            GetDataFromXml();
        }

        private void EditData()
        {
            _dataservices.EditData(SelectedPerson);
            GetDataFromXml();
            IsEnableEditButton = false;
            IsEnableSaveButton = true;
        }

        private void DeleteData()
        {
            _dataservices.DeleteData(SelectedPerson.Id);
            GetDataFromXml();
        }

        #endregion
        private string CalculateAge(DateTime dateOfBirth)
        {
            int Days = (DateTime.Now.Year * 365 + DateTime.Now.DayOfYear) - (dateOfBirth.Year * 365 + dateOfBirth.DayOfYear);
            int Years = Days / 365;
            var res =  (Days >= 365) ?  + Years + " years" : + Days + " days";
            Age = res;
            return res;
        }

        private void GetDataFromXml()
        {
            var list = _dataservices.ReadData();

            if (list !=null)
            Peoples = new ObservableCollection<Person>(list);

        }
        
    }
}

using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Share.Dtos;
using Share.Services;
using System.Windows;


namespace IceCreamAppWPF.ViewModels
{
    public partial class UserListViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CustomerService _customerService;

        public UserListViewModel(IServiceProvider serviceProvider, CustomerService customerService)
        {
            _serviceProvider = serviceProvider;
            _customerService = customerService;
        }

        [RelayCommand]
        public void NavigateToWelcomePage()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            //Går till
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        }

        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value); 
        } 

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string? _streetName;
        public string? StreetName
        {
            get => _streetName;
            set => SetProperty(ref _streetName, value);
        }

        private string? _city;
        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private int _postalCode;
        public int PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        [RelayCommand]
        public void AddCustomer()
        {
            //Result.Content = "";

            var customer = new Customer()
            {
                FirstName = FirstName!,
                LastName = LastName!,
                Email = Email!,
                Password = Password!,
                StreetName = StreetName,
                PostalCode = PostalCode,
                City = City,
            };

            var result = _customerService.CreateCustomer(customer);
            ClearForm();

            if (result == true)
            {
                //Result.Content = "Customer Added";
            }
            else
            {
                //Result.Content = "Something went wrong";
            }
        }

        public void ClearForm()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            StreetName = "";
            City = "";
        }
}
}

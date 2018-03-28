using DbApp.Model;
using DbApp.Services;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DbApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(ICustomersService customersService)
        {
            this.customersService = customersService;
            status = "Loading";
            this.customersService.GetCustomersAsync()
                .ContinueWith(antc => Customers = antc.Result);

            addNewCommand = new DelegateCommand(AddNewCustomer);
            editCustomerCommand = new DelegateCommand<Customer>(EditCustomer, CanEditCustomer);
            editCustomerCommand.ObservesProperty(() => this.SelectedCustomer);
            deleteCustomerCommand = new DelegateCommand<Customer>(DeleteCustomer, CanDeleteCustomer);
            deleteCustomerCommand.ObservesProperty(() => this.SelectedCustomer);
            detailsCommand = new DelegateCommand<Customer>(ShowDetails);

            customerUpdateInteraction = new InteractionRequest<Confirmation>();
            customerDetailsInteraction = new InteractionRequest<Notification>();
        }

        private ICustomersService customersService;
        private ObservableCollection<Customer> customers;
        private Customer selectedCustomer;
        private string status;
        private DelegateCommand addNewCommand;
        private DelegateCommand<Customer> editCustomerCommand, deleteCustomerCommand, detailsCommand;
        private InteractionRequest<Confirmation> customerUpdateInteraction;
        private InteractionRequest<Notification> customerDetailsInteraction;

        public ICommand AddNewCustomerCommand => this.addNewCommand;
        public ICommand EditCustomerCommand => this.editCustomerCommand;
        public ICommand DeleteCustomerCommand => this.deleteCustomerCommand;
        public ICommand CustomerDetailsCommand => this.detailsCommand;
        public IInteractionRequest CustomerInteraction => this.customerUpdateInteraction;
        public IInteractionRequest CustomerDetailsInteraction => this.customerDetailsInteraction;

        public ObservableCollection<Customer> Customers
        {
            get => this.customers;
            private set
            {
                if (SetProperty(ref this.customers, value))
                {
                    Status = $"Loaded {value?.Count} Customers";
                }
            }
        }

        public Customer SelectedCustomer
        {
            get => this.selectedCustomer;
            set => SetProperty(ref this.selectedCustomer, value);
        }

        public string Status
        {
            get => this.status;
            private set => SetProperty(ref this.status, value);
        }

        private bool CanDeleteCustomer(Customer customer)
        {
            return customer != null;
        }

        private bool CanEditCustomer(Customer customer)
        {
            return customer != null;
        }

        private void AddNewCustomer()
        {
            Customer newCustomer = customersService.CreateNewCustomer();
            customerUpdateInteraction.Raise(new Confirmation()
            {
                Title = "Add New customer",
                Content = newCustomer
            }, async (res) =>
            {
                if (!res.Confirmed)
                    return;

                int result = 0;
                try
                {
                    result = await this.customersService.AddCustomerAsync(newCustomer);
                    Status = $"Added {result} records";
                }
                catch (Exception)
                {
                    Status = "Error";
                }
            });
        }

        private void EditCustomer(Customer customer)
        {
            customerUpdateInteraction.Raise(new Confirmation()
            {
                Title = "Edit customer",
                Content = customer
            }, async (res) =>
            {
                if (!res.Confirmed)
                    return;

                int result = 0;
                try
                {
                    result = await this.customersService.UpdateCustomerAsync(customer);
                    Status = $"Updated {result} records";
                }
                catch (Exception)
                {
                    Status = "Error";
                }
            });
        }

        private async void DeleteCustomer(Customer customer)
        {
            int result = 0;
            try
            {
                result = await this.customersService.DeleteCustomerAsync(customer);
                Status = $"Deleted {result} records";
            }
            catch (Exception)
            {
                Status = "Error";
            }
        }

        private void ShowDetails(Customer customer)
        {
            customerDetailsInteraction.Raise(new Notification()
            {
                Title = customer.ContactName,
                Content = customer
            });
        }
    }
}

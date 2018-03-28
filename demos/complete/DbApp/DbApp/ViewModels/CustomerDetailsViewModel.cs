using DbApp.Model;
using DbApp.Services;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace DbApp.ViewModels
{
    public class CustomerDetailsViewModel : BindableBase, IInteractionRequestAware
    {
        public CustomerDetailsViewModel(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        private ICustomersService _customersService;
        private INotification _interaction;
        private Customer _customer;
        private IList<ProductReport> _customerProducts;

        public INotification Notification
        {
            get => _interaction;
            set
            {
                if (value == _interaction)
                    return;
                _interaction = value;

                Customer = _interaction.Content as Customer;
                if (Customer != null)
                    CustomerProducts = _customersService.CreateCustomerProductReports(Customer);
            }
        }
        public Action FinishInteraction { get; set; }

        public Customer Customer
        {
            get => _customer;
            private set => SetProperty(ref _customer, value);
        }

        public IList<ProductReport> CustomerProducts
        {
            get => _customerProducts;
            private set => SetProperty(ref _customerProducts, value);
        }
    }
}

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
        private IList<ProductReport> _customerProducts;

        // <Prism.IInteractionRequestAware>
        public INotification Notification
        {
            get => _interaction;
            set
            {
                if (value == _interaction)
                    return;
                _interaction = value;

                ShowCustomer(_interaction.Content as Customer);
            }
        }

        public Action FinishInteraction { get; set; }
        // </Prism.IInteractionRequestAware>

        public IList<ProductReport> CustomerProducts
        {
            get => _customerProducts;
            private set => SetProperty(ref _customerProducts, value);
        }

        private void ShowCustomer(Customer customer)
        {
            CustomerProducts = _customersService.CreateCustomerProductReports(customer);
        }
    }
}

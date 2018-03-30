using DbApp.Model;
using DbApp.Services;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;

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

        private void ShowCustomer(Customer customer)
        {
            // TODO: Show customer products report
        }
    }
}

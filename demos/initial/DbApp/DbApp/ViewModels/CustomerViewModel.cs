using DbApp.Model;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace DbApp.ViewModels
{
    public class CustomerViewModel : BindableBase, IInteractionRequestAware
    {
        public CustomerViewModel()
        {
            _okCommand = new DelegateCommand(OnOk);
        }

        private Confirmation _interaction;
        private Customer _customer;
        private DelegateCommand _okCommand;
        private string _okAction;

        public INotification Notification
        {
            get => _interaction;
            set
            {
                var newInteraction = value as Confirmation;
                if (newInteraction == _interaction)
                    return;
                _interaction = newInteraction;

                if (newInteraction == null)
                    CleanUp();
                else
                    Initialize(_interaction);
            }
        }
        public Action FinishInteraction { get; set; }
        public Customer Customer
        {
            get => _customer;
            private set => SetProperty(ref _customer, value);
        }

        public string OkAction { get => _okAction; private set => SetProperty(ref _okAction, value); }

        public ICommand OkCommand => _okCommand;

        public void Initialize(Confirmation interaction)
        {
            Customer = interaction.Content as Customer;
            OkAction = string.IsNullOrEmpty(Customer.CustomerID) ? "Insert" : "Update";
        }

        public void CleanUp()
        {
            Customer = null;
        }

        private void OnOk()
        {
            if (_customer.Validate())
            {
                CompleteInteraction(true);
            }
        }

        private void CompleteInteraction(bool result)
        {
            _interaction.Confirmed = result;
            CleanUp();
            FinishInteraction();
        }
    }
}

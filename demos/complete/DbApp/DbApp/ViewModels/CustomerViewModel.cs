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
        }

        private Confirmation _interaction;
        private Customer _customer;

        // <Prism.IInteractionRequestAware>
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
        // </Prism.IInteractionRequestAware>

        public Customer Customer
        {
            get => _customer;
            private set
            {
                if (_customer != null)
                {
                    _customer.EditEnd -= OnEditEnd;
                    _customer.EditCancel -= OnEditCancel;
                }

                if (!SetProperty(ref _customer, value))
                    return;

                if (_customer != null)
                {
                    _customer.EditEnd += OnEditEnd;
                    _customer.EditCancel += OnEditCancel;
                }
            }
        }

        private void Initialize(Confirmation interaction)
        {
            Customer = interaction.Content as Customer;
        }

        private void CleanUp()
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

        private void OnEditCancel()
        {
            CompleteInteraction(false);
        }

        private void OnEditEnd()
        {
            OnOk();
        }
    }
}

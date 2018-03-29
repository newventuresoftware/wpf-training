using DbApp.Model;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;

namespace DbApp.ViewModels
{
    public class CustomerViewModel : BindableBase, IInteractionRequestAware
    {
        private Confirmation _interaction;
        private Customer _customer;

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

        public void Initialize(Confirmation interaction)
        {
            Customer = interaction.Content as Customer;
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

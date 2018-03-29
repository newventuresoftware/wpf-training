using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace DbApp.Model
{
    public class Customer : INotifyDataErrorInfo
    {
        public Customer(Data.CustomerDTO dto)
        {
            this.dto = dto;
            this.errorsContainer = new Prism.Mvvm.ErrorsContainer<string>(RaiseErrorsChanged);
        }

        private Data.CustomerDTO dto;
        private Prism.Mvvm.ErrorsContainer<string> errorsContainer;

        [Display(Name = "Customer ID")]
        public string CustomerID
        {
            get => this.dto.CustomerID;
            set
            {
                this.dto.CustomerID = value;
                this.Validate();
            }
        }

        [Display(Name = "Company Name")]
        public string CompanyName
        {
            get => this.dto.CompanyName;
            set
            {
                dto.CompanyName = value;
                this.Validate();
            }
        }

        [Display(Name = "Contact Name")]
        public string ContactName { get => this.dto.ContactName; set => this.dto.ContactName = value; }

        [Display(Name = "Contact Title")]
        public string ContactTitle { get => this.dto.ContactTitle; set => this.dto.ContactTitle = value; }

        public string Address { get => this.dto.Address; set => this.dto.Address = value; }

        public string City { get => this.dto.City; set => this.dto.City = value; }

        public string Region { get => this.dto.Region; set => this.dto.Region = value; }

        public string PostalCode { get => this.dto.PostalCode; set => this.dto.PostalCode = value; }

        public string Country { get => this.dto.Country; set => this.dto.Country = value; }

        public string Phone { get => this.dto.Phone; set => this.dto.Phone = value; }

        public string Fax { get => this.dto.Fax; set => this.dto.Fax = value; }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        [Display(AutoGenerateField = false)]
        public bool HasErrors => errorsContainer.HasErrors;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return errorsContainer.GetErrors(propertyName);
        }

        public bool Validate()
        {
            // CustomerID validation
            if (CustomerID == null || !Regex.IsMatch(CustomerID, "^[A-Z]{5}$"))
            {
                errorsContainer.SetErrors(() => CustomerID, new string[]
                {
                    "Customer ID must be exactly 5 uppercase symbols long."
                });
            }
            else
            {
                errorsContainer.ClearErrors(() => CustomerID);
            }

            // CompanyName validation
            if (string.IsNullOrEmpty(this.CompanyName))
            {
                errorsContainer.SetErrors(() => CompanyName, new string[]
                {
                    "Company Name should not be empty."
                });
            }
            else
            {
                errorsContainer.ClearErrors(() => CompanyName);
            }

            return !errorsContainer.HasErrors;
        }

        public Data.CustomerDTO ToDto() => this.dto;

        private void RaiseErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}

using DbApp.Data;
using DbApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DbApp.Services
{
    public interface ICustomersService
    {
        Task<ObservableCollection<Customer>> GetCustomersAsync();
        Task<int> AddCustomerAsync(Customer customer);
        Task<int> UpdateCustomerAsync(Customer customer);
        Task<int> DeleteCustomerAsync(Customer customer);

        Customer CreateNewCustomer();
        IList<ProductReport> CreateCustomerProductReports(Customer customer);
    }

    public class CustomersService : ICustomersService
    {
        public CustomersService()
        {
            this.context = new NorthwindEntities();
        }

        private NorthwindEntities context;
        private ObservableCollection<Customer> cachedCustomers;

        public Customer CreateNewCustomer()
        {
            var newCustomerDto = this.context.Customers.Create();
            return new Customer(newCustomerDto);
        }

        public async Task<ObservableCollection<Customer>> GetCustomersAsync()
        {
            if (cachedCustomers != null)
                return cachedCustomers;

            await context.Customers.LoadAsync();
            var query = context.Customers.Local.Select(dto => new Customer(dto));
            cachedCustomers = new ObservableCollection<Customer>(query);
            return cachedCustomers;
        }

        public Task<int> AddCustomerAsync(Customer customer)
        {
            var dto = customer.ToDto();
            this.context.Customers.Add(dto);
            cachedCustomers.Add(customer);
            return this.context.SaveChangesAsync();
        }

        public Task<int> UpdateCustomerAsync(Customer customer)
        {
            return this.context.SaveChangesAsync();
        }

        public Task<int> DeleteCustomerAsync(Customer customer)
        {
            var dto = customer.ToDto();
            dto.Orders.Clear();
            this.context.Customers.Remove(dto);
            cachedCustomers.Remove(customer);
            return this.context.SaveChangesAsync();
        }

        public IList<ProductReport> CreateCustomerProductReports(Customer customer)
        {
            var dto = customer.ToDto();
            var query = dto.Orders
                .SelectMany(c => c.Order_Details)
                .GroupBy(c => c.Products.ProductName)
                .Select(item => new ProductReport()
                {
                    Product = item.Key,
                    TotalQuantity = item.Sum(c => c.Quantity),
                    TotalSpent = item.Sum(c => c.Quantity * (c.UnitPrice * Convert.ToDecimal(1 - c.Discount)))
                })
                .OrderBy(c => c.TotalSpent);
            return query.ToArray();
        }
    }
}

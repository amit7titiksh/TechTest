using AnyCompany.Entities;
using AnyCompany.DataAccess;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            if (customerId < 0)
                return false;
            if (order.Amount == 0)
                return false;
            Customer customer = CustomerRepository.Load(customerId);
            if (customer == null)
                return false;
            order.CustomerID = customer.CustomerID;

           

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository.Save(order);

            return true;
        }
    }
}

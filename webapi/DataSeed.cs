using System.Reflection;
using webapi.Models;

namespace webapi
{
    public class DataSeed : IDataSeed
    {
        private readonly DataContext _context;

        public DataSeed(DataContext context)
        {
            _context = context;
        }

        public void SeedData(int nUsers, int nOrders)
        {
            if (!_context.users.Any())
            {
                SeedUsers(nUsers);
                _context.SaveChanges();
            }

            if (!_context.orders.Any())
            {
                SeedOrders(nUsers);
                _context.SaveChanges();
            }

            if (!_context.order_items.Any())
            {
                SeedOrderItems();
                _context.SaveChanges();
            }
        }

        private void SeedUsers(int n)
        {
            List<Users> users = BuildUserList(n);

            foreach (var user in users)
            {
                _context.users.Add(user);
            }
        }

        private List<Users> BuildUserList(int nCustomers)
        {
            var users = new List<Users>();
            var names = new List<string>();

            for (var i = 1; i <= nCustomers; i++)
            {
                var name = Helpers.MakeUniqueUserName(names);
                names.Add(name);

                users.Add(new Users
                {
                    username = name,
                    password = Helpers.GenerateRandomPassword(6),
                    first_name = name.Split('_')[0],
                    last_name = name.Split('_')[1],
                    telephone = Helpers.GenerateRandomTelephoneNumber(),
                    mobile = Helpers.GenerateRandomMobileNumber(),
                    email = Helpers.MakeCustomerEmail(name),
                    role = "user"
                }); ;
            }

            return users;
        }

        private void SeedOrders(int n)
        {
            List<Orders> orders = BuildOrderList(n);

            foreach (var order in orders)
            {
                _context.orders.Add(order);
            }
        }

        private List<Orders> BuildOrderList(int nOrders)
        {
            var orders = new List<Orders>();
            var rand = new Random();

            for (var i = 1; i <= nOrders; i++)
            {
                var randUserId = rand.Next(1, _context.users.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var users = _context.users.ToList();

                orders.Add(new Orders
                {
                    users = users.First(user => user.id == randUserId),
                    total = Helpers.GetRandomOrderTotal(),
                    payment_status = completed != null ? "completed" : "pending",
                    order_status = completed != null ? "completed" : "pending",
                    moodified_at = completed,
                    modified_by = 0,
                    created_at = placed,
                    created_by = 0
                }); ;
            }

            return orders;
        }

        private void SeedOrderItems()
        {
            List<OrderItems> orderItems = BuildOrderItemsList();

            foreach (var orderItem in orderItems)
            {
                _context.order_items.Add(orderItem);
            }
        }

        private List<OrderItems> BuildOrderItemsList()
        {
            var orderItems = new List<OrderItems>();
            var rand1 = new Random();
            var rand2 = new Random();
            var orders = _context.orders.ToList();

            for (var i = 1; i <= orders.Count; i++)
            {
                Random random = new Random();
                var nOrderItems = random.Next(1, 5);

                for (var j = 1; j <= nOrderItems; j++)
                {
                    var randOrderId = rand1.Next(1, _context.orders.Count());
                    var placed = Helpers.GetRandomOrderPlaced();
                    var completed = Helpers.GetRandomOrderCompleted(placed);
                    var product_variant = _context.product_variants.ToList();

                    orderItems.Add(new OrderItems
                    {
                        order = orders[i],
                        product_variant = product_variant.First(order => order.id == randOrderId),
                        quantity = rand2.Next(1, 10),
                        created_at = placed,
                        created_by = 0
                    });
                }
            }

            return orderItems;
        }
    }
}

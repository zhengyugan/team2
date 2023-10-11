namespace webapi
{
    public class Helpers
    {
        private static Random _rand = new Random();

        private static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }

        internal static string MakeUniqueUserName(List<string> names)
        {
            var maxNames = firstName.Count * lastName.Count;

            if (names.Count >= maxNames)
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            
            var prefix = GetRandom(firstName);
            var suffix = GetRandom(lastName);
            var name = prefix + "_" + suffix;

            if (names.Contains(name))
                MakeUniqueUserName(names);
            
            return name;
        }

        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal static string GenerateRandomMobileNumber()
        {
            Random random = new Random();

            // Generate random country code (e.g., +1 for USA)
            int countryCode = random.Next(1, 100);

            // Generate random mobile prefix (3 digits)
            int mobilePrefix = random.Next(100, 1000);

            // Generate random mobile number (7 digits)
            int mobileNumber = random.Next(1000000, 10000000);

            // Format and return the mobile number
            return $"+{countryCode}-{mobilePrefix}-{mobileNumber}";
        }

        internal static string GenerateRandomTelephoneNumber()
        {
            Random random = new Random();

            // Generate random area code (3 digits)
            int areaCode = random.Next(100, 1000);

            // Generate random first part (3 digits)
            int firstPart = random.Next(100, 1000);

            // Generate random second part (4 digits)
            int secondPart = random.Next(1000, 10000);

            // Format and return the telephone number
            return $"{areaCode}-{firstPart}-{secondPart}";
        }

        internal static string GetRandomState()
        {
            return GetRandom(usStates);
        }

        internal static int GenerateRandomPaymentId()
        {
            Random random = new Random();
            return random.Next(1000, 10000); // Generates a random integer between 1000 and 9999
        }

        internal static float GetRandomOrderTotal()
        {
            return _rand.Next(100, 5000);
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-365);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);
            
            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if (timePassed < minLeadTime)
            {
                return null;
            }

            return orderPlaced.AddDays(_rand.Next(7, 14));
        }

        private static readonly List<string> usStates = new List<string>()
        {
            "AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };

        private static readonly List<string> firstName = new List<string>()
        {
            "John", 
            "Jane", 
            "Michael", 
            "Emily", 
            "David", 
            "Sarah", 
            "Robert", 
            "Jessica"
        };

        private static readonly List<string> lastName = new List<string>()
        {
            "Smith", 
            "Johnson", 
            "Brown", 
            "Lee", 
            "Taylor", 
            "Clark", 
            "Wilson", 
            "Davis"
        };
    }
}

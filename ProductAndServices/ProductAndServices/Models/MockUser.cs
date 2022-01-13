namespace ProductAndServices.Models
{
    public class MockUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public static class UserData
    {
        public static List<MockUser> Users = new List<MockUser>()
        {
            new MockUser()
            {
                Id = 1,
                FirstName = "Mark",
                LastName = "Hunt",
                Email = "markhunt@example.com"
            },
            new MockUser()
            {
                Id = 1,
                FirstName = "Tina",
                LastName = "Smith",
                Email = "smth@example.com"
            }
        };
    }
}

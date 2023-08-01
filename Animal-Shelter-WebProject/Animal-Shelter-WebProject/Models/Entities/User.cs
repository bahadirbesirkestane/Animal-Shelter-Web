namespace Animal_Shelter_WebProject.Models.Entities
{
    public class User : IEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}

namespace Animal_Shelter_WebProject.Models.Entities
{
    public abstract class IEntity
    {
        public IEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

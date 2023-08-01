using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter_WebProject.Models.Entities
{
    public class Admin : IEntity
    {

        [StringLength(50)]
        public string AdminEmail { get; set; }

        [StringLength(50)]
        public string AdminPassword { get; set; }

        [StringLength(1)]
        public string AdminRole { get; set; }

    }
}

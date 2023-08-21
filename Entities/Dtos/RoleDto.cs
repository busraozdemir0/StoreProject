using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class RoleDto
    {
        public String? Id{ get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Name is required.")]
        public String? Name { get; set; }

    }
}

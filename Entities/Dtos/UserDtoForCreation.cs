using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record UserDtoForCreation : UserDto
    {
        [DataType(DataType.Password)]  // bu şekilde yazdığımızda view kısmında type="password" gibi bir ifade kullanmamıza gerek kalmayacaktır
        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}
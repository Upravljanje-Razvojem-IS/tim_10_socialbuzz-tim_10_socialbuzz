using UserService.DTOs.UserDTOs;

namespace UserService.DTOs.PersonalDTOs
{
    /// <summary>
    /// PersonalCreateDto
    /// </summary>
    public class PersonalCreateDto : UserCreateDto
    {
        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}

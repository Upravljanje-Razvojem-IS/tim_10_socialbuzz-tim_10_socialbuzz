using UserService.DTOs.UserDTOs;

namespace UserService.DTOs.PersonalDTOs
{
    /// <summary>
    /// PerosnalReadDto
    /// </summary>
    public class PersonalReadDto : UserReadDto
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

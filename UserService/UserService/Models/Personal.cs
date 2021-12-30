namespace UserService.Models
{
    /// <summary>
    /// Personal entity
    /// </summary>
    public class Personal : User
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

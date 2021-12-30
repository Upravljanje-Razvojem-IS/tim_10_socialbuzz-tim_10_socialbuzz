namespace UserService.Models
{
    /// <summary>
    /// Corporate entity
    /// </summary>
    public class Corporate
    {
        /// <summary>
        /// Id primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// PIB
        /// </summary>
        public string Pib { get; set; }
        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Company city
        /// </summary>
        public string CompanyCity { get; set; }
        /// <summary>
        /// Company address
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// Company email
        /// </summary>
        public string CompanyEmail { get; set; }
        /// <summary>
        /// Company mobile
        /// </summary>
        public string CompanyMobile { get; set; }
        /// <summary>
        /// Owner Id foregin key
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// Owner
        /// </summary>
        public User Owner { get; set; }
    }
}

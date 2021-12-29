namespace UserService.DTOs.CorporateDTOs
{
    /// <summary>
    /// CorporateCreateDto
    /// </summary>
    public class CorporateCreateDto
    {
        /// <summary>
        /// CreatedAT
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
    }
}

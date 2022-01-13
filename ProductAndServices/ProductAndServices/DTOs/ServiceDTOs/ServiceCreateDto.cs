namespace ProductAndServices.DTOs.Service
{
    /// <summary>
    /// Service create dto
    /// </summary>
    public class ServiceCreateDto
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
    }
}

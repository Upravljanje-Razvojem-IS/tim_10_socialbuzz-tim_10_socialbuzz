namespace ProductAndServices.DTOs.ProductDTOs
{
    /// <summary>
    /// Product create dto
    /// </summary>
    public class ProductCreateDto
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
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
    }
}

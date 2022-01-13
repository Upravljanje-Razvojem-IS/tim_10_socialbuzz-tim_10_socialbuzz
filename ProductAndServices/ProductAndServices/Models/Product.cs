using System.ComponentModel.DataAnnotations;

namespace ProductAndServices.Models
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
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
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [Range(0, double.MaxValue)]
        public int Quantity { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
    }
}
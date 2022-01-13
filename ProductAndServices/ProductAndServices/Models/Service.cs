using System.ComponentModel.DataAnnotations;

namespace ProductAndServices.Models
{
    /// <summary>
    /// Service
    /// </summary>
    public class Service
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
        public double Price { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
    }
}
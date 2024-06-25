using System;
using System.ComponentModel.DataAnnotations;

namespace EportfolioRestAPI.Models
{
    public class PortfolioModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PortfolioName { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string YouTubeLink { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        public string Username { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace blogger.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdateddAt { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string imgUrl { get; set; }
        public bool published { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        
    }
}
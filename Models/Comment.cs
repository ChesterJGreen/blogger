using System.ComponentModel.DataAnnotations;

namespace blogger.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatorId { get; set; }
        [Required]
        [MaxLength(240)]
        public string Body { get; set; }
        [Required]
        public int BlogId { get; set; }
        public Profile Creator;
        
    }
}
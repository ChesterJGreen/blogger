using System.ComponentModel.DataAnnotations;

namespace blogger.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string ImgUrl { get; set; }
        
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        public bool? Published { get; set; }
       
        
    }
}
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
        public string ImgUrl { get; set; }
        
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        //NOTE i need help understanding this Below
        private bool _published;
        internal bool PublishedWasSet { get; set; }
        public bool Published
        {
            get
            {
                return _published;
            }
            set
            {
             _published = value;
            PublishedWasSet = true;
            }
         }
        
    }
}
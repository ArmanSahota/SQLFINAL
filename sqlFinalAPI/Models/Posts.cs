using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sqlFinalAPI.Models
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}

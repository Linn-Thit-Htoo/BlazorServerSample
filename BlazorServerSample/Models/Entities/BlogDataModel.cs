using System.ComponentModel.DataAnnotations;

namespace BlazorServerSample.Models
{
    public class BlogDataModel
    {
        [Key]
        public long BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }
}


using System;

namespace News.Core.Domain.Models
{
    public class PinnedPost
    {
        public int Id { get; set; }
        public byte Key { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

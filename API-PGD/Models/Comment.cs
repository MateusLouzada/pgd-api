namespace API_PGD.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}

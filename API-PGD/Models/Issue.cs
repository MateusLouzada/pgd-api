namespace API_PGD.Models
{
    public class Issue
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public Guid? CurrentStageId { get; set; }
        public Guid IssueTypeId { get; set;}
        public Guid? ProjectId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? FinishDate { get; set;}
    }
}

namespace Domain.Entities
{
    public abstract class AuditableOperation
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}

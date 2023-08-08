namespace Domain.Entities
{
    public class Operation : AuditableOperation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public bool IsIncome { get; set; }


        public Operation()
        {
        }

        public Operation(int id, string name, float amount, bool isIncome = false)
        {
            Id = id;
            Name = name;
            Amount = amount;
            IsIncome = isIncome;
        }
    }
}

using Domain.Entities;

namespace Service
{
    public interface IOperationService
    {
        public Operation Post (Operation operation);
        public Operation Get(int id);
        public IEnumerable<Operation> Get();
        public void Put(Operation operation);
        public void Delete (int id);


    }
}

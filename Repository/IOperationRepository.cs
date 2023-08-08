using Domain.Entities;

namespace Repository
{
    public interface IOperationRepository
    {
        Operation CreateOperation(Operation operation);

        IEnumerable<Operation> ReadAllOperations();

        Operation ReadOperationById(int id);

        void UpdateOperation(Operation operation);

        void DeleteOperation(int operationId);

    }
}
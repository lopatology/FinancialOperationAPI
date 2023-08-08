using Domain.Entities;

namespace Repository
{
    internal interface IOperationRepository
    {
        Operation CreateOperation(Operation operation);

        IEnumerable<Operation> ReadAllOperations();

        Operation ReadOperationById(int id);

        void UpdateOperation(Operation operation);

        void DeleteOperation(Operation operation);

    }
}
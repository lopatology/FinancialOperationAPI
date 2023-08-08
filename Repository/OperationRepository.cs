using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<Operation> _operationsEntity;

        public OperationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _operationsEntity = _applicationDbContext.Operations;
        }

        public Operation CreateOperation(Operation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException($"Operation object {nameof(operation)} was null");
            }
            operation.Created = DateTime.UtcNow;
            var resultOperation = _operationsEntity.Add(operation);
            _applicationDbContext.SaveChanges();

            return resultOperation.Entity;
        }

        public IEnumerable<Operation> ReadAllOperations()
        {
            return _operationsEntity;
        }

        public Operation ReadOperationById(int id)
        {
            var result = _operationsEntity.FirstOrDefault(o => o.Id == id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return result;
        }

        public void UpdateOperation(Operation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException();
            }
            var foundOperation = _operationsEntity.Find(operation.Id);

            if (foundOperation == null)
            {
                throw new KeyNotFoundException();
            }
            foundOperation.Amount = operation.Amount;
            foundOperation.Name = operation.Name;
            foundOperation.IsIncome = operation.IsIncome;
            foundOperation.Modified = DateTime.UtcNow;

            _operationsEntity.Update(foundOperation);
            _applicationDbContext.SaveChanges();
        }

        public void DeleteOperation(Operation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException();
            }
            var foundOperationToDelete = _operationsEntity.FirstOrDefault(x => x.Id == operation.Id);
            if (foundOperationToDelete == null)
            {
                throw new KeyNotFoundException(operation.Id.ToString());
            }
            _operationsEntity.Remove(foundOperationToDelete);
            _applicationDbContext.SaveChanges();
        }
    }
}
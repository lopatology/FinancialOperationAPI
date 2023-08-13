using Domain.Entities;
using Repository;

namespace Service
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _repository;

        public OperationService(IOperationRepository repository)
        {
            _repository = repository;
        }
        public Operation Post(Operation operation)
        {
            try
            {
                return _repository.CreateOperation(operation);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public Operation Get(int id)
        {
            try
            {
                return _repository.ReadOperationById(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }

        public IEnumerable<Operation> Get()
        {
            try
            {
                return _repository.ReadAllOperations();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Put(Operation operation)
        {
            try
            {
                _repository.UpdateOperation(operation);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }
        public void Delete(int id)
        {
            try
            {
                _repository.DeleteOperation(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }

    }
}
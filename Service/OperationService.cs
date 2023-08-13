﻿using Domain.Entities;
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
                Operation result = _repository.ReadOperationById(id);
                
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }

        public IEnumerable<Operation> Get()
        {
            return _repository.ReadAllOperations();
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
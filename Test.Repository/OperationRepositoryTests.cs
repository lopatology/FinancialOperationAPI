using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Shouldly;

namespace Test.Repository
{
    public class OperationRepositoryTests
    {
        [Fact]
        public void CreateOperation_Result_ShouldthrowArgumentNullException_WhenPassingOperationObjectIsNull()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            OperationRepository repository = new OperationRepository(_dbContext);

            Operation operation = null;

            Should.Throw<ArgumentNullException>(() => repository.CreateOperation(operation));
        }

        [Fact]
        public void CreateOperation_ResultShouldHaveCreationDateGreaterThanDefaultDateTimeValue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase1");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            OperationRepository repository = new OperationRepository(_dbContext);
            Operation operation = new Operation(1, "Wydatek", 1.5f);
            var result = repository.CreateOperation(operation);

            result.Created.ShouldBeGreaterThan(new DateTime(), "Creation date was no add");
        }

        [Fact]
        public void CreateOperation_ShouldAddNewItemIntoDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase2");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            OperationRepository repository = new OperationRepository(_dbContext);
            Operation operation = new Operation(3, "Wydatek", 1.5f);
            //_dbContext.Set<Operation>().ShouldBeEmpty<Operation>("Database should not be Empty!!");
            repository.CreateOperation(operation);
            _dbContext.Operations.ShouldNotBeEmpty<Operation>("Database is Empty!!");
        }

        [Fact]
        public void ReadAllOperation_ShouldReturnEmptyOperationsList()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase3");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            OperationRepository repository = new OperationRepository(_dbContext);
            var result = repository.ReadAllOperations();

            result.ShouldBeEmpty("Zwrócona kolekcja nie jest pusta");
            result.Count().ShouldBe(0, "Kolekcja nie zawiera nadmiarowe elementy");
        }

        [Fact]
        public void ReadAllOperation_ShouldReturnAllOperationsList()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase4");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);

            // Act
            var result = repository.ReadAllOperations();

            // Assert
            result.ShouldNotBeEmpty("Zwrócona kolekcja jest pusta");
            result.Count().ShouldBe(2, "Kolekcja nie zawiera okreœlonej liczby elementów");
        }


        [Fact]
        public void ReadOperationById_ShouldReturnSpecyficOperation()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase5");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var operation4 = new Operation(4, "Wydatek1", 10.0f);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                operation4,
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);

            // Act
            var result = repository.ReadOperationById(4);

            // Assert
            result.ShouldBeEquivalentTo(operation4);
            result.Id.ShouldBe(operation4.Id);
            result.Name.ShouldBe(operation4.Name);
            result.Amount.ShouldBe(operation4.Amount);
        }

        [Fact]
        public void ReadOperationById_ShouldThrowKeyNotFoundExceptionIfOperationWasNotFoundInDatabase()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase6");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);

            // Act, Assert
            Should.Throw<KeyNotFoundException>(() => repository.ReadOperationById(7));
        }


        [Fact]
        public void UpdateOperation_ShouldThrowArgumentNullExceptionIfOperationIsNull()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase7");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);
            Operation operationToUpdate = null;

            // Act, Assert
            Should.Throw<ArgumentNullException>(() => repository.UpdateOperation(operationToUpdate));
        }


        [Fact]
        public void UpdateOperation_ShouldThrowKeyNotFoundExceptionIfOperationNotFoundInDatabase()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase8");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            Operation operationToUpdate = new Operation(99, "operation update", 500f);

            OperationRepository repository = new OperationRepository(_dbContext);

            // Act, Assert
            Should.Throw<KeyNotFoundException>(() => repository.UpdateOperation(operationToUpdate));
        }

        [Fact]
        public void UpdateOperation_UpdateOpeartionIfOperationWasFoundInDatabase()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase9");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            Operation operationToUpdate = new Operation(4, "WydatekUpdate", 10.0f);

            OperationRepository repository = new OperationRepository(_dbContext);

            // Act
            repository.UpdateOperation(operationToUpdate);

            // Assert
            var resultOperationFromDBState = _dbContext.Operations.FirstOrDefault(x => x.Id == operationToUpdate.Id);
            resultOperationFromDBState.Name.ShouldBe(operationToUpdate.Name);
            resultOperationFromDBState.Amount.ShouldBe(operationToUpdate.Amount);
            resultOperationFromDBState.IsIncome.ShouldBe(operationToUpdate.IsIncome);
            resultOperationFromDBState.Modified.ShouldNotBeNull();
        }


        [Fact]
        public void DeleteOperation_ShouldThrowArgumentNullExceptionIfOperationIsNull()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase10");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);
            Operation operationToDelete = null;

            // Act, Assert
            Should.Throw<ArgumentNullException>(() => repository.DeleteOperation(operationToDelete));
        }


        [Fact]
        public void DeleteOperation_ShouldThrowKeyNotFoundExceptionIfOperationIsNull()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase11");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IEnumerable<Operation> operations = new List<Operation>()
            {
                new Operation(4, "Wydatek1", 10.0f),
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);
            Operation operationToDelete = new Operation(10, "Wydatek10", 10.0f);

            // Act, Assert
            Should.Throw<KeyNotFoundException>(() => repository.DeleteOperation(operationToDelete));
        }


        [Fact]
        public void DeleteOperation_ShouldDeleteOperationFromDB()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "OperationsDatabase12");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            Operation operationToDelete = new Operation(4, "Wydatek1", 10.0f);

            IEnumerable<Operation> operations = new List<Operation>()
            {
                operationToDelete,
                new Operation(5, "Wydatek2", 10.0f)
            };
            _dbContext.Operations.AddRange(operations);
            _dbContext.SaveChanges();

            OperationRepository repository = new OperationRepository(_dbContext);

            // Act
            repository.DeleteOperation(operationToDelete);

            // Assert
            _dbContext.Operations.ToList().FirstOrDefault(x => x.Id == operationToDelete.Id).ShouldBeNull();
            _dbContext.Operations.Count().ShouldBe(1);
        }

    }
}
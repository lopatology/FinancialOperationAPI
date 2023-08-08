using Domain.Entities;
using Moq;
using Repository;
using Service;
using Shouldly;

namespace Test.Service
{
    public class OperatonServiceTest
    {
        [Fact]
        public void Post_ShouldReturnAddedTypeOperation_RepositoryMethodInvokedOnce()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = new Operation
            {
                Id = 1,
                Name = "Wydatek1",
                Amount = 10f,
                IsIncome = false,
                Created = DateTime.UtcNow
            };
            repoMock.Setup(x => x.CreateOperation(It.IsAny<Operation>())).Returns(operation);

            OperationService service = new OperationService(repoMock.Object);
            var result = service.Post(operation);

            Assert.NotNull(result);
            result.ShouldBeOfType<Operation>();
            repoMock.Verify(x => x.CreateOperation(It.IsAny<Operation>()), Times.Once());
        }

        [Fact]
        public void Post_ShouldHandledArgumentNullExceptionThrownByRepositoryMethodInvoke()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = null;
            repoMock.Setup(x => x.CreateOperation(operation)).Throws(new ArgumentNullException());

            OperationService service = new OperationService(repoMock.Object);

            Should.Throw<ArgumentNullException>(() => service.Post(operation));
            repoMock.Verify(x => x.CreateOperation(null), Times.Once());
        }


        [Fact]
        public void Get_ShouldReturnSingleOperationResult_RepoReadByIdMethodInvokedOnce()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = new Operation
            {
                Id = 1,
                Name = "Wydatek1",
                Amount = 10f,
                IsIncome = false,
                Created = DateTime.UtcNow
            };
            repoMock.Setup(x => x.ReadOperationById(It.IsAny<int>())).Returns(operation);

            OperationService service = new OperationService(repoMock.Object);
            var result = service.Get(1);

            Assert.NotNull(result);
            result.ShouldBeOfType<Operation>();
            repoMock.Verify(x => x.ReadOperationById(It.IsAny<int>()), Times.Once());
        }


        [Fact]
        public void Get_ShouldHandledKeyNotFoundExceptionThrownBy_RepositoryMethodInvoke()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            repoMock.Setup(x => x.ReadOperationById(1)).Throws(new KeyNotFoundException());

            OperationService service = new OperationService(repoMock.Object);

            Should.Throw<KeyNotFoundException>(() => service.Get(1));
            repoMock.Verify(x => x.ReadOperationById(1), Times.Once());
        }

        [Fact]
        public void Get_ShouldReturnCollectionOfOperationObjectResult_RepoMethodInvokedOnce()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            IEnumerable<Operation> resultList = new List<Operation>(){
                new Operation(),
                new Operation()
            };
            repoMock.Setup(x => x.ReadAllOperations()).Returns(resultList);

            OperationService service = new OperationService(repoMock.Object);
            var result = service.Get();

            Assert.NotNull(result);
            result.Count().ShouldBe(2);
            repoMock.Verify(x => x.ReadAllOperations(), Times.Once());
        }

        [Fact]
        public void Put_ShouldUpdateOperation_RepoMethodInvokedOnce()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = new Operation();
            repoMock.Setup(x => x.UpdateOperation(It.IsAny<Operation>()));

            OperationService service = new OperationService(repoMock.Object);
            service.Put(operation);

            repoMock.Verify(x => x.UpdateOperation(It.IsAny<Operation>()), Times.Once());
        }

        [Fact]
        public void Put_ShouldHandledKeyNotFoundExceptionThrownBy_RepositoryMethodInvoke()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = new Operation();
            repoMock.Setup(x => x.UpdateOperation(It.IsAny<Operation>())).Throws<KeyNotFoundException>();

            OperationService service = new OperationService(repoMock.Object);
            

            Should.Throw<KeyNotFoundException>(() => service.Put(operation));
            repoMock.Verify(x => x.UpdateOperation(It.IsAny<Operation>()), Times.Once());
        }

        [Fact]
        public void Put_ShouldHandledArgumentNullExceptionThrownBy_RepositoryMethodInvoke()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = null;
            repoMock.Setup(x => x.UpdateOperation(It.IsAny<Operation>())).Throws<ArgumentNullException>();

            OperationService service = new OperationService(repoMock.Object);
            

            Should.Throw<ArgumentNullException>(() => service.Put(operation));
            repoMock.Verify(x => x.UpdateOperation(It.IsAny<Operation>()), Times.Once());
        }

        [Fact]
        public void Delete_ShouldHandledKeyNotFoundExceptionThrownBy_RepositoryMethodInvoke()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = new Operation();
            repoMock.Setup(x => x.DeleteOperation(It.IsAny<int>())).Throws<KeyNotFoundException>();

            OperationService service = new OperationService(repoMock.Object);


            Should.Throw<KeyNotFoundException>(() => service.Delete(99));
            repoMock.Verify(x => x.DeleteOperation(It.IsAny<int>()), Times.Once());
        }


        [Fact]
        public void Delete_ShouldDeleteOperation_RepoMethodInvokedOnce()
        {
            Mock<IOperationRepository> repoMock = new Mock<IOperationRepository>();
            Operation operation = new Operation();
            repoMock.Setup(x => x.DeleteOperation(It.IsAny<int>()));

            OperationService service = new OperationService(repoMock.Object);
            service.Delete(1);

            repoMock.Verify(x => x.DeleteOperation(It.IsAny<int>()), Times.Once());
        }
    }
}
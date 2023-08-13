using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Moq;
using Service;
using Shouldly;
using System;
using WebApi.Controllers;

namespace Test.WebApi
{
    public class OperationControllerTest
    {
        Mock<IOperationService> _service = new Mock<IOperationService>();
        Operation operation1 = new Operation(1, "Nazwa1", 100f, true);
        Operation operation2 = new Operation(2, "Nazwa2", 200f, false);

        public OperationControllerTest()
        {        
            _service.Setup(x => x.Get(1)).Returns(operation1);
            _service.Setup(x => x.Get(5)).Returns<Operation>(null);
            _service.Setup(x => x.Get()).Returns(new List<Operation>(){operation1, operation2});
        }
        [Fact]
        public void GetOperationById_ShouldReturnSingleOperationResultAndOkResponseCode()
        {
            // Arrange            
            OperationController controller = new OperationController(_service.Object);
            
            // Act
            IActionResult actionResult = controller.GetOperationByID(1);
            actionResult.ShouldBeOfType<OkObjectResult>();
            
            // Assert
            var okActionResult = actionResult as OkObjectResult;
            okActionResult.StatusCode.ShouldBe(200);
            var resultOperation = okActionResult.Value as Operation;
            resultOperation.Id.ShouldBe(operation1.Id);
            resultOperation.Name.ShouldBe(operation1.Name);
            resultOperation.Amount.ShouldBe(operation1.Amount);
            resultOperation.IsIncome.ShouldBe(operation1.IsIncome);
            resultOperation.Created.ShouldBe(operation1.Created);
        }

        [Fact]
        public void GetOperationById_ShouldHandleThrowKeyNotFoundExceptionAndNotFoundResponseCode()
        {
            // Arrange            
            OperationController controller = new OperationController(_service.Object);

            // Act
            IActionResult actionResult = controller.GetOperationByID(5);
            actionResult.ShouldBeOfType<NotFoundResult>();

            // Assert
            var notFoundActionResult = actionResult as NotFoundResult;
            notFoundActionResult.StatusCode.ShouldBe(404);
        }


        [Fact]
        public void GetAllOperations_ShouldReturnCollectionOfOperationsResultAndOkResponseCode()
        {
            // Arrange            
            OperationController controller = new OperationController(_service.Object);

            // Act
            IActionResult actionResult = controller.GetAllOperations();
            actionResult.ShouldBeOfType<OkObjectResult>();

            // Assert
            var okActionResult = actionResult as OkObjectResult;
            okActionResult.StatusCode.ShouldBe(200);
            var resultOperation = okActionResult.Value as IEnumerable<Operation>;
            resultOperation.Count().ShouldBe(2);
        }

        [Fact]
        public void PostOperation_ShouldInserdOperationAndOkResponseCode()
        {
            // Arrange            
            OperationController controller = new OperationController(_service.Object);
            Operation operation = new Operation(3, "Name3", 300f, true);
            // Act
            IActionResult actionResult = controller.PostOperation(operation);
            actionResult.ShouldBeOfType<OkObjectResult>();

            // Assert
            var okActionResult = actionResult as OkObjectResult;
            okActionResult.StatusCode.ShouldBe(200);
        }
    }
}
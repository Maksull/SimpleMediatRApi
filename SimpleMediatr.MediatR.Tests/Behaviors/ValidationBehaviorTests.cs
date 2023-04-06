using FluentValidation;
using FluentValidation.Results;
using SimpleMediatr.MediatR.Behaviors;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Tests.Behaviors
{
    public sealed class ValidationBehaviorTests
    {
        private readonly Mock<IValidator<CreateProductCommand>> _validator;
        private readonly List<IValidator<CreateProductCommand>> _validators;
        private readonly ValidationBehavior<CreateProductCommand, Product> _behavior;

        public ValidationBehaviorTests()
        {
            _validator = new();
            _validators = new() { _validator.Object};
            _behavior = new(_validators);
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var request = new CreateProductCommand(new Product());

            _validator.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateProductCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Name", "Name is required.") }));

            // Act
            Func<Task> act = async () => await _behavior.Handle(request, () => Task.FromResult(new Product()), CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_WithValidRequest_CallsNext()
        {
            // Arrange
            var request = new CreateProductCommand(new Product());

            _validator.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateProductCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());


            // Act
            var result = await _behavior.Handle(request, () => Task.FromResult(new Product()), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }
    }
}

using System.Security.Cryptography;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Validation;
using FluentAssertions;

namespace Backend.Domain.Tests;

public class UserUnitTest1
{
    [Fact(DisplayName = "Create User With Valid State")]
    public void CreateUser_WithValideUser_ResultObjectValidState()
    {
        Action action = () => new User("admin", "admin@admin" , "12345678");

        action
            .Should()
            .NotThrow<DomainExceptionValidation>();
        
    }
    [Fact]
    public void CreateUser_InvalidName_DomainExceptionValidation()
    {
        Action action = () => new User( "ad", "admin@admin", "12345678");

        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minumum 3 caracters");
    }
    [Fact]
    public void CreateUser_EmptyName_DomainExceptionValidate()
    {
       Action action = () => new User( "", "admin@admin", "12345678");

       action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, Name is required");
    
    }
    [Fact]
    public void CreateUser_EmptyEmail_DomainExceptionValidate()
    {
        Action action = () => new User( "admin", "", "12345678");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid email, Email is required");
    }
    [Fact]
    public void CreateUser_ShortEmail_DomainExceptionValidation()
    {
        Action action = () => new User("admin", "adm", "12345678");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minumum 3 caracters");
    }
    [Fact]
    public void CreateUser_InvalidEmail_DomainExceptionValidation()
    {
        Action action = () => new User("admin", "admin@eu.com", "12345678");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Email, Valid Email is @gmail.com or @hotmal.com");
    }
    [Fact]
    public void CreateUser_EmptyPassword_DomainExceptionValidate()
    {
        Action action =  () => new User("admin", "admin@admin", "");

        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid password, Password is required");
    }
    [Fact]
    public void CreateUser_ShortPassword_DomainExceptionValidate()
    {
        Action action = () => new User("admin", "admin@admin", "12345");

        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid password, too short, minumum 8 caracters");
    }
}
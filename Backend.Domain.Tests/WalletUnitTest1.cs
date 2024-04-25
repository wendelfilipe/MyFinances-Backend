using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Validation;
using FluentAssertions;

namespace Backend.Domain.Tests
{
    public class WalletUnitTest1
    {
        [Fact]
        public void CreateWallet_WithValideWallet_ResultObjectValidState()
        {
           Action action = () => new Wallet( "x" );
           action
                .Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateWallet_InvalidWalletName_DomainExceptionValidation()
        { 
            Action action = () => new Wallet( "" );
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, Name is required");
        }
        [Fact]
        public void CreateWallet_NullWalletName_DomainExceptionValidation()
        {
            Action action = () => new Wallet( null );
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, Name is required");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Validation;
using FluentAssertions;

namespace Backend.Domain.Tests
{
    public class AssetsUnitTest1
    {
        [Fact(DisplayName = "Create Assets with valid value")]
        public void CreateAssets_WithValidAssets_ResultObjectValidState()
        {
            Action action = ()=> new Assets("PETR4", 41.23m);
            action
                .Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateAssets_ShortAssetsCodName_DomainExceptionValidation()
        {
            Action action = () => new Assets( "PETR", 41.23m);
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("CodName is Invalid, CodName is short");
        }
        [Fact]
        public void CreateAssets_EmptyAssetsCodName_DomainExceptionValidation()
        {
            Action action = () => new Assets("", 41.23m);
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("CodName is Required");
        }
        [Fact]
         public void CreateAssets_NullAssetsCodName_DomainExceptionValidation()
        {
            Action action = () => new Assets( null, 41.23m );
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("CodName is Required");
        }
        [Fact]
        public void CreateAssets_NegativeCurrentPrice_DomainExceptionValidation()
        {
            Action action = () => new Assets("PETR4", -41.23m);
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid CurrentPrice, invalid value");
        }
    }
}
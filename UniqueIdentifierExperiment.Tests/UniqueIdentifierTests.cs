using System;
using FluentAssertions;
using NUnit.Framework;
using UniqueIdentifierExperiment.Tests.Fixtures;

namespace UniqueIdentifierExperiment.Tests
{
    [TestFixture]
    public class UniqueIdentifierTests
    {
        [Test]
        public void ShouldCreateBasicEntityId()
        {
            // setup
            var entity = new EntityWithoutConstraints();
            
            // execute
            entity.CreateNewId();
            var isGuid = Guid.TryParse(entity.Id, out _);
            
            // assert
            entity.Id.Should().NotBeEmpty();
            isGuid.Should().BeTrue();
        }
        
        [Test] 
        public void ShouldCreateKnownEntityIdFromUniqueConstraint()
        {
            // setup
            const int expectedCropYear = 2020;
            var expectedId = $"'{expectedCropYear}','{EntityFixture.OneId}'";
            var newEntity = new EntityWithConstraintsOne
            {
                CropYear = expectedCropYear,
                EntityWithoutConstraints = EntityFixture.One,
                EntityWithoutConstraintsId = EntityFixture.OneId
            };
            
            // execute
            newEntity.CreateNewId();

            // assert
            newEntity.Id.Should().Be(expectedId);
        }


        [Test]
        public void ShouldCreateKnownEntityIdsFromComplexUniqueConstraints()
        {
            // setup
            const int expectedCropYear = 2020;
            const int expectedComplexCropYear = 2012;
            var parentId = $"'{expectedCropYear}','{EntityFixture.OneId}'";
            var expectedComplexId = $"'{expectedComplexCropYear}','{EntityFixture.OneId}','{parentId}'";
            var parentEntity = new EntityWithConstraintsOne
            {
                CropYear = expectedCropYear,
                EntityWithoutConstraints = EntityFixture.One,
                EntityWithoutConstraintsId = EntityFixture.OneId
            };
            // execute
            parentEntity.CreateNewId();
            
            // setup
            var complexEntity = new EntityWithConstraintsTwo
            {
                CropYear = expectedComplexCropYear,
                EntityWithoutConstraints = EntityFixture.One,
                EntityWithoutConstraintsId = EntityFixture.OneId,
                Parent = parentEntity,
                ParentId = parentEntity.Id
            };
            
            // execute
            complexEntity.CreateNewId();

            // assert
            parentEntity.Id.Should().Be(parentId);
            complexEntity.Id.Should().Be(expectedComplexId);
        }
    }
}
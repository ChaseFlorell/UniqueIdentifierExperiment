# UniqueIdentifierExperiment

```csharp
        [Test]
        public void ShouldCreateBasicEntityId()
        {
            // setup
            var entity = new EntityWithoutConstraints();
            
            // execute
            UniqueIdentifierGenerator.SetId(ref entity);
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
            var newEntity = new EntityWithConstraints
            {
                CropYear = expectedCropYear,
                EntityWithoutConstraints = EntityFixture.One,
                EntityWithoutConstraintsId = EntityFixture.OneId
            };
            
            // execute
            UniqueIdentifierGenerator.SetId(ref newEntity);

            // assert
            newEntity.Id.Should().Be(expectedId);
        }
```

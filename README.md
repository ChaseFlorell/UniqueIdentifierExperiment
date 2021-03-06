# UniqueIdentifierExperiment

This is a similar approach to what the Azure Sync Framework has done with [CompositeTableKey](https://docs.microsoft.com/ko-kr/dotnet/api/microsoft.azure.mobile.server.compositetablekey?view=azure-dotnet). Details can be found in [this blog post](https://wp.sjkp.dk/azure-mobile-service-net-backend-using-azure-table-storage-part-2/)

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
```

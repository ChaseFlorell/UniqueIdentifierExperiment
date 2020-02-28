namespace UniqueIdentifierExperiment.Tests.Fixtures
{
    public static class EntityFixture
    {
        public const string OneId = "8B262C8C-5CEA-4E58-BDB9-AD6B0B0C678C";
        public static EntityWithoutConstraints One => new EntityWithoutConstraints
        {
            Id = OneId
        };
    }
}
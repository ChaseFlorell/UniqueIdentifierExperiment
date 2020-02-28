namespace UniqueIdentifierExperiment.Tests.Fixtures
{
    [UniqueOn(new []{nameof(CropYear), nameof(EntityWithoutConstraintsId)})]
    public class EntityWithConstraints : ITableEntity
    {
        public string Id { get; set; }
        public int CropYear { get; set; }
        public string EntityWithoutConstraintsId { get; set; }
        public EntityWithoutConstraints EntityWithoutConstraints { get; set; }
    }
}
namespace UniqueIdentifierExperiment.Tests.Fixtures
{
    [UniqueOn(new []{nameof(CropYear), nameof(EntityWithoutConstraintsId), nameof(ParentId)})]
    public class EntityWithConstraintsTwo : ITableEntity
    {
        public string Id { get; set; }
        public int CropYear { get; set; }
        public string ParentId { get; set; }
        public EntityWithConstraintsOne Parent { get; set; }
        public string EntityWithoutConstraintsId { get; set; }
        public EntityWithoutConstraints EntityWithoutConstraints { get; set; }
    }
}
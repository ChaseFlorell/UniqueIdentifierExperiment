using System;
using System.Linq;

namespace UniqueIdentifierExperiment
{
    public static class IdGeneratorMixin
    {
        public static string CreateNewId<TEntity>(this TEntity entity) where TEntity : ITableEntity
        {
            var entityType = typeof(TEntity);
            var uniqueOnAttribute = (UniqueOnAttribute)Attribute.GetCustomAttribute(entityType, typeof(UniqueOnAttribute));
            
            if (uniqueOnAttribute is null)
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var uniqueColumns = uniqueOnAttribute.GetColumns();
                var values = uniqueColumns
                    .Select(column => entityType.GetProperty(column)?.GetValue(entity, null))
                    .Select(value => $"'{value}'")
                    .Cast<object>()
                    .ToList();

                entity.Id = string.Join(",", values);
            }
            
            return entity.Id;
        }
    }
}
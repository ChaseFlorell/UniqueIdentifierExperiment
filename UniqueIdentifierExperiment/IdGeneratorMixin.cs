using System;
using System.Collections.Generic;
using System.Linq;

namespace UniqueIdentifierExperiment
{
    public static class UniqueIdentifierGenerator
    {
        public static void SetId<TEntity>(this TEntity newEntity) where TEntity : ITableEntity
        {
            var entityType = typeof(TEntity);
            var uniqueOnAttribute = (UniqueOnAttribute)Attribute.GetCustomAttribute(entityType, typeof(UniqueOnAttribute));
            
            if (uniqueOnAttribute is null)
            {
                newEntity.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var uniqueColumns = uniqueOnAttribute.GetColumns();
                var values = uniqueColumns
                    .Select(column => entityType.GetProperty(column)?.GetValue(newEntity, null))
                    .Select(value => $"'{value}'").
                    Cast<object>()
                    .ToList();

                newEntity.Id = string.Join(",", values);
            }
        }
    }
}
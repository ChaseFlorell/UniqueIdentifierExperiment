using System;
using System.Collections.Generic;

namespace UniqueIdentifierExperiment
{
    public class UniqueIdentifierGenerator
    {
        public static void SetId<TEntity>(ref TEntity newEntity) where TEntity : ITableEntity
        {
            var entityType = typeof(TEntity);
            var uniqueOnAttribute = (UniqueOnAttribute)Attribute.GetCustomAttribute(entityType, typeof(UniqueOnAttribute));
            
            if (uniqueOnAttribute is null)
            {
                newEntity.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var values = new List<object>();
                var uniqueColumns = uniqueOnAttribute.GetColumns();
                foreach (var column in uniqueColumns)
                {
                    var value = entityType.GetProperty(column)?.GetValue(newEntity, null);
                    var formatted = $"'{value}'";
                    values.Add(formatted);
                }

                newEntity.Id = string.Join(",", values);
            }
        }
    }
}
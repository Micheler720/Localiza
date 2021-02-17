using System;

namespace Domain.UseCase.Builder
{
    public class EntityBuilder
    {
        public static T Call<T>(object data)
        {
            var entity = Activator.CreateInstance<T>();
            foreach (var field in data.GetType().GetProperties())
            {
                var value = data.GetType().GetProperty(field.Name).GetValue(data);
                if(value != null)
                {
                    var prop = entity.GetType().GetProperty(field.Name);
                    if(prop != null) prop.SetValue(entity, value);
                }
            }

            return entity;
        }
    }
}
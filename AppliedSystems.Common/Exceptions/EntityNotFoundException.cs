using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AppliedSystems.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public sealed class EntityNotFoundException<T> : Exception where T : class
    {
        public T Entity { get; set; }

        public EntityNotFoundException(T entity)
            : base(GetMessage(entity))
        {
            Entity = entity;
        }

        private static string GetMessage(T entity)
        {
            var highLevelMessage = string.Format("{0} could not be found.", entity.GetType().Name);

            var serializedObject = JsonConvert.SerializeObject(entity,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return string.Format("{0}\r\n\r\n{1}", highLevelMessage, serializedObject);
        }
    }
}

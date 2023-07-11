using Cinemark.Domain.Core.Commom;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Cinemark.Infrastructure.Data.Mapping.Mongo
{
    public class EntityMap
    {
        public static void Configure()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
            {
                BsonClassMap.RegisterClassMap<Entity>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapIdMember(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                });               
            }
        }
    }
}

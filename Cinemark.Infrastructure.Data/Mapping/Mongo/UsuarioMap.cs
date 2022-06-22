using Cinemark.Domain.Models;
using MongoDB.Bson.Serialization;

namespace Cinemark.Infrastructure.Data.Mapping.Mongo
{
    public class UsuarioMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Usuario>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapIdMember(x => x.Nome).SetIsRequired(true);
                map.MapIdMember(x => x.Email).SetIsRequired(true);
                map.MapIdMember(x => x.Senha).SetIsRequired(true);
                map.MapIdMember(x => x.DataCriacao).SetIsRequired(true);
            });
        }
    }
}

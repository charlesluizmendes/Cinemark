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
                map.MapIdMember(x => x.Id).SetIsRequired(true); ;
                map.MapProperty(x => x.Nome).SetIsRequired(true);
                map.MapProperty(x => x.Email).SetIsRequired(true);
                map.MapProperty(x => x.Senha).SetIsRequired(true);
                map.MapProperty(x => x.DataCriacao).SetIsRequired(true);
            });
        }
    }
}

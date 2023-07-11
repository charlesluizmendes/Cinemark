using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using MongoDB.Bson.Serialization;

namespace Cinemark.Infrastructure.Data.Mapping.Mongo
{
    public class UsuarioMap
    {
        public static void Configure()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Usuario)))
            {
                BsonClassMap.RegisterClassMap<Usuario>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapProperty(x => x.Nome).SetIsRequired(true);
                    map.MapProperty(x => x.Email).SetIsRequired(true);
                    map.MapProperty(x => x.Senha).SetIsRequired(true);
                    map.MapProperty(x => x.DataCriacao).SetIsRequired(true);
                });
            }
                
        }
    }
}

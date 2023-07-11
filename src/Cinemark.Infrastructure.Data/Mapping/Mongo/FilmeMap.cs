using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MongoDB.Bson.Serialization;

namespace Cinemark.Infrastructure.Data.Mapping.Mongo
{
    public class FilmeMap
    {
        public static void Configure()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Filme)))
            {
                BsonClassMap.RegisterClassMap<Filme>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapProperty(x => x.Nome).SetIsRequired(true);
                    map.MapProperty(x => x.Categoria).SetIsRequired(true);
                    map.MapProperty(x => x.FaixaEtaria).SetIsRequired(true);
                    map.MapProperty(x => x.DataLancamento).SetIsRequired(true);
                });
            }                
        }
    }
}

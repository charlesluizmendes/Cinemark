﻿using Cinemark.Domain.Models;
using MongoDB.Bson.Serialization;

namespace Cinemark.Infrastructure.Data.Mapping.Mongo
{
    public class FilmeMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Filme>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapIdMember(x => x.Nome).SetIsRequired(true);
                map.MapIdMember(x => x.Categoria).SetIsRequired(true);
                map.MapIdMember(x => x.FaixaEtaria).SetIsRequired(true);
                map.MapIdMember(x => x.DataLancamento).SetIsRequired(true);
            });
        }
    }
}

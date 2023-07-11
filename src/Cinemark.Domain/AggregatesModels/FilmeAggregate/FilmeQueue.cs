using Cinemark.Domain.Core.Commom;

namespace Cinemark.Domain.AggregatesModels.FilmeAggregate
{
    public class FilmeQueue : Enumeration
    {
        public static FilmeQueue Created = new(1, nameof(Created));
        public static FilmeQueue Updated = new(2, nameof(Updated));
        public static FilmeQueue Removed = new(3, nameof(Removed));

        public FilmeQueue(int id, string name) 
            : base(id, name)
        {
        }
    }
}

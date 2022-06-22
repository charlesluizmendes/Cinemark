namespace Cinemark.Domain.Models
{
    public class Token
    {       
        public virtual string? AccessKey { get; set; }       
        public virtual string? ValidTo { get; set; }
    }
}

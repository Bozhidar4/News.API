using News.Domain.Core;

namespace News.Domain.Countries
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

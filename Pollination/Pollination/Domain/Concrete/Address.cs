namespace Pollination.Domain.Concrete
{
    public struct Address
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public override string ToString()
        {
            return string.Format("Country: {0}, City: {1}, Street: {2}", Country, City, Street);
        }
    }
}
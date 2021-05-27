namespace Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }        
        public string Phone { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }        
        public string LogoUrl { get; set; }       
        public string Description { get; set; }
    }
}
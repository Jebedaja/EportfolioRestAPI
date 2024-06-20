namespace EportfolioRestAPI.Entities
{
    public class Portofolio
    {
        public int Id { get; set; }
        public string PortofolioName { get; set; }   
        public string Description { get; set; }


        public int ContactInfoId { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }

        public virtual List<Information> Informations { get; set; }

    }
}

namespace EportfolioRestAPI.Entities
{
    public class Information
    {
        public int Id { get; set; }

        public string AboutMe { get; set; }
        public string MyGear { get; set; }
        public string Music { get; set; }


        public int PortofolioId { get; set; }
        public virtual Portofolio Portofolio { get; set; }






    }
}

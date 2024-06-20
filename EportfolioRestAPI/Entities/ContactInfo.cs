namespace EportfolioRestAPI.Entities
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Nickname { get; set; }
       
        public string Email { get; set; }
        public string PhoneNumber { get; set; }  


        public virtual Portofolio Portofolio { get; set; }
    }
}

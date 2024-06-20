using EportfolioRestAPI.Entities;

namespace EportfolioRestAPI                                 // Serwis do seedowania danych, zeby nie robic tego w dbcontext
{
   
    public class PortofolioSeeder
    {

        private readonly PortolioDbContext _dbContext;

        public PortofolioSeeder(PortolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        
        
        
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())                       // Czy da sie połączyć z bazą danych
            {
                if (!_dbContext.Portofolios.Any())                      // Czy jest już jakiś content w danej tabelce
                {
                    var portofolios = GetPortofolios();
                    _dbContext.Portofolios.AddRange(portofolios);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Portofolio> GetPortofolios()                    // populacja danymi
        {
            var portofolios = new List<Portofolio>()
            {
                new Portofolio()
                {
                    PortofolioName = "ErykSpiewak",
                    Description = "Spiewam piosenki Disney",

                    Informations = new List<Information>()
                    {
                        new Information()
                        {
                            AboutMe = "Od lat śpiewam w chórze",
                            MyGear = "Audiotechnica CS-1000",
                            Music = "Pop, Light, Folk"
                        }
                    },

                    ContactInfo = new ContactInfo()
                    {
                        Name = "Eryk",
                        Surname = "Nowak",
                        Nickname = "Nowaczek111",
                        Email = "Eryk.Nowak@gmail.com",
                        PhoneNumber = "+48666222333"
                    }
                },
                new Portofolio()
                    {
                    PortofolioName = "JanRepairGuita",
                    Description = "Lutnik z Zielonej Góry",

                    Informations = new List<Information>()
                    {
                        new Information()
                        {
                            AboutMe = "Mam długoletnie doświadczenie",
                            MyGear = "Fender FZ900",
                            Music = "Post-Rock, Acoustic"
                        }
                    },

                    ContactInfo = new ContactInfo()
                    {
                        Name = "Jan",
                        Surname = "Kowalski",
                        Nickname = "Jan456",
                        Email = "Jan456@gmail.com",
                        PhoneNumber = "+48123456789"


                    }
                }
            };

            return portofolios;
        }

    }     
    
}

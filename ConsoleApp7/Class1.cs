
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp7
{
    public class AirplaneDbContext : DbContext
    {
        public AirplaneDbContext()
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();

        }
        //Collections
        public DbSet<Client> Clients { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-83U7DVV\SQLEXPRESS;
                                        Initial Catalog = NewAirplane;
                                        Integrated Security=True;
                                        Connect Timeout=2;Encrypt=False;
                                         Trust Server Certificate=False;
                                        Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Initializator - Seeder
            modelBuilder.Entity<Airplane>().HasData(new Airplane[]
            {
                new Airplane()
                {
                    Id = 1,
                    Model = "Boing747",
                    MaxPassanger = 300
                },
                new Airplane()
                {
                    Id = 2,
                    Model = "AN914",
                    MaxPassanger = 200
                },
                new Airplane()
                {
                    Id = 3,
                    Model = "Mria",
                    MaxPassanger = 150
                }
            });
            modelBuilder.Entity<Flight>().HasData(new Flight[] {
                new Flight()
                {
                     Number = 1,
                     DepartureCity = "Kyiv",
                     ArrivalCity = "Lviv",
                     DepartureTime = new DateTime(2024,2,17),
                     ArrivalTime = new DateTime(2024,2,17),
                  
                     AirplaneId = 1
                },
                new Flight()
                {
                     Number = 2,
                     DepartureCity = "Varshava",
                     ArrivalCity = "Lviv",
                     DepartureTime = new DateTime(2024,2,18),
                     ArrivalTime = new DateTime(2024,2,18),
                     
                     AirplaneId = 2
                },
                new Flight()
                {
                     Number = 3,
                     DepartureCity = "Kyiv",
                     ArrivalCity = "Lviv",
                     DepartureTime = new DateTime(2024,2,22),
                     ArrivalTime = new DateTime(2024,2,22),
                     
                     AirplaneId = 3
                }
            });
        }

    }

    public class Airplane
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Model { get; set; }
        public int MaxPassanger { get; set; }

        //Navigation properties

        //Relational type : Many to Many (*....*)
        public ICollection<Flight> Flights { get; set; }
    }

    //Entities
    [Table("Passangers")]
    public class Client
    {
        public int Id { get; set; }
        [Required]//null ---> not null
        [MaxLength(100)]//nvarchar(100)
        [Column("FirstName")]//set column name
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }//not null ===> null

        //Navigation properties

        //Relational type : Many to Many (*....*)
        public ICollection<Flight> Flights { get; set; }
    }
    public class Flight
    {
        //Primary key naming : Id/id/ID / EntityName+Id = FlightId
        [Key]//Primary key
        public int Number { get; set; }
        [Required, MaxLength(100)]
        public string DepartureCity { get; set; }
        [Required, MaxLength(100)]
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        [MaxLength(100)]


        //Navigation properties
        //Relational type : One to Many (1....*)
        //Foreign key naming : RelatedEntityName + RelatedEntityPrimaryKeyName
        public int AirplaneId { get; set; }//foreign key
        public Airplane Airplane { get; set; }//null
                                              //Relational type : Many to Many (*....*)

        //Navigation properties
        public ICollection<Client> Clients { get; set; }

    }

}

using System;
namespace Domain
{
    public class Client

    {

        public Client(Guid id, string clientName, string adress, string city, string postalCode, string country){
            this.Id = id;
            this.ClientName = clientName;
            this.Adress = adress;
            this.City = city;
            this.PostalCode = postalCode;
            this.Country = country;
        }
        public Guid Id { get; private set; }
        public string ClientName { get;private set; }
        public string Adress { get;private set; }
        public string City { get; private set; }
        public string PostalCode{get; private set;}
        public string Country {get;private set;}

        public Client UpdateName(string name)
        {
            return new Client(this.Id, name ?? this.ClientName, this.Adress, this.City, this.PostalCode, this.Country);
        }

        public Client UpdateAddress(string address)
        {
            return new Client(this.Id, this.ClientName, address ?? this.Adress, this.City, this.PostalCode, this.Country);
        }
        public Client UpdateCity(string city)
        {
            return new Client(this.Id,this.ClientName,this.Adress, city ?? this.City,this.PostalCode,this.Country);
        }
         public Client UpdatePostal(string postalcode)
        {
            return new Client(this.Id,this.ClientName,this.Adress,this.City,postalcode ?? this.PostalCode,this.Country);
        }
         public Client UpdateCountry(string country)
        {
            return new Client(this.Id,this.ClientName,this.Adress,this.City,this.PostalCode,country ?? this.Country);
        }
    }
}

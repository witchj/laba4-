using System;

namespace Hairdresser_s
{
    [Serializable]
    public class HairStyle
    {
        public string haircutName { get; set; }
        public Client client { get; set; }
        public Barber barber { get; set; }
        public double price { get; set; }
        public bool additionalServices { get; set; }
        public HairStyle() { }
        public HairStyle(string haircutName, Client client, Barber barber, double price, bool additionalServices)
        {
            this.haircutName = haircutName;
            this.client = client;
            this.barber = barber;
            this.price = price;
            this.additionalServices = additionalServices;
        }
        public override string ToString()
        {
            return "Haircut Name: " + haircutName + " Client: " + client.ToString() + " Barber: " + barber.ToString() + " Price: " + price + " Additional Services?: " + additionalServices;
        }
    }
}

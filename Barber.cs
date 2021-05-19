using System;

namespace Hairdresser_s
{
    [Serializable]
    public class Barber
    {
        public string name { get; set; }
        public string surname { get; set; }
        public Barber() { }
        public Barber(string Name, string Surname)
        {
            this.name = Name;
            this.surname = Surname;
        }
        public override string ToString()
        {
            return "Barber Name: " + name + " Surname: " + surname;
        }
    }
}

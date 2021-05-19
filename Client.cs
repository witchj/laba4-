using System;

namespace Hairdresser_s
{
    [Serializable]
    public class Client
    {
        public enum ClientValue { Man, women_Paint, Women_Haircut, Women_Laying, Kid }
        public ClientValue clientValue { get; set; }
        public Client() { }
        public Client(ClientValue v)
        {
            this.clientValue = v;
        }
        public override string ToString()
        {
            return "Client: " + clientValue.ToString();
        }
    }
}

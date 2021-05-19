using System;
using System.Collections.Generic;

namespace Hairdresser_s
{
    [Serializable]
    public class BarberShop
    {
        public int BarberShopNumber { get; set; }
        public DateTime Day { get; set; }
        public static double AdditionalPrice { get; set; }
        public List<HairStyle> madeHairstyles = new List<HairStyle>();
        public void AddHairStyle(HairStyle hairStyle)
        {
            madeHairstyles.Add(hairStyle);
        }
        public void DeleteHairStyle(HairStyle hairStyle)
        {
            madeHairstyles.Remove(hairStyle);
        }

        public override string ToString() //short information
        {
            return "BarberShop: " + BarberShopNumber + " Date: " + Day.Date.ToShortDateString() + " Made Hairstyles: " + madeHairstyles.Count;
        }
        public string AllInfoString()
        {
            string str = "";
            str += this.ToString();
            if (madeHairstyles != null)
            {
                foreach (var hairstyle in madeHairstyles)
                {
                    str += hairstyle.ToString() + Environment.NewLine;
                }
            }
            return str;

        }
        public BarberShop() { }

        public BarberShop(int number, DateTime Day, double additionalPrice)
        {
            BarberShopNumber = number;
            this.Day = Day;
            AdditionalPrice = additionalPrice;
        }
    }
}

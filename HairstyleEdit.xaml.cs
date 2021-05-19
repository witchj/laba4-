using System;
using System.Windows;
using System.Windows.Controls;

namespace Hairdresser_s
{
    /// <summary>
    /// Interaction logic for HairstyleEdit.xaml
    /// </summary>
    public partial class HairstyleEdit : Window
    {
        ListBox HairstyleList;
        ListBox BarberShopList;

        BarberShop SelectedBarberShop;

        public HairstyleEdit()
        {
            InitializeComponent();
            ClientComboBox.Items.Add(Client.ClientValue.Kid);
            ClientComboBox.Items.Add(Client.ClientValue.Man);
            ClientComboBox.Items.Add(Client.ClientValue.Women_Haircut);
            ClientComboBox.Items.Add(Client.ClientValue.Women_Laying);
            ClientComboBox.Items.Add(Client.ClientValue.women_Paint);
            HairstyleList = (ListBox) Application.Current.Windows[0].FindName("HairstylesList");
            BarberShopList = (ListBox) Application.Current.Windows[0].FindName("BarberShopList");
            SelectedBarberShop =
                (BarberShop) ((ListBox) Application.Current.Windows[0].FindName("BarberShopList")).SelectedItem;
        }

        public HairstyleEdit(ListBox hairstyleList, ListBox barberShopList)
        {
            InitializeComponent();
            ClientComboBox.Items.Add(Client.ClientValue.Kid);
            ClientComboBox.Items.Add(Client.ClientValue.Man);
            ClientComboBox.Items.Add(Client.ClientValue.Women_Haircut);
            ClientComboBox.Items.Add(Client.ClientValue.Women_Laying);
            ClientComboBox.Items.Add(Client.ClientValue.women_Paint);
            HairstyleList = hairstyleList;
            BarberShopList = barberShopList;
            SelectedBarberShop = (BarberShop) barberShopList.SelectedItem;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBarberShop != null)
            {
                if (Edit.IsChecked == false)
                {
                    Client Client = new Client((Client.ClientValue) ClientComboBox.SelectedItem);
                    Barber Barber = new Barber(Name.Text, Surname.Text);
                    HairStyle Hairstyle = new HairStyle(HairstyleName.Text, Client, Barber,
                        Convert.ToDouble(Price.Text), (bool) AdditionalServiceNeeded.IsChecked);
                    //Отрисовка в форме
                    SelectedBarberShop.AddHairStyle(Hairstyle);
                    HairstyleList.Items.Add(Hairstyle);
                    BarberShopList.Items.Refresh();
                }
                else
                {
                    Client Client = new Client((Client.ClientValue) ClientComboBox.SelectedItem);
                    Barber Barber = new Barber(Name.Text, Surname.Text);

                    HairStyle hairStyle = ((HairStyle) HairstyleList.Items[HairstyleList.SelectedIndex]);

                    hairStyle.haircutName = HairstyleName.Text;
                    hairStyle.client = Client;
                    hairStyle.barber = Barber;
                    hairStyle.price = Convert.ToDouble(Price.Text);
                    hairStyle.additionalServices = (bool) AdditionalServiceNeeded.IsChecked;
                    BarberShopList.Items.Refresh();
                    HairstyleList.Items.Refresh();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
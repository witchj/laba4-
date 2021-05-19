using System;
using System.Windows;
using System.Windows.Controls;

namespace Hairdresser_s
{
    /// <summary>
    /// Interaction logic for BarberEdit.xaml
    /// </summary>
    public partial class BarberEdit : Window
    {
        public BarberEdit()
        {
            InitializeComponent();
        }

        ListBox BarberShopList = (ListBox) Application.Current.Windows[0].FindName("BarberShopList");

        BarberShop SelectedBarberShop =
            (BarberShop) ((ListBox) Application.Current.Windows[0].FindName("BarberShopList")).SelectedItem;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Edit.IsChecked == false)
            {
                BarberShop BarberShop = new BarberShop(BarberShopList.Items.Count, Convert.ToDateTime(DatePicker.Text),
                    Convert.ToDouble(Additional_Price.Text));
                BarberShopList.Items.Add(BarberShop);
                BarberShopList.SelectedItem = BarberShop;
            }
            else
            {
                BarberShop BarberShop = SelectedBarberShop;
                BarberShop.Day = Convert.ToDateTime(DatePicker.Text);
                BarberShop.AdditionalPrice = Convert.ToDouble(Additional_Price.Text);
                BarberShopList.Items[BarberShop.BarberShopNumber] = BarberShop;
                BarberShopList.Items.Refresh();
            }
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
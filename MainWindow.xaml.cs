using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml.Serialization;
using static System.Console;

namespace Hairdresser_s
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Serialize
        {
            Binary,
            XML
        }

        private String fileNameBinary = Directory.GetCurrentDirectory() + "\\data.txt";
        private String fileNameXML = Directory.GetCurrentDirectory() + "\\data.xml";
        private String fileNameJson = Directory.GetCurrentDirectory() + "\\data.json";

        public MainWindow()
        {
            InitializeComponent();
            ComboBox1.Items.Add(Serialize.Binary);
            ComboBox1.Items.Add(Serialize.XML);
            ComboBox1.SelectedItem = ComboBox1.Items[0];
        }

        private void AddBarber_Click(object sender, RoutedEventArgs e)
        {
            BarberEdit window = new BarberEdit();
            window.ShowDialog();
        }

        private void DeleteBarber_Click(object sender, RoutedEventArgs e)
        {
            if (BarberShopList.SelectedItem != null)
                BarberShopList.Items.Remove(BarberShopList.SelectedItem);
        }

        private void AddHairstyle_Click_2(object sender, RoutedEventArgs e)
        {
            if (BarberShopList.SelectedItem != null)
            {
                HairstyleEdit window = new HairstyleEdit();
                window.ShowDialog();
            }
        }

        private void DeleteHairstyle_Click_3(object sender, RoutedEventArgs e)
        {
            if (HairstylesList.SelectedItem != null && BarberShopList.SelectedItem != null)
            {
                BarberShop bs = BarberShopList.SelectedItem as BarberShop;
                bs.DeleteHairStyle((HairStyle) HairstylesList.SelectedItem);
                BarberShopList.Items[BarberShopList.SelectedIndex] = bs;
                HairstylesList.Items.Remove(HairstylesList.SelectedItem);
                BarberShopList.Items.Refresh();
            }
        }

        private void EditBarber_Click(object sender, RoutedEventArgs e)
        {
            if (BarberShopList.SelectedItem != null)
            {
                BarberEdit window = new BarberEdit();
                BarberShop BarberShop = (BarberShop) BarberShopList.SelectedItem;

                window.DatePicker.SelectedDate = BarberShop.Day;
                window.Additional_Price.Text = BarberShop.AdditionalPrice.ToString();
                window.Edit.IsChecked = true;
                window.ShowDialog();
                window.Edit.IsChecked = false;
            }
        }

        private void Edit_Hairstyle_Click(object sender, RoutedEventArgs e)
        {
            if (BarberShopList.SelectedItem != null && HairstylesList.SelectedItem != null)
            {
                HairstyleEdit window = new HairstyleEdit();
                HairStyle hairStyle = (HairStyle) HairstylesList.SelectedItem;

                window.HairstyleName.Text = hairStyle.haircutName;
                window.ClientComboBox.SelectedItem = hairStyle.client.clientValue;
                window.Name.Text = hairStyle.barber.name;
                window.Surname.Text = hairStyle.barber.surname;
                window.Price.Text = hairStyle.price.ToString();
                window.AdditionalServiceNeeded.IsChecked = hairStyle.additionalServices;
                window.Edit.IsChecked = true;
                window.ShowDialog();
                window.Edit.IsChecked = false;
            }
        }

        private void BarberShopList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            HairstylesList.Items.Clear();
            if (BarberShopList.SelectedItem != null)
            {
                foreach (var hairstyle in ((BarberShop) BarberShopList.SelectedItem).madeHairstyles)
                {
                    HairstylesList.Items.Add(hairstyle);
                }
            }
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BarberShopList.Items.Clear(); // Очистка списку перукарень
                // В залежності від типу обраного файлу проводить десеріалізацію
                switch ((Serialize) ComboBox1.SelectedItem)
                {
                    case Serialize.Binary:
                        // Перевіряєм на те, чи створений файл, якщо ні то створюємо
                        if (!(File.Exists(fileNameBinary)))
                        {
                            File.Create(fileNameBinary);
                        }

                        FileStream fileStream2 = new FileStream(fileNameBinary, FileMode.Open);
                        BinaryFormatter bf2 = new BinaryFormatter();

                        List<BarberShop> BarberShops = new List<BarberShop>();
                        try
                        {
                            BarberShops = bf2.Deserialize(fileStream2) as List<BarberShop>;
                        }
                        catch (SerializationException ex)
                        {
                            WriteLine("Failed to open serializable file... ");
                            throw;
                        }
                        finally
                        {
                            fileStream2.Close();
                        }

                        // Заповнюємо данними форму
                        foreach (var eachBarberShop in BarberShops)
                        {
                            BarberShopList.Items.Add((BarberShop) eachBarberShop);
                        }

                        break;
                    case Serialize.XML:
                        // Перевіряєм на те, чи створений файл, якщо ні то створюємо
                        if (!(File.Exists(fileNameXML)))
                        {
                            File.Create(fileNameXML);
                        }

                        // Відкриваємо потік на зчитування
                        FileStream fileStream = new FileStream(fileNameXML, FileMode.Open);
                        // Налаштовуємо XML механіз серіалізації, передаючи типи,
                        // розмітка типу <Назва типу(назва поля в серіалузємому класі)>
                        XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<BarberShop>),
                            new Type[] {typeof(Barber), typeof(Client), typeof(HairStyle)});
                        List<BarberShop> BarberShopsXml = new List<BarberShop>();
                        try
                        {
                            BarberShopsXml = xmlFormatter.Deserialize(fileStream) as List<BarberShop>;
                        }
                        catch (SerializationException ex)
                        {
                            WriteLine("Failed to open serializable file... ");
                            throw;
                        }
                        finally
                        {
                            fileStream.Close();
                        }

                        // Заповнюємо данними форму
                        foreach (var barberShop in BarberShopsXml)
                        {
                            BarberShopList.Items.Add(barberShop);
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + fileNameBinary + fileNameXML);
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        private void saveFile()
        {
            if (MessageBox.Show("Do you want to save changes?", "Serialise",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    List<BarberShop> barberShops = new List<BarberShop>();
                    foreach (var barberShop in BarberShopList.Items)
                    {
                        barberShops.Add((BarberShop) barberShop);
                    }

                    switch ((Serialize) ComboBox1.SelectedItem)
                    {
                        case Serialize.Binary:
                            BinarySerialization(barberShops);

                            break;
                        case Serialize.XML:
                            XMLSerialization(barberShops);

                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + fileNameBinary + fileNameXML);
                }
            }
        }

        private void XMLSerialization(List<BarberShop> barberShops)
        {
            CheckPresentFile();

            FileStream fileStreamXml = new FileStream(fileNameXML, FileMode.Create);
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<BarberShop>),
                new Type[] {typeof(Barber), typeof(Client), typeof(HairStyle)});
            try
            {
                xmlFormatter.Serialize(fileStreamXml, barberShops);
            }
            catch (SerializationException ex)
            {
                WriteLine("Failed to serialize file...");
                throw;
            }
            finally
            {
                fileStreamXml.Close();
            }
        }

        private void CheckPresentFile()
        {
            if (!File.Exists(fileNameBinary))
            {
                File.Create(fileNameBinary);
            }
        }

        private void BinarySerialization(List<BarberShop> barberShops)
        {
            CheckPresentFile();

            FileStream fileStreamBinary = new FileStream(fileNameBinary, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fileStreamBinary, barberShops);
            }
            catch (SerializationException ex)
            {
                WriteLine("Failed to serialize file...");
                throw;
            }
            finally
            {
                fileStreamBinary.Close();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //TODO:Add command to window
            saveFile();
        }
    }
}
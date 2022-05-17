using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarSales.Model;

namespace CarSales.View.MainView.Catalog
{
    public partial class CatalogUserControl : UserControl
    {
        List<Advertisements> advertisements;
        List<Cars> cars;

        public CatalogUserControl()
        {
            advertisements = new List<Advertisements>();
            cars = new List<Cars>();

            InitializeComponent();
            using(var context = new CarSalesEntities())
            {
                advertisements = context.Advertisements.ToList();
                cars = context.Cars.ToList();
                // Заполнение комбобоксов для фильтрации

                ModelsComboBox.ItemsSource = context.Models.ToList();
                MarksComboBox.ItemsSource = context.Marks.ToList();
                ColorsComboBox.ItemsSource = context.Colors.ToList();
                TransmissionsComboBox.ItemsSource = context.Transmissions.ToList();
                HandlebarsComboBox.ItemsSource = context.Handlebars.ToList();
                WheeldrivesComboBox.ItemsSource = context.Wheeldrives.ToList();
                BodyworksComboBox.ItemsSource = context.Bodyworks.ToList();
            }
            CatalogListBox.ItemsSource = advertisements;
        }

        private void MarksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = MarksComboBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                Marks mark = (Marks)MarksComboBox.SelectedItem;
                using (var context = new CarSalesEntities())
                {
                    ModelsComboBox.ItemsSource = context.Models.Where(m => m.Mark == mark.Id).ToList();
                }
            }
        }

        private void ModelsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = MarksComboBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                Marks mark = (Marks)MarksComboBox.SelectedItem;
                cars = cars.Where(c => c.MarkModel == mark.Id).ToList();
            }
        }

        private void ColorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TransmissionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void HandlebarsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WheeldrivesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BodyworksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

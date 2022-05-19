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
        private List<int> IndexesCars { get; set; }
        private int IndexCatalog = 0;

        public CatalogUserControl()
        {
            advertisements = new List<Advertisements>();
            cars = new List<Cars>();
            IndexesCars = new List<int>();
            InitializeComponent();

            updateItems();
        }

        // Отображение товаров
        private void OneProduct_Click(object sender, RoutedEventArgs e)
        {
            DataTemplate template = (DataTemplate)this.Resources["TemplateListCatalog"];
            CatalogListBox.ItemTemplate = template;

            ButtonHadlerOneProductDisplay.Visibility = Visibility.Visible;
            ButtonHadlerFourProductDisplay.Visibility = Visibility.Hidden;

            updateItems();
        }
        private void FourProduct_Click(object sender, RoutedEventArgs e)
        {

            ButtonHadlerOneProductDisplay.Visibility = Visibility.Hidden;
            ButtonHadlerFourProductDisplay.Visibility = Visibility.Visible;
            updateItems();
        }
        private void ListProduct_Click(object sender, RoutedEventArgs e)
        {
            DataTemplate template = (DataTemplate)this.Resources["TemplateListCatalog"];
            CatalogListBox.ItemTemplate = template;
            updateItems();
        }


        private void LoadIdFromSwitchProduct()
        {
            IndexesCars.Clear();
            IndexCatalog = 0;

            foreach(var itemId in advertisements)
            {
                IndexesCars.Add(itemId.Id);
            }

            if (OneProduct.IsChecked == true)
            {
                var filter = advertisements.Where(a => a.Id == IndexesCars[IndexCatalog]);
                CatalogListBox.ItemsSource = filter;
            }
            else if (FourProduct.IsChecked == true)
            {

                if (IndexesCars.Count() < 4)
                {
                    var filteredFourProduct = advertisements.Where(u => u.Id >= IndexesCars[IndexCatalog] && u.Id <= IndexesCars[IndexesCars.Count() - 1]);
                    CatalogListBox.ItemsSource = filteredFourProduct;
                }
                else
                {
                    int numberForCorrectDisplayDuringTransitions = 3;

                    var filteredFourProduct = advertisements.Where(u => u.Id >= IndexesCars[IndexCatalog] && u.Id <= IndexesCars[IndexCatalog + numberForCorrectDisplayDuringTransitions]);
                    CatalogListBox.ItemsSource = filteredFourProduct;
                }
            }
            else
            {
                CatalogListBox.ItemsSource = advertisements;
            }
        }


        // Переключение продуктов по 1
        private void ButtonPreviousOneProduct_Click(object sender, RoutedEventArgs e)
        {
            if (IndexCatalog < 1)
                IndexCatalog = 0;
            else
                IndexCatalog--;

            //var carsItems = ProductCatalog.ItemsSource.Cast<Cars>();
            var filtered = advertisements.Where(c => c.Id == IndexesCars[IndexCatalog]);
            CatalogListBox.ItemsSource = filtered;
        }
        private void ButtonNextOneProduct_Click(object sender, RoutedEventArgs e)
        {
            IndexCatalog++;

            if (IndexCatalog < IndexesCars.Count())
            {
                var filtered = advertisements.Where(c => c.Id == IndexesCars[IndexCatalog]);
                CatalogListBox.ItemsSource = filtered;
            }
            else
                IndexCatalog--;
        }


        // Переключение продуктов по 4
        private void ButtonPreviousFourProduct_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonNextFourProduct_Click(object sender, RoutedEventArgs e)
        {

        }


        // Выбор из комбобоксов
        private void MarksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = MarksComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Marks mark = (Marks)MarksComboBox.SelectedItem;
                using (var context = new CarSalesEntities())
                {
                    ModelsComboBox.ItemsSource = context.Models.Where(m => m.Mark == mark.Id).ToList();
                }

                advertisements = advertisements.Where(a => a.Cars.Models.Mark == mark.Id).ToList();

                LoadIdFromSwitchProduct();
            }
        }
        private void ModelsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ModelsComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Models model = (Models)ModelsComboBox.SelectedItem;
                advertisements = advertisements.Where(a => a.Cars.MarkModel == model.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
            }
        }
        private void ColorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ColorsComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Model.Colors color = (Model.Colors)ColorsComboBox.SelectedItem;
                advertisements = advertisements.Where(a => a.Cars.Color == color.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
            }
        }
        private void TransmissionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = TransmissionsComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Transmissions transmission = (Transmissions)TransmissionsComboBox.SelectedItem;
                advertisements = advertisements.Where(a => a.Cars.Transmission == transmission.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
            }
        }
        private void HandlebarsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = HandlebarsComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Handlebars handlebar = (Handlebars)HandlebarsComboBox.SelectedItem;
                advertisements = advertisements.Where(a => a.Cars.Handlebar == handlebar.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
            }
        }
        private void WheeldrivesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = WheeldrivesComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Wheeldrives wheeldrive = (Wheeldrives)WheeldrivesComboBox.SelectedItem;
                advertisements = advertisements.Where(a => a.Cars.Wheeldrive == wheeldrive.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
            }
        }
        private void BodyworksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = BodyworksComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Bodyworks bodywork = (Bodyworks)BodyworksComboBox.SelectedItem;
                MessageBox.Show(bodywork.NameBodywork);

                advertisements = advertisements.Where(a => a.Cars.Bodywork == bodywork.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
            }
        }


        // Выбор Продукта
        private void ButtonChoice_Click(object sender, RoutedEventArgs e)
        {
            Advertisements curItem = (Advertisements)((ListBoxItem)CatalogListBox.ContainerFromElement((Button)sender)).Content;
            advertisements = advertisements.Where(a => a.Id != curItem.Id).ToList();

            CatalogListBox.ItemsSource = advertisements;

        }

        // Отчистка поиска
        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            MarksComboBox.SelectedIndex = -1;
            ModelsComboBox.SelectedIndex = -1;
            ColorsComboBox.SelectedIndex = -1;
            TransmissionsComboBox.SelectedIndex = -1;
            HandlebarsComboBox.SelectedIndex = -1;
            WheeldrivesComboBox.SelectedIndex = -1;
            BodyworksComboBox.SelectedIndex = -1;
            updateItems();
        }

        private void updateItems()
        {
            using (var context = new CarSalesEntities())
            {
                advertisements = context.Advertisements.Where(a => a.Statuses.NameStatus == "Активен").ToList();
                cars = context.Cars.ToList();
                // Заполнение комбобоксов для фильтрации

                ModelsComboBox.ItemsSource = context.Models.ToList();
                MarksComboBox.ItemsSource = context.Marks.ToList();
                ColorsComboBox.ItemsSource = context.Colors.ToList();
                TransmissionsComboBox.ItemsSource = context.Transmissions.ToList();
                HandlebarsComboBox.ItemsSource = context.Handlebars.ToList();
                WheeldrivesComboBox.ItemsSource = context.Wheeldrives.ToList();
                BodyworksComboBox.ItemsSource = context.Bodyworks.ToList();

                List<Photos> photos = context.Photos.ToList();
                foreach (var item in cars)
                {
                    if (photos.Where(p => p.Car == item.Id).ToList() != null)
                        item.photoCar = photos.Where(p => p.Car == item.Id).ToList();
                }
            }

            LoadIdFromSwitchProduct();

        }
    }
}

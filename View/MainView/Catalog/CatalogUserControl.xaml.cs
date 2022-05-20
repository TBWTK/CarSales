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
        List<Advertisements> HandlerBasketAdvertisements;
        List<Cars> cars;
        private List<int> IndexesCars { get; set; }
        private int IndexCatalog = 0;
        private decimal additionsSum;

        int idUser;
        public CatalogUserControl(int ID)
        {
            idUser = ID;

            advertisements = new List<Advertisements>();
            HandlerBasketAdvertisements = new List<Advertisements>();
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

            DataTemplate template = (DataTemplate)this.Resources["listTemplateSmall"];
            CatalogListBox.ItemTemplate = template;

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
            int numberForCorrectDisplayDuringTransitions = 5;
            int numberToDisplayWhenEmptyClicks = 3;
            int numberForAccessingProductsByIndex = 3;


            if (IndexCatalog - numberForAccessingProductsByIndex >= 0)
            {
                IndexCatalog -= numberForAccessingProductsByIndex;
            }

            if (IndexCatalog - numberForAccessingProductsByIndex >= 0)
            {
                var filtered = advertisements.Where(u => u.Id > IndexesCars[IndexCatalog] && u.Id < IndexesCars[IndexCatalog + numberForCorrectDisplayDuringTransitions]);
                CatalogListBox.ItemsSource = filtered;
            }
            else
            {
                if (IndexCatalog + numberToDisplayWhenEmptyClicks < IndexesCars.Count())
                {
                    var filtered = advertisements.Where(u => u.Id >= IndexesCars[IndexCatalog] && u.Id <= IndexesCars[IndexCatalog + numberToDisplayWhenEmptyClicks]);
                    CatalogListBox.ItemsSource = filtered;
                }
                else
                {
                    var filtered = advertisements.Where(u => u.Id >= IndexesCars[IndexCatalog] && u.Id <= IndexesCars[IndexesCars.Count() - 1]);
                    CatalogListBox.ItemsSource = filtered;
                }
            }
        }
        private void ButtonNextFourProduct_Click(object sender, RoutedEventArgs e)
        {
            int numberForCorrectDisplayDuringTransitions = 5;
            int numberToDisplayFinalProducts = 1;
            int numberForAccessingProductsByIndex = 3;

            if (IndexCatalog + numberForAccessingProductsByIndex < IndexesCars.Count() - 1)
            {
                IndexCatalog += numberForAccessingProductsByIndex;
                if (IndexCatalog + numberForAccessingProductsByIndex > IndexesCars.Count() - 1)
                {
                    IndexCatalog -= numberForAccessingProductsByIndex;
                }
            }

            if (IndexCatalog + numberForCorrectDisplayDuringTransitions < IndexesCars.Count())
            {
                var filtered = advertisements.Where(u => u.Id > IndexesCars[IndexCatalog] && u.Id < IndexesCars[IndexCatalog + numberForCorrectDisplayDuringTransitions]);
                CatalogListBox.ItemsSource = filtered;
            }
            else
            {
                if (IndexCatalog + numberToDisplayFinalProducts < IndexesCars.Count() - 1)
                {
                    var filtered = advertisements.Where(u => u.Id > IndexesCars[IndexCatalog + numberToDisplayFinalProducts] && u.Id <= IndexesCars[IndexesCars.Count() - 1]);
                    CatalogListBox.ItemsSource = filtered;
                }
                else
                {
                    var filtered = advertisements.Where(u => u.Id >= IndexesCars[IndexCatalog] && u.Id <= IndexesCars[IndexesCars.Count() - 1]);
                    CatalogListBox.ItemsSource = filtered;
                }

            }
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
                LoadIdFromSwitchProduct();
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
                LoadIdFromSwitchProduct();
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
                LoadIdFromSwitchProduct();
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
                LoadIdFromSwitchProduct();
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
                LoadIdFromSwitchProduct();
            }
        }
        private void BodyworksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = BodyworksComboBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Bodyworks bodywork = (Bodyworks)BodyworksComboBox.SelectedItem;
                advertisements = advertisements.Where(a => a.Cars.Bodywork == bodywork.Id).ToList();
                CatalogListBox.ItemsSource = advertisements;
                LoadIdFromSwitchProduct();
            }
        }


        // Поиск по вводимым данным
        private void YearStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filter = advertisements.Where(a => a.Cars.Year >= Convert.ToInt32(YearStart.Text)).ToList();
                CatalogListBox.ItemsSource = filter;
                LoadIdFromSwitchProduct();
            }
            catch
            {
                MessageBox.Show("Введите число");
            }
        }
        private void YearEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filter = advertisements.Where(a => a.Cars.Year <= Convert.ToInt32(YearStart.Text)).ToList();
                CatalogListBox.ItemsSource = filter;
                LoadIdFromSwitchProduct();
            }
            catch
            {
                MessageBox.Show("Введите число");
            }
        }
        private void PriceStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filter = advertisements.Where(a => a.Price >= Convert.ToInt32(PriceStart.Text)).ToList();
                CatalogListBox.ItemsSource = filter;
                LoadIdFromSwitchProduct();
            }
            catch
            {
                MessageBox.Show("Введите число");
            }
        }
        private void PriceEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filter = advertisements.Where(a => a.Price <= Convert.ToInt32(PriceStart.Text)).ToList();
                CatalogListBox.ItemsSource = filter;
                LoadIdFromSwitchProduct();
            }
            catch
            {
                MessageBox.Show("Введите число");
            }
        }
        private void HorsePowerStart_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var filter = advertisements.Where(a => a.Cars.Engines.Horsepower >= Convert.ToInt32(HorsePowerStart.Text)).ToList();
                CatalogListBox.ItemsSource = filter;
                LoadIdFromSwitchProduct();
            }
            catch
            {
                MessageBox.Show("Введите число");

            }
        }
        private void HorsePowerEnd_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var filter = advertisements.Where(a => a.Cars.Engines.Horsepower <= Convert.ToInt32(HorsePowerStart.Text)).ToList();
                CatalogListBox.ItemsSource = filter;
                LoadIdFromSwitchProduct();
            }
            catch
            {
                MessageBox.Show("Введите число");

            }
        }


        // Выбор Продукта
        private void ButtonChoice_Click(object sender, RoutedEventArgs e)
        {
            Advertisements curItem = (Advertisements)((ListBoxItem)CatalogListBox.ContainerFromElement((Button)sender)).Content;
            advertisements = advertisements.Where(a => a.Id != curItem.Id).ToList();
            
            HandlerBasketAdvertisements.Add(curItem);
            additionsSum += (decimal)(curItem.Price);
            SumOrder.Text = $"{additionsSum:0.00}";

            int handlerAdd = Convert.ToInt32(QuentytiProduct.Text);
            handlerAdd++;
            QuentytiProduct.Text = $"{handlerAdd}";

            LoadIdFromSwitchProduct();
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

                //
                EngineCapacityComboBox.ItemsSource = context.EngineCapacity.ToList();
                EngineTypesComboBox.ItemsSource = context.EngineCapacity.ToList();

                List<Photos> photos = context.Photos.ToList();
                foreach (var item in cars)
                {
                    if (photos.Where(p => p.Car == item.Id).ToList() != null)
                        item.photoCar = photos.Where(p => p.Car == item.Id).ToList();
                }
            }

            LoadIdFromSwitchProduct();

        }


        // Переходы между корзиной и каталогом
        private void OpenBasketsPage_Click(object sender, RoutedEventArgs e)
        {
            BasketGird.Visibility = Visibility.Visible;
            CatalogGrid.Visibility = Visibility.Hidden;
            BasketCatalog.ItemsSource = HandlerBasketAdvertisements;
        }
        private void BackToCatalog_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BasketGird.Visibility = Visibility.Hidden;
            CatalogGrid.Visibility = Visibility.Visible;
            if(HandlerBasketAdvertisements.Count == 0)
            {
                updateItems();
            }
        }


        private void MakeOrder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (HandlerBasketAdvertisements.Count != 0)
            {
                try
                {
                    using (var context = new CarSalesEntities())
                    {
                        var datetime = DateTime.Now;
                        var onlydate = datetime.Date;
                        Orders newOrder = new Orders()
                        {
                            User = idUser,
                            Price = additionsSum,
                            Date = onlydate
                        };
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        var getOrder = context.Orders.SingleOrDefault(o => o.User == idUser && o.Price == additionsSum && o.Date == onlydate);

                        foreach (var item in HandlerBasketAdvertisements)
                        {
                            Baskets productToBasket = new Baskets
                            {
                                Advertisement = item.Id,
                                Order = getOrder.Id

                            };
                            context.Baskets.Add(productToBasket);
                        }
                        context.SaveChanges();
                    }
                    MessageBox.Show("Заказ оформлен");

                    additionsSum = 0;
                    QuentytiProduct.Text = $"{additionsSum:0.00}";
                    SumOrder.Text = $"{additionsSum:0.00}";

                    HandlerBasketAdvertisements.Clear();
                    BasketCatalog.ItemsSource = HandlerBasketAdvertisements.ToList();
                }
                catch
                {
                    MessageBox.Show("Произшла непредвиденная ошибка, помолитесь");
                }

            }
            else
                MessageBox.Show("Товаров нет в корзине");
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Advertisements curItem = (Advertisements)((ListBoxItem)BasketCatalog.ContainerFromElement((Button)sender)).Content;
            additionsSum -= curItem.Price;
            SumOrder.Text = $"{additionsSum:0.00}";

            int handlerSub = Convert.ToInt32(QuentytiProduct.Text);
            handlerSub--;
            QuentytiProduct.Text = $"{handlerSub}";

            var newProducts = HandlerBasketAdvertisements.Where(u => u.Id != curItem.Id);
            HandlerBasketAdvertisements.Remove(curItem);
            BasketCatalog.ItemsSource = newProducts;
        }
    }
}

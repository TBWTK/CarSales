using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class AdvertisementsUserControl : UserControl
    {
        int idUser;

        Advertisements advertisements;
        Cars car;

        List<Marks> marks;
        List<Models> models;


        public AdvertisementsUserControl(int ID)
        {
            idUser = ID;
            InitializeComponent();

            advertisements = new Advertisements();
            car = new Cars();

            marks = new List<Marks>();
            models = new List<Models>();


            using (var context = new CarSalesEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;

                marks = context.Marks.ToList();
                models = context.Models.ToList();
            }

            MarksBox.ItemsSource = marks;

        }

        private void MarksBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = MarksBox.SelectedIndex;
            if (selectedIndex > -1)
            {
                Marks selectedItem = (Marks)MarksBox.SelectedItem;
                using (var context = new CarSalesEntities())
                {
                    models = context.Models.Where(model => model.Mark == selectedItem.Id).ToList();
                }
                ModelsBox.ItemsSource = models;
            }
        }

        private void BaseCarForward_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int checkMark = MarksBox.SelectedIndex;
            int checkModel = ModelsBox.SelectedIndex;

            if (checkMark < 0 && checkModel < 0)
                MessageBox.Show("Выберите марку и модель");
            else if (VinTextBox.Text == "" && StateNumberTextBox.Text == "" && MileageTextBox.Text == "" && NumOfOwnersTextBox.Text == "")
                MessageBox.Show("Введите все данные!");
            else
            {
                Models MarkModel = (Models)ModelsBox.SelectedItem;

                car.MarkModel = MarkModel.Id;
                car.Vin = VinTextBox.Text;
                car.StateNumber = StateNumberTextBox.Text;
                car.Mileage = MileageTextBox.Text;
                car.NumOfOwners = Convert.ToInt32(NumOfOwnersTextBox.Text);
                BaseCar.Visibility = Visibility.Hidden;
                ListCar.Visibility = Visibility.Visible;


                // Заполнение данными следующего представления

                List<Model.Colors> colors = new List<Model.Colors>();
                List<Transmissions> transmissions = new List<Transmissions>();
                List<Handlebars> handlebars = new List<Handlebars>();
                List<Wheeldrives> wheeldrives = new List<Wheeldrives>();
                List<Bodyworks> bodyworks = new List<Bodyworks>();


                using (var context = new CarSalesEntities())
                {
                    colors = context.Colors.ToList();
                    transmissions = context.Transmissions.ToList();
                    handlebars = context.Handlebars.ToList();
                    wheeldrives = context.Wheeldrives.ToList();
                    bodyworks = context.Bodyworks.ToList();
                }
                ColorsBox.ItemsSource = colors;
                TransmissionsBox.ItemsSource = transmissions;
                HandlerbarsBox.ItemsSource = handlebars;
                WheeldrivesBox.ItemsSource = wheeldrives;
                BodyworksBox.ItemsSource = bodyworks;
            }
        }

        private void ListCarForward_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int checkColor = ColorsBox.SelectedIndex;
            int checkTransmission = TransmissionsBox.SelectedIndex;
            int checkHandlebar = HandlerbarsBox.SelectedIndex;
            int checkWheeldrive = WheeldrivesBox.SelectedIndex;
            int checkBodywork = BodyworksBox.SelectedIndex;

            if (checkColor < 0 && checkTransmission < 0 && checkHandlebar < 0 && checkWheeldrive < 0 && checkBodywork < 0 && YearCar.Text == "")
                MessageBox.Show("Введите все данные!");
            else
            {
                Model.Colors colorCar = (Model.Colors)ColorsBox.SelectedItem;
                Transmissions transmissionCar = (Transmissions)TransmissionsBox.SelectedItem;
                Handlebars handlebarCar = (Handlebars)HandlerbarsBox.SelectedItem;
                Wheeldrives wheeldriveCar = (Wheeldrives)WheeldrivesBox.SelectedItem;
                Bodyworks bodyworkCar = (Bodyworks)BodyworksBox.SelectedItem;

                car.Color = colorCar.Id;
                car.Transmission = transmissionCar.Id;
                car.Handlebar = handlebarCar.Id;
                car.Wheeldrive = wheeldriveCar.Id;
                car.Bodywork = bodyworkCar.Id;
                car.Year = Convert.ToInt32(YearCar.Text);

                // Заполнение данными следующего представления
                List<EngineTypes> engineTypes = new List<EngineTypes>();
                List<Fuels> fuels = new List<Fuels>();
                List<Cylinders> cylinders = new List<Cylinders>();
                List<EngineCapacity> engineCapacity = new List<EngineCapacity>();


                using (var context = new CarSalesEntities())
                {
                    engineTypes = context.EngineTypes.ToList();
                    fuels = context.Fuels.ToList();
                    cylinders = context.Cylinders.ToList();
                    engineCapacity = context.EngineCapacity.ToList();
                }

                foreach(var tyE in engineTypes)
                {
                    foreach (var fuel in fuels)
                        if (tyE.Fuel == fuel.Id)
                            tyE.NameEngineType += " " + fuel.NameFuel;
                }

                EngineTypesBox.ItemsSource = engineTypes;
                CylindersBox.ItemsSource = cylinders;
                EngineCapacityBox.ItemsSource = engineCapacity;

                ListCar.Visibility = Visibility.Hidden;
                EngineCar.Visibility = Visibility.Visible;
            }
        }

        private void EngineCarForward_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int checkEngineTypes = EngineTypesBox.SelectedIndex;
            int checkCylinders = CylindersBox.SelectedIndex;
            int checkEngineCapacity = EngineCapacityBox.SelectedIndex;

            if (checkEngineTypes < 0 && checkCylinders < 0 && checkEngineCapacity < 0 && HorsepowerTextBox.Text == "" && ConsumptionFuelTextBox.Text == "")
                MessageBox.Show("Введите все данные!");
            else
            {
                EngineTypes type = (EngineTypes)EngineTypesBox.SelectedItem;
                Cylinders cylinder = (Cylinders)CylindersBox.SelectedItem;
                EngineCapacity capacity = (EngineCapacity)EngineCapacityBox.SelectedItem;

                try
                {
                    using (var context = new CarSalesEntities())
                    {
                        var engine = new Engines()
                        {
                            EngineType = type.Id,
                            Cylinder = cylinder.Id,
                            Capacity = capacity.Id,
                            Horsepower = Convert.ToInt32(HorsepowerTextBox.Text),
                            ConsumptionFuel = Convert.ToDouble(ConsumptionFuelTextBox.Text)
                        };

                        context.Engines.Add(engine);
                        context.SaveChanges();

                        var getIdEngine = context.Engines.SingleOrDefault(en => en.EngineType == engine.EngineType && en.Cylinder == engine.Cylinder && en.Capacity == engine.Capacity && en.Horsepower == engine.Horsepower && en.ConsumptionFuel == engine.ConsumptionFuel);
                        car.Engine = getIdEngine.Id;

                        context.Cars.Add(car);
                        context.SaveChanges();
                    }
                    EngineCar.Visibility = Visibility.Hidden;
                    PhotoCar.Visibility = Visibility.Visible;
                }
                catch
                {
                    MessageBox.Show("Вводите расход через запятую, а не через точку");
                }

            }
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        private void DonwloadPhoto_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            Microsoft.Win32.OpenFileDialog ofdPicture = new Microsoft.Win32.OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.JPG;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                myBitmapImage.UriSource = new Uri(ofdPicture.FileName);
                myBitmapImage.EndInit();
                CarPhoto.Source = myBitmapImage;
                using (var context = new CarSalesEntities())
                {
                    var car = context.Cars.SingleOrDefault(x => x.StateNumber == StateNumberTextBox.Text && x.Vin == VinTextBox.Text);
                    if (car != null)
                    {
                        var newPhotCar = new Photos
                        {
                            Car = car.Id,
                            Photo = getJPGFromImageControl(CarPhoto.Source as BitmapImage)
                        };
                        context.Photos.Add(newPhotCar);
                        context.SaveChanges();
                        MessageBox.Show("Фотография загружена");
                    }
                }
            }
        }

        private void PhotoCarForward_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PhotoCar.Visibility = Visibility.Hidden;
            AdvertisementCreate.Visibility = Visibility.Visible;
        }

        private void AdvertisementCreateForward_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                advertisements.User = idUser;
                advertisements.Price = Convert.ToDecimal(PriceTextBox.Text);
                advertisements.Comment = CommentTextBox.Text;
                advertisements.Date = DateTime.Now;

                using (var context = new CarSalesEntities())
                {
                    var status = context.Statuses.SingleOrDefault(s => s.NameStatus == "Активен");
                    advertisements.Status = status.Id;
                    var car = context.Cars.SingleOrDefault(x => x.StateNumber == StateNumberTextBox.Text && x.Vin == VinTextBox.Text);
                    advertisements.Car = car.Id;

                    context.Advertisements.Add(advertisements);
                    context.SaveChanges();
                }


                MarksBox.SelectedIndex = -1;
                ModelsBox.SelectedIndex = -1;
                VinTextBox.Text = "";
                StateNumberTextBox.Text = "";
                MileageTextBox.Text = "";
                NumOfOwnersTextBox.Text = "";
                ColorsBox.SelectedIndex = -1;
                TransmissionsBox.SelectedIndex = -1;
                HandlerbarsBox.SelectedIndex = -1;
                WheeldrivesBox.SelectedIndex = -1;
                BodyworksBox.SelectedIndex = -1;
                YearCar.Text = "";
                EngineTypesBox.SelectedIndex = -1;
                CylindersBox.SelectedIndex = -1;
                EngineCapacityBox.SelectedIndex = -1;
                HorsepowerTextBox.Text = "";
                ConsumptionFuelTextBox.Text = "";
                PriceTextBox.Text = "";
                CommentTextBox.Text = "";

                car = new Cars();
                advertisements = new Advertisements();


                BaseCar.Visibility = Visibility.Visible;
                AdvertisementCreate.Visibility = Visibility.Hidden;
            }
            catch
            {
                MessageBox.Show("Вводите цену через запятую, а не через точку");
            }

        }
    }
}

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
using System.Windows.Forms.DataVisualization.Charting;
using CarSales.Model;

namespace CarSales.View.MainView.Statistics
{
    /// <summary>
    /// Логика взаимодействия для StatisticsUserControl.xaml
    /// </summary>
    public partial class StatisticsUserControl : UserControl
    {
        List<Orders> orders { get; set; }
        List<Baskets> baskets { get; set; }
        List<Cars> cars { get; set; }
        List<Users> users { get; set; }

        public StatisticsUserControl()
        {
            InitializeComponent();
            orders = new List<Orders>();
            baskets = new List<Baskets>();
            cars = new List<Cars>();
            users = new List<Users>();

            using (var context = new CarSalesEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                orders = context.Orders.ToList();
                baskets = context.Baskets.ToList();
                cars = context.Cars.ToList();
                users = context.Users.ToList();
                ChoiceMark.ItemsSource = context.Marks.ToList();
            }

            ChartPayments.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true
            };

            ChartPayments.Series.Add(currentSeries);

            ComboChart.ItemsSource = Enum.GetValues(typeof(SeriesChartType));

            DataGridUsers.ItemsSource = users;
            //ChoiceMark.ItemsSource = cars;
            StartDate.SelectedDate = new DateTime(2022, 04, 28);
            EndDate.SelectedDate = DateTime.Now;
        }



        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGridUsers.SelectedIndex = -1;
            ChoiceMark.SelectedIndex = -1;
            ChoiceModel.SelectedIndex = -1;
            StartDate.SelectedDate = new DateTime(2022, 04, 28);
            EndDate.SelectedDate = DateTime.Now;
            UpdateChart();
        }
        private void ChoiceMark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ChoiceMark.SelectedIndex;
            if (selectedIndex > -1)
            {
                Marks mark = (Marks)ChoiceMark.SelectedItem;
                using (var context = new CarSalesEntities())
                {
                    ChoiceModel.ItemsSource = context.Models.Where(m => m.Mark == mark.Id).ToList();
                }
            }
            UpdateChart();
        }

        private void ChoiceModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }
        private void ComboChart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }
        private void DataGridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }
        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }
        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }
    
        private void UpdateChart()
        {
            Series currentSeries = ChartPayments.Series.FirstOrDefault();
            if (ComboChart.SelectedItem is SeriesChartType currentType)
            {
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();
            }


            if (StartDate.SelectedDate != null && EndDate.SelectedDate != null)
            {
                var filterDate = orders.Where(o => o.Date >= StartDate.SelectedDate.Value && o.Date <= EndDate.SelectedDate.Value);
                foreach (var order in filterDate)
                {
                    currentSeries.Points.AddXY($"{order.Date.Value.Day}.{order.Date.Value.Month}.{order.Date.Value.Year}\n{order.Users.Surname} {order.Users.Name}", order.Price);
                }
            }
            else
            {
                foreach (var order in orders)
                {
                    currentSeries.Points.AddXY($"{order.Date.Value.Day}.{order.Date.Value.Month}.{order.Date.Value.Year}\n{order.Users.Surname} {order.Users.Name}", order.Price);
                }
            }
        }
    }
}

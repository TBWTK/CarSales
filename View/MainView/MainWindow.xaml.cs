using System.Windows;
using System.Windows.Threading;
using System;
using System.Linq;
using System.Windows.Input;

namespace CarSales.View.MainView
{
    public partial class MainWindow : Window
    {
        int idUser;
        int tick = 0;
        Point now = new Point(0, 0);
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow(int ID)
        {
            idUser = ID;
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }


        // Смена пейджей 
        private void AccountPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CatalogPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatPage_Click(object sender, RoutedEventArgs e)
        {

        }

        // Бездекйствие пользователя
        private void Timer_Tick(object sender, EventArgs e)
        {
            ++tick;
            if (tick == 20)
            {
                timer.Stop();
                AuthenticationView.AuthenticationWindow authentication = new AuthenticationView.AuthenticationWindow();
                this.Close();
                authentication.Show();
            }
        }
        private void Window_KeyDownAndUp(object sender, KeyEventArgs e)
        {
            tick = 0;
        }
        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            tick = 0;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point pp = e.GetPosition(this);
            if (pp != now)
            {
                tick = 0;
            }
            now = pp;
        }
    }
}

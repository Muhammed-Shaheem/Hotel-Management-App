using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
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
using System.Windows.Shapes;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        public CheckInWindow(IDatabaseData db)
        {
            InitializeComponent();
            this.db = db;

        }

        private FullBookingModel data;
        private readonly IDatabaseData db;

        public void PopuateCheckInInfo(FullBookingModel data)
        {
            this.data = data;
            firstNameText.Text = data.FirstName;
            lastNameText.Text = data.LastName;
            titleText.Text = data.Title;
            RoomNumberText.Text = data.RoomNumber;
            totalCostText.Text = String.Format("{0:c}", data.TotalCost);

        }

        private void confirmCheckIn_Click(object sender, RoutedEventArgs e)
        {

            db.CheckInGuest(data.Id);

            this.Close();


        }
    }
}

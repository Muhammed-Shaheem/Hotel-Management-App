using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IDatabaseData db;

    public MainWindow(IDatabaseData db)
    {
        InitializeComponent();
        this.db = db;
    }

    private void searchForGuest_Click(object sender, RoutedEventArgs e)
    {
        var bookings = db.SearchBookings(lastNameTxt.Text);
        bookingsList.ItemsSource = bookings;
  
    }

    private void checkIn_Click(object sender, RoutedEventArgs e)
    {

        var checkInBtn = (Button)sender;
        var bookingData = (FullBookingModel)checkInBtn.DataContext;

        var checkInWindow = App.serviceProvider.GetService<CheckInWindow>();
        checkInWindow.PopuateCheckInInfo(bookingData);
        checkInWindow.Show();

    }
}
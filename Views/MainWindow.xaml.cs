using System.Windows;
using System.Windows.Controls;

namespace MyPeopleApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_CancelClick(object sender, RoutedEventArgs e)
        {
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            streetName.Text = string.Empty;
            houseNo.Text = string.Empty;
            apartmentNo.Text = string.Empty;
            postalCode.Text = string.Empty;
            phoneNumber.Text = string.Empty;
        }

    }
}

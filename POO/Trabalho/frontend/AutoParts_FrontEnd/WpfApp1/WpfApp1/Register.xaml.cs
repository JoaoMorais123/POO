using System.Windows;
using WpfFrontEnd.Services;

namespace WpfApp1;

public partial class Register : Window
{
    private ApiService _api;
    public Register()
    {
        _api = new ApiService();
        InitializeComponent();
    }

    private async void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string name = tbName.Text;
            string email = tbEmail.Text;
            string password = tbPassword.Text;

            await _api.RegisterUser(name, email, password);
            MessageBox.Show("User created");

            login login = new login();
            login.Show();

            this.Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}
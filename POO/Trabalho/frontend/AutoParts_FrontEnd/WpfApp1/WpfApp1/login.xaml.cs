using System.Windows;
using AuthLib.Models;
using OrderLib.Models;
using WpfFrontEnd.Services;
using WpfFrontEnd.Views;

namespace WpfApp1;

public partial class login : Window
{
    private ApiService _api;
    public login()
    {
        _api = new ApiService();
        InitializeComponent();
    }

    private async void btnLogin_Click (object sender, RoutedEventArgs e)
    {
        try
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            
            await _api.Login(email, password);
            MessageBox.Show("Login Sucess");
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
            this.Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private async void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        Register register = new Register();
        register.Show();
        this.Close();
    }
    
        

}
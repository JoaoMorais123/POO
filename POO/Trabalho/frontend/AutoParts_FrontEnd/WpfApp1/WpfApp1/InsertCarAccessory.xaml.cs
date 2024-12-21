using System.Windows;
using OrderLib.Models;
using WpfFrontEnd.Services;

namespace WpfApp1;

public partial class InsertCarAccessory : Window
{
    private ApiService _api;
    public InsertCarAccessory()
    {
        _api = new ApiService();
        InitializeComponent();
    }
    

    private async void btnInsertCarAccessories_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            string accessoryCategory = tbDescription.Text;
            string stockQuantity = tbStockQuantity.Text;
            
            var carAccessory = new OrderLib.Models.CarAccessory(1,Convert.ToInt32(stockQuantity),accessoryCategory);
            await _api.InsertCarAccessory(carAccessory);
            MessageBox.Show("Car accessory inserted");
            this.Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
    
}
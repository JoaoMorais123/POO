using System.Windows;
using OrderLib.Models;
using WpfFrontEnd.Services;

namespace WpfApp1;

public partial class UpdateCarAccessory : Window
{
    private ApiService _api;

    public UpdateCarAccessory()
    {
        _api = new ApiService();
        InitializeComponent();
    }

    private async void btnUpdateCarAccessory_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(tbID.Text);
            string accessoryCategory = tbDescription.Text;
            string stockQuantity = tbStockQuantity.Text;

            var carAccessory = new OrderLib.Models.CarAccessory(id, Convert.ToInt32(stockQuantity), accessoryCategory);
            await _api.UpdateCarAccessory(carAccessory);
            MessageBox.Show("Car accessory updated");
            this.Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

    }
}
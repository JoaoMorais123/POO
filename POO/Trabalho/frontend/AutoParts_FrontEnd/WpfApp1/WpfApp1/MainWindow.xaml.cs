using System;
using System.Windows;
using OrderLib.Models;
using WpfApp1;
using WpfFrontEnd.Services;

namespace WpfFrontEnd.Views {
    public partial class MainWindow : Window {
        private ApiService _api;
        private string _role;

        public MainWindow() {
            InitializeComponent();
            _api = new ApiService();
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);

            if (App.Current.Properties.Contains("UserRole")) {
                _role = App.Current.Properties["UserRole"] as string;
            }

            ConfigurePermissions();
        }

        private void ConfigurePermissions() {
            if (_role == "user") {
                // Desativar botões de inserção, atualização e exclusão para usuários
                foreach (var button in new[] {
                    btnInsertOrderLine, btnInsertInvoice, btnInsertCarAccessory, btnInsertCarrier
                }) {
                    button.IsEnabled = false;
                }
            }
        }
        

        // Métodos para OrderLine
        private async void btnGetOrderLines_Click(object sender, RoutedEventArgs e) {
            try {
                var orderLines = await _api.GetOrderLines();
                lvOrderLines.ItemsSource = orderLines;
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao listar OrderLines: {ex.Message}");
            }
        }

        private async void btnInsertOrderLine_Click(object sender, RoutedEventArgs e) {
            try
            {
                // Criando o objeto Product
                var product = new Product(
                    id: 1,                 // ID do produto
                    name: "Produto A",     // Nome do produto
                    stockQuantity: 50,     // Quantidade em estoque
                    weight: 2.0,           // Peso do produto
                    unitPrice: 100.0m,     // Preço unitário
                    IDCarAccessory: 1      // ID do acessório de carro
                );

                // Criando o objeto OrderLine com os parâmetros obrigatórios
                var orderLine = new OrderLine(product, 2); // Produto e quantidade

                // Inserindo a OrderLine
                await _api.InsertOrderLine(orderLine);

                MessageBox.Show("OrderLine inserido com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir OrderLine: {ex.Message}");
            }
        }

        // Métodos para Invoice
        private async void btnGetInvoices_Click(object sender, RoutedEventArgs e) {
            try {
                var invoices = await _api.GetInvoices();
                lvInvoices.ItemsSource = invoices;
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao listar Invoices: {ex.Message}");
            }
        }

        private async void btnInsertInvoice_Click(object sender, RoutedEventArgs e) {
            try {
                var invoice = new Invoice(1, 0.1, "pending");
                
                await _api.InsertInvoice(invoice);
                MessageBox.Show("Invoice inserido com sucesso!");
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao inserir Invoice: {ex.Message}");
            }
        }

        // Métodos para CarAccessory
        private async void btnGetCarAccessories_Click(object sender, RoutedEventArgs e) {
            try {
                var carAccessories = await _api.GetCarAccessories();
                List<string>items = new List<string>();
                carAccessories.ForEach(item => items.Add(item.DisplayInfo()));
                lvCarAccessories.ItemsSource = items;
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao listar Car Accessories: {ex.Message}");
            }
        }

        private async void btnInsertCarAccessory_Click(object sender, RoutedEventArgs e) {
            InsertCarAccessory insertCarAccessory = new InsertCarAccessory();
            insertCarAccessory.Show();
        }
        
        private void btnUpdateCarAccessory_Click(object sender, RoutedEventArgs e)
        {
            UpdateCarAccessory updateCarAccessory = new UpdateCarAccessory();
            updateCarAccessory.Show();
        }

        // Métodos para Carrier
        private async void btnGetCarriers_Click(object sender, RoutedEventArgs e) {
            try {
                var carriers = await _api.GetCarriers();
                lvCarriers.ItemsSource = carriers;
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao listar Carriers: {ex.Message}");
            }
        }

        private async void btnInsertCarrier_Click(object sender, RoutedEventArgs e) {
            try {
                var carrier = new Carrier("Transportadora XYZ", 0.1, 5);

                await _api.InsertCarrier(carrier);
                MessageBox.Show("Carrier inserido com sucesso!");
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao inserir Carrier: {ex.Message}");
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) {
            var loginWindow = new login();
            loginWindow.Show();
            this.Close();
        }

        
    }
    
}

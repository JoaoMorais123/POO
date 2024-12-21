using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using AuthLib.Models;
using OrderLib.Models;
using WpfApp1;
using WpfApp1.Models;
using CarAccessory = OrderLib.Models.CarAccessory;

namespace WpfFrontEnd.Services {
    
    public class ApiService {
        private HttpClient _client;

        public ApiService() {
            SetHttpClient();
        }
        
        private void SetHttpClient ()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _client = new HttpClient(httpClientHandler) {
                BaseAddress = new Uri("https://localhost:7231")
            };
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SingletonToken.Instance.Token);
        }

        #region User
        public async Task<List<User>> GetUser() {
            var response = await _client.GetAsync("/getUser");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<User>>();
        }

        public async Task<ResponseLogin?> Login(string email, string password)
        {
            var loginRequest = new { Email = email, Password = password };
            var response = await _client.PostAsJsonAsync("/Login", loginRequest);
            response.EnsureSuccessStatusCode();
            var responseLogin = await response.Content.ReadFromJsonAsync<ResponseLogin>();
            SingletonToken.Instance.Token = responseLogin.access_token;
            SetHttpClient();
            return responseLogin;
        }

        public async Task RegisterUser(string name, string email, string password) {
            var registerRequest = new { Name = name, Email = email, Password = password };
            var response = await _client.PostAsJsonAsync("/Register", registerRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUser(string name, string email) {
            var deleteRequest = new { Name = name, Email = email };
            var response = await _client.DeleteAsync($"/Delete");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUser(int id, string name, string email, string password) {
            var updateRequest = new { IDUser = id, Name = name, Email = email, Password = password };
            var response = await _client.PostAsJsonAsync("/Update", updateRequest);
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Product
        public async Task<List<Product>> GetProducts() {
            var response = await _client.GetAsync("/GetProducts");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        public async Task InsertProduct(Product product) {
            var productRequest = new {
                name = product.Name,
                StockQuantity = product.IsStockAvailable(9999999),
                weight = product.Weight,
                unitPrice = product.UnitPrice,
                IDCarAccessory = product.IDCarAccessory
            };
            var response = await _client.PostAsJsonAsync("/InsertProducts", productRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProduct(int id, Product product) {
            var productRequest = new {
                name = product.Name,
                stockQuantity = product.IsStockAvailable(9999999),
                weight = product.Weight,
                unitPrice = product.UnitPrice,
                IDCarAccessory = product.IDCarAccessory
            };
            var response = await _client.PutAsJsonAsync($"/UpdateProducts/{id}", productRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProduct(int id) {
            var response = await _client.DeleteAsync($"/DeleteProducts/{id}");
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Order
        public async Task<List<Order>> GetOrders() {
            var response = await _client.GetAsync("/GetOrders");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Order>>();
        }

        public async Task InsertOrder(Order order) {
            var orderRequest = new {
                orderDate = order.OrderDate,
                shippingAddress = order.ShippingAddress
            };
            var response = await _client.PostAsJsonAsync("/InsertOrders", orderRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrder(int id, Order order) {
            var orderRequest = new {
                orderDate = order.OrderDate,
                shippingAddress = order.ShippingAddress
            };
            var response = await _client.PutAsJsonAsync($"/UpdateOrders/{id}", orderRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrder(int id) {
            var response = await _client.DeleteAsync($"/DeleteOrders/{id}");
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region OrderLine
        public async Task<List<OrderLine>> GetOrderLines() {
            var response = await _client.GetAsync("/GetOrderLines");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrderLine>>();
        }

        public async Task InsertOrderLine(OrderLine orderLine) {
            var orderLineRequest = new {
                quantity = orderLine.Quantity,
                totalPrice = orderLine.TotalPrice,
                unitPrice = orderLine.UnitPrice
            };
            var response = await _client.PostAsJsonAsync("/InsertOrderLines", orderLineRequest);
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Invoice
        public async Task<List<Invoice>> GetInvoices() {
            var response = await _client.GetAsync("/GetInvoices");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Invoice>>();
        }

        public async Task InsertInvoice(Invoice invoice) {
            var invoiceRequest = new {
                invoiceData = invoice.InvoiceDate,
                totalAmount = invoice.TotalAmount,
                processPayment = invoice.ProcessPayment
            };
            var response = await _client.PostAsJsonAsync("/InsertInvoices", invoiceRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateInvoice(int id, Invoice invoice) {
            var invoiceRequest = new {
                invoiceData = invoice.InvoiceDate,
                totalAmount = invoice.TotalAmount,
                processPayment = invoice.ProcessPayment
            };
            var response = await _client.PutAsJsonAsync($"/UpdateInvoices/{id}", invoiceRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteInvoice(int id) {
            var response = await _client.DeleteAsync($"/DeleteInvoices/{id}");
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region CarAccessory
        public async Task<List<CarAccessory>> GetCarAccessories() {
            var response = await _client.GetAsync("GetCarAccessories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CarAccessory>>();
        }

        public async Task InsertCarAccessory(CarAccessory carAccessory) {
            var carAccessoryRequest = new {
                stockQuantity = carAccessory.StockQuantity,
                accessoryCategory = carAccessory.AccessoryCategory,
            };
            var response = await _client.PostAsJsonAsync("/InsertCarAccessories", carAccessoryRequest);
            response.EnsureSuccessStatusCode();
            
        }

        public async Task UpdateCarAccessory(CarAccessory carAccessory) {
            var carAccessoryRequest = new {
                id = carAccessory.ID,
                stockQuantity = carAccessory.StockQuantity,
                accessoryCategory = carAccessory.AccessoryCategory
            };
            var response = await _client.PutAsJsonAsync($"/UpdateCarAccessories/{carAccessory.ID}", carAccessoryRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCarAccessory(int id) {
            var response = await _client.DeleteAsync($"/DeleteCarAccessories/{id}");
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Carrier
        public async Task<List<Carrier>> GetCarriers() {
            var response = await _client.GetAsync("/GetCarriers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Carrier>>();
        }

        public async Task InsertCarrier(Carrier carrier) {
            var carrierRequest = new {
                name = carrier.Name,
                shippingCost = carrier.ShippingCostPerKg,
                estimatedDeliveryTime = carrier.EstimatedDeliveryTime
            };
            var response = await _client.PostAsJsonAsync("/InsertCarriers", carrierRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCarrier(int id, Carrier carrier) {
            var carrierRequest = new {
                name = carrier.Name,
                estimatedDeliveryTime = carrier.EstimatedDeliveryTime
            };
            var response = await _client.PutAsJsonAsync($"/UpdateCarriers/{id}", carrierRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCarrier(int id) {
            var response = await _client.DeleteAsync($"/DeleteCarriers/{id}");
            response.EnsureSuccessStatusCode();
        }
        #endregion
    }
}

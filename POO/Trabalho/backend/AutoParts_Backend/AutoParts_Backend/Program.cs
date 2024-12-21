using System;
using System.Data;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using AuthLib;
using AuthLib.Models;
using AutoParts_Backend;
using AutoParts_Backend.Models;
using AutoParts_Backend.Models.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderLib;
using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using OrderLib.Models;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;



internal class Program
{
    public static void Main(string[] args)
    {
        NpgsqlConnection? npgsql = null; 
        var builder = WebApplication.CreateBuilder(args);
        
        var jwtOptions = builder.Configuration
            .GetSection("JwtOptions")
            .Get<jwtOptions.JwtOptions>();

        builder.Services.AddSingleton(jwtOptions);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                //convert the string signing key to byte array
                byte[] signingKeyBytes = Encoding.UTF8
                    .GetBytes(jwtOptions.SigningKey);

                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
        builder.Services.AddAuthorization();
        

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        
// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        #region Function 
        
        // Função genérica para estabelecer conexão
        NpgsqlConnection EnsureConnection()
        {
            if (npgsql == null)
            {
                npgsql = connection.GetConnection();
            }
            return npgsql;
        }
        #endregion
        
        #region Login
        app.MapGet("/getUser", [Authorize(Roles = "Admin")] () =>
            {
                try
                {
                    List<Person> persons = new();

                    npgsql = EnsureConnection();
                    npgsql.Open();

                    using (var command =
                           new NpgsqlCommand(
                               "SELECT \"IDUser\", \"Name\", \"Password\", \"Email\", \"Role\" FROM public.\"User\"", npgsql))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetString(4) == "user"){
                                User user = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                                persons.Add(user);
                            }
                            else
                            {
                                Admin admin = new Admin(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                                persons.Add(admin);
                            }
                        }
                        reader.Close();
                    }

                    npgsql.Close();
                    return Results.Ok(persons);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

                return Results.Problem("FAIL");
            })
            .WithName("GetUser")
            .RequireAuthorization()
            .WithOpenApi();

       

        app.MapPost("/Login", ([FromBody] LoginRequest loginRequest, HttpContext ctx, jwtOptions.JwtOptions jwtOptions) =>
            {
                try{
                    
                    string password = loginRequest.Password;
                    string email = loginRequest.Email;
        
        
                    Person? user = null;
        
        
                    var npgsql = EnsureConnection();
                    npgsql.Open();
        
        
                    using (var command = 
                           new NpgsqlCommand(
                               "select \"Name\", \"Email\", \"Role\" from  public.\"User\" where \"Password\" =:password and \"Email\" =:email",
                               npgsql))
                    {
                        command.Parameters.AddWithValue("password", password);
                        command.Parameters.AddWithValue("email", email);
            
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetString(2) == "user")
                            {
                                user = new User(reader.GetString(0), "", reader.GetString(1));
                            }
                            else
                            {
                                user = new Admin(reader.GetString(0), "", reader.GetString(1));
                            }
                        }
                        reader.Close();
                    }
                    npgsql.Close();
                    if (user != null)
                    {
                        var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationSeconds);
                        var accessToken = CreateAccessToken(
                            jwtOptions,
                            user.Email,
                            user.AuthLevel,
                            TimeSpan.FromMinutes(60),
                            new[] { "read_todo", "create_todo" });
                        return Results.Ok(new
                        {
                            access_token = accessToken,
                            expiration = (int)tokenExpiration.TotalSeconds,
                            type = "bearer",
                            data=user
                        });
                    }
                    else
                    {
                        throw new ApplicationException("Username or password is incorrect");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

                return Results.Problem("FAIL");
            })
            .WithName("Login")
            .AllowAnonymous()
            .WithOpenApi();

        app.MapPost("/Register", ([FromBody] RegisterUserRequest registerRequest) =>
            {
                try {
                    
                    User? user = null;
      
      
                    npgsql = EnsureConnection();
                    npgsql.Open();
      
                    using (var command = 
                           new NpgsqlCommand(
                               "INSERT INTO public.\"User\"(\n\t\"Name\", \"Password\", \"Email\")\n\tVALUES (:name, :password, :email)",
                               npgsql))
                    {
                        command.Parameters.AddWithValue("password", registerRequest.Password);
                        command.Parameters.AddWithValue("email", registerRequest.Email);
                        command.Parameters.AddWithValue("name", registerRequest.Name);
          
                        if(command.ExecuteNonQuery() > 0)
                            Console.WriteLine("Success");
                        else
                        {
                            Console.WriteLine("Failure");
                        }
                    }
                    npgsql.Close();
                    if (user != null)
                        throw new ApplicationException("Query returned unsuccessfully ");
                    else
                    {
                        return Results.Ok("Query returned successfully ");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

                return Results.Problem("FAIL");
            })
            .WithName("Register")
            .WithOpenApi();

        app.MapDelete("/Delete", [Authorize(Roles = "Admin")] ([FromBody] DeleteRequest registerRequest) =>
            {
                try {
                    
                    string name = registerRequest.Name;
                    string email = registerRequest.Email;
        
                    User? user = null;
        
        
                    npgsql = EnsureConnection();
                    npgsql.Open();

                    using (var command = new NpgsqlCommand("DELETE FROM public.\"User\" where \"Name\" =:name and \"Email\" =:email",npgsql))
                    {
                        command.Parameters.AddWithValue("email", email);
                        command.Parameters.AddWithValue("name", name);
            
                        if(command.ExecuteNonQuery() > 0)
                            Console.WriteLine("Success");
                        else
                        {
                            Console.WriteLine("Failure");
                        }
                    }
        
                    npgsql.Close();
                    if (user != null)
                        throw new ApplicationException("Query returned unsuccessfully ");
                    else
                    {
                        return Results.Ok("Query returned successfully ");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

                return Results.Problem("FAIL");
            })
            .WithName("Delete")
            .RequireAuthorization()
            .WithOpenApi();

        app.MapPost("/Update", [Authorize(Roles = "Admin")] ([FromBody] UpdateRequest UpdateRequest) =>
            {
                try{
    
                    Person? user = null;
        
                    npgsql = EnsureConnection();
                    npgsql.Open();

                    using (var command = new NpgsqlCommand("UPDATE public.\"User\" SET \"Name\"=:name,\"Password\"=:password,\"Email\"=:email WHERE \"IDUser\" = :IDUser",npgsql))
                    {
                        command.Parameters.AddWithValue("name", UpdateRequest.Name);
                        command.Parameters.AddWithValue("email", UpdateRequest.Email);
                        command.Parameters.AddWithValue("password", UpdateRequest.Password);
                        command.Parameters.AddWithValue("IDUser", UpdateRequest.IDUser);
            
                        if(command.ExecuteNonQuery() > 0){
                            Console.WriteLine("Success");
                        }    
                        else
                        {
                            Console.WriteLine("Failure");
                        }
                    }
                    npgsql.Close();
                    if (user != null)
                        throw new ApplicationException("Query returned unsuccessfully ");
                    else
                    {
                        return Results.Ok("Query returned successfully ");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

                return Results.Problem("FAIL");
            })
            .WithName("Update")
            .RequireAuthorization()
            .WithOpenApi();
        #endregion

        #region Products
        app.MapGet("/GetProducts", () =>
            {
                try {
                    
    
                    List<Product> products = new();
        
                    npgsql = EnsureConnection();
                    npgsql.Open();
        
        
                    using (var command = 
                           new NpgsqlCommand("SELECT \"IDProduct\",\"Name\",\"StockQuantity\", \"Weight\", \"UnitPrice\", \"IDCarAccessory\" FROM public.\"Product\"", 
                               npgsql))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Product product = new Product(reader.GetInt32(0),reader.GetString(1), reader.GetInt32(2), 
                                reader.GetDouble(3), Convert.ToDecimal(reader.GetDouble(4)), reader.GetInt32(5));
                            products.Add(product);
                        }
                        reader.Close();
                    }
                    npgsql.Close();
                    return Results.Ok(products);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

                return Results.Problem("FAIL");
            })
            .WithName("GetProducts")
            .RequireAuthorization()
            .WithOpenApi();
        
        app.MapPost("/InsertProducts", [Authorize(Roles = "Admin")] ([FromBody] ProductRequest product) =>
        {
            try {
            
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("INSERT INTO public.\"Product\" (\"Name\", \"StockQuantity\", \"Weight\", \"UnitPrice\", \"IDCarAccessory\") VALUES(:name,:stockQuantity,:weight,:unitPrice,:IDCarAccessory)", npgsql))
                {
                    command.Parameters.AddWithValue("name", product.name);
                    command.Parameters.AddWithValue("stockQuantity", product.stockQuantity);
                    command.Parameters.AddWithValue("weight", product.weight);
                    command.Parameters.AddWithValue("unitPrice", product.unitPrice);
                    command.Parameters.AddWithValue("IDCarAccessory", product.IDCarAccessory);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Product inserted successfully.");
            }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }
            return Results.Problem("FAIL");
        })
        .WithName("InsertProducts")
        .RequireAuthorization()
        .WithOpenApi();

        app.MapPut("/UpdateProducts/{IDProduct}",  [Authorize(Roles = "Admin")] ([FromRoute] int IDProduct, [FromBody] ProductRequest product) =>
        {
            try {
                npgsql = EnsureConnection();
                npgsql.Open();

                using (var command = new NpgsqlCommand(
                           "UPDATE public.\"Product\" SET \"Name\"=:name,\"StockQuantity\"=:stockQuantity,\"Weight\"=:weight,\"UnitPrice\"=:unitPrice,\"IDCarAccessory\"=:IDCarAccessory WHERE \"IDProduct\"=:IDProduct",
                           npgsql))
                {
                    command.Parameters.AddWithValue("name", product.name);
                    command.Parameters.AddWithValue("stockQuantity", product.stockQuantity);
                    command.Parameters.AddWithValue("weight", product.weight);
                    command.Parameters.AddWithValue("unitPrice", product.unitPrice);
                    command.Parameters.AddWithValue("IDCarAccessory", product.IDCarAccessory);
                    command.Parameters.AddWithValue("IDProduct", IDProduct);
                    command.ExecuteNonQuery();
                }

                npgsql.Close();
                return Results.Ok("Product updated successfully.");
            }
            catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    npgsql.Close();
                }

            return null;
        })
        .WithName("UpdateProducts")
        .RequireAuthorization()
        .WithOpenApi();

        app.MapDelete("/DeleteProducts/{IDProduct}", [Authorize(Roles = "Admin")] ([FromRoute] int IDProduct) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
    
                using (var command = new NpgsqlCommand("DELETE FROM public.\"Product\" WHERE \"IDProduct\" = :idProduct", npgsql))
                {
                    command.Parameters.AddWithValue("IDProduct", IDProduct);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                
                return Results.Ok("Product deleted successfully.");
            }
                catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }
            return Results.Problem("SERVER ERROR.");
        })
        .WithName("DeleteProducts")
        .RequireAuthorization()
        .WithOpenApi();
        #endregion

        #region CarAccessories

        app.MapGet("/GetCarAccessories", () =>
        {
            try{
                List<CarAccessory> carAccessories = new();
        
                npgsql = EnsureConnection();
                npgsql.Open();
        
        
                using (var command = new NpgsqlCommand("SELECT \"IDAccessory\", \"StockQuantity\", \"AccessoryCategory\" FROM public.\"CarAccessory\"", 
                           npgsql))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CarAccessory carAccessorie = new CarAccessory(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2));
                        carAccessories.Add(carAccessorie);
                    }
                    reader.Close();
                }
                npgsql.Close();
                return Results.Ok(carAccessories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("GetCarAccessories")
        .RequireAuthorization()
        .WithOpenApi();


        app.MapPost("/InsertCarAccessories", [Authorize(Roles = "Admin")] ([FromBody] CarAccessoryRequest CarAccessoryRequest) =>
        {
            try {

    
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("INSERT INTO public.\"CarAccessory\" (\"StockQuantity\", \"AccessoryCategory\") VALUES (:stockQuantity, :accessoryCategory)", npgsql))
                {
                    command.Parameters.AddWithValue("stockQuantity", CarAccessoryRequest.stockQuantity);
                    command.Parameters.AddWithValue("AccessoryCategory", CarAccessoryRequest.accessoryCategory);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("CarAccessory inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("InsertCarAccessories")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapPut("/UpdateCarAccessories/{IDAccessory}", [Authorize(Roles = "Admin")] ([FromRoute] int IDAccessory, [FromBody] CarAccessoryRequest CarAccessoryRequest) =>
        {
            try {
    
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("UPDATE public.\"CarAccessory\" SET \"StockQuantity\" = :stockQuantity, \"AccessoryCategory\" = :accessoryCategory WHERE \"IDAccessory\" = :IDAccessory", npgsql))
                {
                    command.Parameters.AddWithValue("StockQuantity", CarAccessoryRequest.stockQuantity);
                    command.Parameters.AddWithValue("AccessoryCategory", CarAccessoryRequest.accessoryCategory);
                    command.Parameters.AddWithValue("IDAccessory", IDAccessory);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("CarAccessory updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("UpdateCarAccessories")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapDelete("/DeleteCarAccessories/{IDAccessory}",  [Authorize(Roles = "Admin")] ([FromRoute] int IDAccessory) =>
        {
            try{
    
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("DELETE FROM public.\"CarAccessory\" WHERE \"IDAccessory\" = :IDAccessory", npgsql))
                {
                    command.Parameters.AddWithValue("IDAccessory", IDAccessory);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("CarAccessory deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("DeleteCarAccessories")
        .RequireAuthorization()
        .WithOpenApi();    
        #endregion

        #region orders
        
        app.MapGet("/GetOrders", () =>
        {
            try {
                
                List<Order> orders = new();
                var npgsql = EnsureConnection();
                npgsql.Open();
        
                
                using (var command = new NpgsqlCommand("SELECT \"IDOrder\", \"ShippingAddress\", \"OrderStatus\", \"TotalAmount\", \"DisplayInfo\", \"CreateDate\" FROM public.\"Order\"", 
                           npgsql))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order order = new Order(reader.GetInt32(0),reader.GetString(1));
                        orders.Add(order);
                    
                    }
                    reader.Close();
                }
                npgsql.Close();
                return Results.Ok(orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("GetOrders")
        .RequireAuthorization()
        .WithOpenApi();    


        app.MapPost("/InsertOrders", [Authorize(Roles = "Admin")] ([FromBody] OrderRequest orderRequest) =>
        {
            try{
    
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("INSERT INTO public.\"Order\" (\"ShippingAddress\", \"OrderStatus\", \"TotalAmount\", \"DisplayInfo\", \"CreateDate\") VALUES (:shippingAddress, :orderStatus, :totalAmount, :DisplayInfo, :CreateDate)",
                           npgsql))
                {
                    command.Parameters.AddWithValue("ShippingAddress", orderRequest.shippingAddress);
                    command.Parameters.AddWithValue("OrderStatus", orderRequest.orderStatus);
                    command.Parameters.AddWithValue("TotalAmount", orderRequest.totalAmount);
                    command.Parameters.AddWithValue("DisplayInfo", orderRequest.DisplayInfo);
                    command.Parameters.AddWithValue("CreateDate", orderRequest.CreateDate);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Order inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("InsertOrders")
        .RequireAuthorization()
        .WithOpenApi();     

        app.MapPut("/UpdateOrders/{IDOrder}", [Authorize(Roles = "Admin")] ([FromRoute] int IDOrder, [FromBody] OrderRequest request) =>
        {
            try{
    
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("UPDATE public.\"Order\" SET \"ShippingAddress\"=:shippingAddress,\"OrderStatus\"=:orderStatus, \"TotalAmount\"=:totalAmount WHERE \"IDOrder\"=:IDOrder", npgsql))
                {
                    command.Parameters.AddWithValue("shippingAddress", request.shippingAddress);
                    command.Parameters.AddWithValue("orderStatus", request.orderStatus);
                    command.Parameters.AddWithValue("totalAmount", request.totalAmount);
                    command.Parameters.AddWithValue("IDOrder", IDOrder);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Order updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("UpdateOrders")
        .RequireAuthorization()
        .WithOpenApi();     

        app.MapDelete("/DeleteOrders/{IDOrder}", [Authorize(Roles = "Admin")] ([FromRoute] int IDOrder) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("DELETE FROM public.\"Order\" WHERE \"IDOrder\" = :IDOrder", npgsql))
                {
                    command.Parameters.AddWithValue("IDOrder", IDOrder);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("DeleteOrders")
        .RequireAuthorization()
        .WithOpenApi();     

        #endregion
        
        #region OrdersLines
        app.MapGet("/GetOrderLines", () =>
        {
            try{
                var npgsql = EnsureConnection();
                npgsql.Open();
        
                List<OrderLine> orderLines = new();
                using (var command = new NpgsqlCommand("SELECT \"IDOrderLine\", \"Quantity\", \"IDCarrier\", \"IDOrder\", \"IDProduct\" FROM public.\"OrderLine\"", 
                           npgsql))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        using (var command2 = new NpgsqlCommand("SELECT \"IDProduct\", \"Name\", \"Weight\", \"UnitPrice\", \"IDCarAccessory\", \"StockQuantity\" FROM public.\"Product\" WHERE \"IDPoduct\" =:id ", 
                                   npgsql))
                        {
                            command2.Parameters.AddWithValue("id", reader.GetInt32(0));
                            var reader2 = command2.ExecuteReader();
                            
                            while (reader2.Read())
                            {
                                Product product = new Product(reader2.GetInt32(0), reader2.GetString(1), reader2.GetInt32(2), 
                                    reader2.GetDouble(3),reader2.GetDecimal(4),reader2.GetInt32(5));
                                OrderLine orderLine = new OrderLine(product, reader.GetInt32(5));
                                orderLines.Add(orderLine);
                            }
                        }
                    }
                    reader.Close();
                }
                npgsql.Close();
                return Results.Ok(orderLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("GetOrdersLines")
        .RequireAuthorization()
        .WithOpenApi();     

        app.MapPost("/InsertOrderLines", [Authorize(Roles = "Admin")] ([FromBody] OrderLineRequest request) =>
        {
            try {
                
                
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("INSERT INTO public.\"OrderLine\"( \"Quantity\", \"IDCarrier\", \"IDOrder\", \"IDProduct\") VALUES (:Quantity, :IDCarrier, :IDOrder, :IDProduct)", npgsql))
                {
                    command.Parameters.AddWithValue("Quantity", request.Quantity);
                    command.Parameters.AddWithValue("IDCarrier", request.IDCarrier);
                    command.Parameters.AddWithValue("IDOrder", request.IDOrder);
                    command.Parameters.AddWithValue("IDProduct", request.IDProduct);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Order Line inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("InsertOrdersLines")
        .RequireAuthorization()
        .WithOpenApi();    


        #endregion

        #region Invoices

        
        app.MapGet("/GetInvoices", () =>
        {
            try {
                List<Invoice> invoices = new();
                
                npgsql = EnsureConnection();
                npgsql.Open();
        
                
                using (var command = new NpgsqlCommand("SELECT \"IDInvoice\", \"InvoiceData\", \"TotalAmount\", \"ProcessPayment\", \"IDOrderLine\" FROM public.\"Invoice\"", 
                           npgsql))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Invoice invoice = new Invoice(reader.GetInt32(0),reader.GetDouble(2), reader.GetString(3));
                        invoices.Add(invoice);
                    }
                    reader.Close();
                }
                npgsql.Close();
                return Results.Ok(invoices);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("GetInvoices")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapPost("/InsertInvoices", [Authorize(Roles = "Admin")] ([FromBody] InvoiceRequest request) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("INSERT INTO public.\"Invoice\"(\"InvoiceData\", \"TotalAmount\", \"ProcessPayment\", \"IDOrderLine\") VALUES (:InvoiceData, :TotalAmount, :ProcessPayment, :IDOrderLine)"
                           , npgsql))
                {
                    command.Parameters.AddWithValue("InvoiceData", request.InvoiceDate);
                    command.Parameters.AddWithValue("TotalAmount", request.TotalAmount);
                    command.Parameters.AddWithValue("ProcessPayment", request.ProcessPayment);
                    command.Parameters.AddWithValue("IDOrderLine", request.IDOrderLine);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Invoice inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("InsertInvoices")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapPut("/UpdateInvoices/{IDInvoice}", [Authorize(Roles = "Admin")] ([FromRoute] int IDInvoice, [FromBody] InvoiceRequest request) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("UPDATE public.\"Invoice\" SET \"InvoiceData\" = :InvoiceData, \"TotalAmount\" = :TotalAmount, \"ProcessPayment\" = :ProcessPayment, \"IDOrderLine\" = :IDOrderLine WHERE \"IDInvoice\" = :IDInvoice", npgsql))
                {
                    command.Parameters.AddWithValue("invoiceData", request.InvoiceDate);
                    command.Parameters.AddWithValue("totalAmount", request.TotalAmount);
                    command.Parameters.AddWithValue("processPayment", request.ProcessPayment);
                    command.Parameters.AddWithValue("IDOrderLine", request.IDOrderLine);
                    command.Parameters.AddWithValue("IDInvoice", IDInvoice);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Invoice updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("UpdateInvoices")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapDelete("/DeleteInvoices/{IDInvoice}", [Authorize(Roles = "Admin")] ([FromRoute] int IDInvoice) =>
        {
            try {
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("DELETE FROM public.\"Invoice\" WHERE \"IDInvoice\" = :IDInvoice", npgsql))
                {
                    command.Parameters.AddWithValue("IDInvoice", IDInvoice);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Invoice deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("DeleteInvoices")
        .RequireAuthorization()
        .WithOpenApi();    
        #endregion

        #region Carriers
        
        app.MapGet("/GetCarriers", () =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                List<Carrier> carriers = new();
                
                using (var command = new NpgsqlCommand("SELECT \"IDCarrier\", \"Name\", \"ShippingCost\", \"CalculateWeight\", \"EstimatedDeliveryTime\", \"ValidateTransport\" FROM public.\"Carrier\"", npgsql))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Carrier carrier = new Carrier(reader.GetString(1),reader.GetDouble(2),reader.GetInt32(4));
                        carriers.Add(carrier);
                    }
                    reader.Close();
                }
                npgsql.Close();
                return Results.Ok(carriers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("GetCarriers")
        .RequireAuthorization()
        .WithOpenApi();

        app.MapPost("/InsertCarriers", [Authorize(Roles = "Admin")] ([FromBody] CarrierRequest CarrierRequest) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("INSERT INTO public.\"Carrier\"(\"Name\", \"CalculateWeight\", \"ValidateTransport\", \"EstimatedDeliveryTime\", \"ShippingCost\") VALUES (:name, :calculateWeight, :validateTransport, :estimatedDeliveryTime, :shippingCost);", 
                           npgsql))
                {
                    command.Parameters.AddWithValue("Name", CarrierRequest.name);
                    command.Parameters.AddWithValue("ShippingCost", CarrierRequest.shippingCost);
                    command.Parameters.AddWithValue("CalculateWeight", CarrierRequest.calculateWeight);
                    command.Parameters.AddWithValue("EstimatedDeliveryTime", CarrierRequest.estimatedDeliveryTime);
                    command.Parameters.AddWithValue("ValidateTransport", CarrierRequest.validateTransport);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Carrier inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("InsertCarriers")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapPut("/UpdateCarriers/{IDCarrier}", [Authorize(Roles = "Admin")] ([FromRoute] int IDCarrier, [FromBody] CarrierRequest CarrierRequest) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("UPDATE public.\"Carrier\" SET \"Name\" =:name, \"ShippingCost\" =:shippingCost, \"CalculateWeight\" =:calculateWeight, \"EstimatedDeliveryTime\" =:estimatedDeliveryTime, \"ValidateTransport\" =:validateTransport WHERE \"IDCarrier\" = :IDCarrier", npgsql))
                {
                    command.Parameters.AddWithValue("Name", CarrierRequest.name);
                    command.Parameters.AddWithValue("ShippingCost", CarrierRequest.shippingCost);
                    command.Parameters.AddWithValue("CalculateWeight", CarrierRequest.calculateWeight);
                    command.Parameters.AddWithValue("EstimatedDeliveryTime", CarrierRequest.estimatedDeliveryTime);
                    command.Parameters.AddWithValue("ValidateTransport", CarrierRequest.validateTransport);
                    command.Parameters.AddWithValue("IDCarrier", IDCarrier);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Carrier updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("UpdateCarriers")
        .RequireAuthorization()
        .WithOpenApi();    

        app.MapDelete("/DeleteCarriers/{IDCarrier}", [Authorize(Roles = "Admin")] ([FromRoute] int IDCarrier) =>
        {
            try{
                npgsql = EnsureConnection();
                npgsql.Open();
        
                using (var command = new NpgsqlCommand("DELETE FROM public.\"Carrier\" WHERE \"IDCarrier\" = :IDCarrier", npgsql))
                {
                    command.Parameters.AddWithValue("IDCarrier", IDCarrier);
                    command.ExecuteNonQuery();
                }
                npgsql.Close();
                return Results.Ok("Carrier deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                npgsql.Close();
            }

            return Results.Problem("FAIL");
        })
        .WithName("DeleteCarriers")
        .RequireAuthorization()
        .WithOpenApi();
        
        #endregion
        
        
        static string CreateAccessToken(
            jwtOptions.JwtOptions jwtOptions,
            string username,
            Authorization.AuthorizationLevel authorizationLevel,
            TimeSpan expiration,
            string[] permissions)
        {
            var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                // 👇 one of the most popular. 
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Role, authorizationLevel.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials);

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rawToken;
        }

        app.Run();
    }
}



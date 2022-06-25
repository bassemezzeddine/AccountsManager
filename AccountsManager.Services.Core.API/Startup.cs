using AccountsManager.Services.Core.API.Contracts;
using AccountsManager.Services.Core.API.Helpers;
using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Enums;
using AccountsManager.Services.Core.Data.Models;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Service.Models.Mapping;
using AccountsManager.Services.Core.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AccountsManager.Services.Core.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers();

            services.AddDbContext<CoreDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "CoreDB"));

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IResponseHelper, ResponseHelper>();
            services.AddTransient<IRequestInfoService, RequestInfoService>();

            // AUTOMAPPER CONFIGURATION
            var dataProtectionProvider = DataProtectionProvider.Create("CoreAPI");
            var protector = dataProtectionProvider.CreateProtector("AccountsManager.Services.Core.API");
            services.AddSingleton(protector);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile(protector));
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // SWAGGER CONFIGURATION
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AccountsManager.Services.Core.API",
                    Version = "v1",
                    Description = "Accounts Manager Core API documentation",
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // GENERATE DATABASE INITIAL DATA
            InitializeDatabase(app);

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccountsManager.Services.Core.API v1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<CoreDbContext>();

            var statuses = new List<Status>
            {
                new Status
                {
                    Id = 1,
                    Name = "Active"
                },
                new Status
                {
                    Id = 2,
                    Name = "Inactive"
                },
                new Status
                {
                    Id= 3,
                    Name = "Closed"
                },
                new Status
                {
                    Id= 4,
                    Name = "Completed"
                }
            };
            context.Status.AddRange(statuses);
            context.SaveChanges();

            var userId = Guid.NewGuid();
            var customer = new Customer
            {
                Name = "Bassem",
                Surname = "Ezzeddine",
                StatusId = (int)StatusEnum.Active,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = "000001-001",
                        Balance = 94.42M,
                        Description = "Account 01",
                        StatusId = (int)StatusEnum.Active,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Reference = Guid.NewGuid().ToString(),
                                Amount = -1.10M,
                                OpeningBalance = 95.52M,
                                ClosingBalance = 94.42M,
                                Description = "Transaction 02",
                                StatusId = (int)StatusEnum.Completed,
                                CreatedBy = userId,
                                CreatedOn = DateTime.UtcNow
                            },
                            new Transaction
                            {
                                Reference = Guid.NewGuid().ToString(),
                                Amount = 95.52M,
                                OpeningBalance = 0.00M,
                                ClosingBalance = 95.52M,
                                Description = "Transaction 01",
                                StatusId = (int)StatusEnum.Completed,
                                CreatedBy = userId,
                                CreatedOn = DateTime.UtcNow
                            }
                        }
                    }
                }
            };
            context.Customer.Add(customer);
            context.SaveChanges();
        }
    }
}

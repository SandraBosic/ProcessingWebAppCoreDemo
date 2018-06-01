using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Interfaces.DataAccess.RecurringBatch;
using Core.Interfaces.Managers.Export;
using Core.Interfaces.Managers.RecurringBatch;
using Core.Interfaces.System;
using Core.Interfaces.VirtualNumberApiAccess;
using Core.System;
using Managers.Export;
using Managers.RecurringBatch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecurringPaymentDataAccess;
using VirtualNumberApiAccess;
using TransactionManager = System.Transactions.TransactionManager;

namespace ProcessingWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.Configure<PagingConfiguration>(Configuration.GetSection("Pager"));

            //core
            services.AddTransient<IGuid, SystemGuid>();
            services.AddTransient<IClock, SystemClock>();
            services.AddTransient<IConvert, SystemConvert>();

            //data access
            services.AddTransient<IBatchRepository, BatchRepository>();
            services.AddTransient<IBatchReportRepository, BatchReportRepository>();
            services.AddTransient<ITransactionReportRepository, TransactionReportRepository>();

            //manager registry
            services.AddTransient<IManageRecurringBatches, BatchManager>();
            services.AddTransient<IManageRecurringBatchTransactions, Managers.RecurringBatch.TransactionManager>();
            
            services.AddTransient<IMapForExport, RecurringBatchMapForExport>(); //.Named("RecurringBatchDataViewModel");
            //services.AddTransient<IMapForExport, RecurringBatchTransactionMapForExport>().Named("RecurringBatchTransactionDataViewModel");
            services.AddTransient<IManageExport, ExportManager>();

            //virtual number api access
            services.AddTransient<IVirtualNumberApiAccess, VirtualNumberApiAccess.VirtualNumberApiAccess>();
            services.AddTransient<IProviderVirtualNumberManagerClient, VirtualNumberManagerClientProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

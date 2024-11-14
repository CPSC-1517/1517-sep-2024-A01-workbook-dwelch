using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.BLL;

#region Additional Namespaces
using WestWindSystem.DAL;
#endregion

namespace WestWindSystem
{
    public static class WestWindExtensions
    {
        //this class can hold an set of extension methods
        //this sample creates an extension method that will add services
        //  to IServiceCollection
        //this method will be called from an separate project to
        //  gain data from the WestWind database
        //In this demo, the call will be done in the WestWindApp Program.cs file
        //this method will do 2 things:
        //  a) register the context connection string
        //  b) register ALL services that I create within the BLL class(es)
        public static void WestWindExtensionServices(this IServiceCollection services,
            Action<DbContextOptionsBuilder>options)
        {
            //handle the connection string
            //add my context class to the services (IServiceCollection)
            services.AddDbContext<WestWindContext>(options);


            //YOU MUST REGISTER EACH AND EVERY BLL SERVICE CLASS YOU WISH YOUR WEB APP TO USE

            //to register a service class you will use the IServiceCollection method
            //  .AddTransient<T>
            //for any outside coding that requires acces to one or more services
            //  you MUST register the service class
            //ALL methods within the class are available to the outside world
            services.AddTransient<BuildVersionServices>((serviceProvider) =>
                {
                    //this statement obtains the context information reqistered above
                    var context = serviceProvider.GetService<WestWindContext>();

                    //create an instance of the service class and register said class in
                    //  IServiceCollection
                    //once the class has been registered, it can be used by ANY ourside source
                    //  as long as the outside source has referenced the extension class
                    //  (see your Program.cs in your web app)
                    return new BuildVersionServices(context);
                });

            services.AddTransient<RegionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new RegionServices(context);
            });
            services.AddTransient<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new ProductServices(context);
            });
            services.AddTransient<ShipmentServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new ShipmentServices(context);
            });
            services.AddTransient<ShipperServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new ShipperServices(context);
            });
            services.AddTransient<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new CategoryServices(context);
            });
            services.AddTransient<SupplierServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new SupplierServices(context);
            });
        }
    }
}

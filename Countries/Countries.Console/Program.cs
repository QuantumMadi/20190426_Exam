using Countries.Models;
using Countries.TableDataService.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            City country = new City("Astana", 1);
        
            ContextServices<City> contextServices = new ContextServices<City>();
            ContextServices<Street> contextServices2 = new ContextServices<Street>();
           
            List<object> citites =  contextServices.GetAll();
            City city = citites[0] as City;
            Street street = new Street("Furmanova", city.Id, 100);
            contextServices2.Add(street);
            
        }
    }
}

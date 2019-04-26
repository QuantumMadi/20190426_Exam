using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int HouseQuantity { get; set; }
        public Street()
        {

        }
        public Street(string name, int cityId, int houseQuant)
        {
            Name = name;
            CityId = cityId;
            HouseQuantity = houseQuant;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smileRed25.Domain
{
    public class Product
    {    
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }  
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal VAT { get; set; }
        public string Image { get; set; }
        public double Stock { get; set; }
        public bool IsActive { get; set; }    
        public string Remarks { get; set; }
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "";
                }

                string[] breakp = Image.Split('/');

                return string.Format(

                    "http://orion.somee.com/" + breakp[1] + "/" + breakp[2]
                     + "/" + breakp[3]);

            }
        }
    }
}



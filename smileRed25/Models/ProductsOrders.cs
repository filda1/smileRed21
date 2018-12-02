using smileRed25.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smileRed25.Models
{
    public class ProductsOrders: Product
    {
        public int OrderID { get; set; }
        public int OrderDetailsID { get; set; }
        public string OrderStatusName { get; set; }
        public int Count { get; set; }
        public DateTime DateOrder { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public int Code { get; set; }
        public int Door { get; set; }
        public float Quantity { get; set; }
        public decimal Value { get; set; }
        public string Ingredients { get; set; }

        #region Formats
        /// </summary>
        public string PriceFormat
        {          
            get
            {
                var price = Price.ToString();
                var StringPrice = price.Replace('.', ',');

                return StringPrice +" "+ "€";
            }

        }

        public string VATFormat
        {
            get
            {
                var iva =VAT.ToString();
                var StringIVA = iva + "%";

                return StringIVA;
            }

        }

        public string PriceQuantityFormat
        {
            get
            {
                var price = PriceQuantity.ToString();
                var StringPrice = price.Replace('.', ',');

                return StringPrice + " " + "€";
            }
        }

        public string PriceVATQuantityiceFormat
        {
            get
            {
                var price = PriceVATQuantity.ToString();
                var StringPrice = price.Replace('.', ',');

                return StringPrice + " " + "€";
            }
        }

        public int RestOrder
        {
            get
            {
                return OrderID-1;
            }

            set { }

        }
        #endregion

        #region Calculated fields 
        /// <summary>
        /// Precio  + Cantidad
        /// </summary>
        public decimal PriceQuantity
        {
            get
            {
                return Price * (decimal)Quantity;
            }

        }

        /// <summary>
        ///  IVA 
        /// </summary>
        public decimal PriceVAT
        {
            get
            {
                return Price + (Price * (VAT / 100));
            }

        }

        /// <summary>
        /// Precio  + IVA * Cantidad
        /// </summary>
        public decimal PriceVATQuantity
        {
            get
            {
                return PriceVAT * (decimal)Quantity;
            }

        }
        #endregion

     
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

    }
}
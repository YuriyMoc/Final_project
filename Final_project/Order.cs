using System.Collections;

namespace AutoServiceApp
{
    public class Order
    {
        public ArrayList SelectedServices { get; set; } = new ArrayList();

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (Service s in SelectedServices)
                {
                    total += s.Price;
                }
                return total;
            }
        }
    }
}

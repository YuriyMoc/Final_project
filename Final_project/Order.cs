using System.Collections;

namespace AutoServiceApp
{
    public class Order // Клас замовлення зі списком 
    {
        public ArrayList SelectedServices { get; set; } = new ArrayList(); // Список послуг
        public decimal TotalPrice
        {
            get // обчислення вартості
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

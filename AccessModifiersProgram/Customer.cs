using System;

namespace AccessModifiersProgram
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Promote()
        {
            var rating = CalculateRating(excludeOrders: true);
            if(rating==0)
                Console.WriteLine("promoted to Level 1");
            else
                Console.WriteLine("promoted to Level 2");
        }
      //  public int CalculateRating(bool excludeOrders)
      //  private int CalculateRating(bool excludeOrders)
        protected int CalculateRating(bool excludeOrders)
        {
            return 0;
        }
    }
}

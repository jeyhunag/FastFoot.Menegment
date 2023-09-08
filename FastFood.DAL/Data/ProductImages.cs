namespace FastFood.DAL.Data
{
    public class ProductImages:BaseEntity
    {
        public string ImagePath { get; set; }
        public int FoodId { get; set; }
        public Foods Foods { get; set; }

        public bool IsMain { get; set; }
    }
}

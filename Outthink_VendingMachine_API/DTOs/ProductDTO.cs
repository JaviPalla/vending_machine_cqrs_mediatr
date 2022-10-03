namespace Outthink_VendingMachine_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Price}EUR ({Quantity} unit/s)";
        }
    }
}

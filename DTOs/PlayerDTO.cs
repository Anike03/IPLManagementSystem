namespace IPLManagementSystem.DTOs
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Role { get; set; } = string.Empty;
        public int TeamId { get; set; }
    }
}
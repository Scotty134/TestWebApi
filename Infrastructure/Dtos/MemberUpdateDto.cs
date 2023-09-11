namespace Infrastructure.Dtos
{
    public class MemberUpdateDto
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string? Introduction { get; set; }
        public DateTime? LastActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.DTOs
{
    public class CreateBloodRequestDto
    {
        [Required]
        public string BloodGroup { get; set; } = null!;

        [Required]
        public int Bags { get; set; }

        [Required]
        public DateTime NeededDate { get; set; }

        public string? ExtraContact { get; set; }
        public string? Reason { get; set; }
    }
}

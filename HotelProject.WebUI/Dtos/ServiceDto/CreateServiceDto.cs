using System.ComponentModel.DataAnnotations;

namespace HotelProject.WebUI.Dtos.ServiceDto
{
    public class CreateServiceDto
    {
        [Required(ErrorMessage ="hizmet ikon linki giriniz")]
        public string? ServiceIcon { get; set; }

        [Required(ErrorMessage = "hizmet ikon linki giriniz")]
        [StringLength(100,ErrorMessage ="wn fazla 100 karakter gir")]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}

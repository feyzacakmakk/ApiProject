using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DtoLayer.Dtos.RoomDto
{
    public class UpdateRoomDto
    {
        public int RoomID { get; set; }

        [Required(ErrorMessage = "Lütfen oda numarasını girinz")]
        public string? RoomNumber { get; set; }

        [Required(ErrorMessage = "Lütfen oda fotosunu girinz")]
        public string? RoomCoverImage { get; set; }

        [Required(ErrorMessage = "Lütfen fiyat bilgisi giriniz")]
        public int Price { get; set; }

        [Required(ErrorMessage = "lütfen oda başlığı giriniz")]
        [StringLength(100,ErrorMessage ="lütfen en fazla 100 karakter girin")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "lütfen yatak sayısı giriniz")]
        public string? BedCount { get; set; }

        [Required(ErrorMessage = "lütfen banyo sayısı giriniz")]
        public string? BathCount { get; set; }

        public string? Wifi { get; set; }

        [Required(ErrorMessage = "Lütfen açıklamayı girinz")]
        public string? Description { get; set; }
    }
}

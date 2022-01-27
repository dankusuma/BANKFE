using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BANKFE.Models.ChangeData
{
    public class ChangeAddress
    {
        public string TOKEN { get; set; }

        [Display(Name = "Username")]
        public string USERNAME { get; set; }

        [Display(Name = "Alamat")]
        [Required(ErrorMessage = "Alamat tidak boleh kosong")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Panjang tidak boleh lebih dari 200 karakter")]
        public string ADDRESS { get; set; }

        [Display(Name = "Provinsi")]
        [Required(ErrorMessage = "Provinsi tidak boleh kosong")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Panjang tidak boleh lebih dari 50 karakter")]
        public string PROVINCE { get; set; }

        [Display(Name = "Kabupaten/Kota")]
        [Required(ErrorMessage = "Kabupaten tidak boleh kosong")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Panjang tidak boleh lebih dari 50 karakter")]
        public string KABUPATEN_KOTA { get; set; }

        [Display(Name = "Kecamatan")]
        [Required(ErrorMessage = "Kecamatan tidak boleh kosong")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Panjang tidak boleh lebih dari 50 karakter")]
        public string KECAMATAN { get; set; }

        [Display(Name = "Kelurahan")]
        [Required(ErrorMessage = "Kelurahan tidak boleh kosong")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Panjang tidak boleh lebih dari 50 karakter")]
        public string KELURAHAN { get; set; }
    }
}

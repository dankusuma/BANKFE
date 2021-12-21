using System;

namespace BANKFE.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string Nik { get; set; }
        public string Nama { get; set; }
        public string TempatLahir { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string NamaIbu { get; set; }
        public string Alamat { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string Kabupaten { get; set; }
        public string Provinsi { get; set; }
        public string Kelamin { get; set; }
        public string StatusPernikahan { get; set; }
        public string Pekerjaan { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Telepon { get; set; }
    }
}

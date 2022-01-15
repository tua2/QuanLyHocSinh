using System;

namespace DTO
{
    public class HocSinhDTO
    {
        private string maHocSinh;
        private string hoTen;
        bool gioiTinh;
        private DateTime ngaySinh; 
        private string diaChi; 
      

        public HocSinhDTO() { }
        public HocSinhDTO(
            string maHocSinh, 
            string hoTen, 
            bool gioiTinh, 
            DateTime ngaySinh, 
            string diaChi
           )
        {
            this.maHocSinh = maHocSinh;
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.diaChi = diaChi;
          
        }

        public string MaHocSinh { get => maHocSinh; set => maHocSinh = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public bool GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        
    }
}

using System;
using System.Collections.Generic;

namespace Invoice
{
    public abstract class Pegawai : IPayable
    {
        public string NamaDepan { get; }
        public string NamaBelakang { get; }
        public string SocialSecurityNumber { get; }
        public Pegawai(string namaDepan, string namaBelakang, string socialSecurityNumber)
        {
            NamaDepan = namaDepan;
            NamaBelakang = namaBelakang;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public override string ToString() => $"{NamaDepan} {NamaBelakang}\n" + $"social security number: {SocialSecurityNumber}";
        public abstract decimal Pendapatan();
        public decimal JumlahPembayaranDidapat() => Pendapatan();

    }

    public class PegawaiGajian : Pegawai

    {
        private decimal gajiMingguan;


        public PegawaiGajian(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal gajiMingguan)
        : base(namaDepan, namaBelakang, socialSecurityNumber)
        {
            GajiMingguan = gajiMingguan;
        }


        public decimal GajiMingguan
        {
            get
            {
                return gajiMingguan;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                     value, $"{nameof(GajiMingguan)} must be >= 0");
                }

                gajiMingguan = value;
            }
        }
        public override decimal Pendapatan() => GajiMingguan;

        public override string ToString() =>
        $"Pegawai Gajian: {base.ToString()}\n" + $"Gaji Mingguan: {GajiMingguan:C}";
    }


    public class PegawaiPerjam : Pegawai
    {

        private decimal upah;
        private decimal jam;


        public PegawaiPerjam(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal gajiperjam, decimal jumlahjamkerja)
       : base(namaDepan, namaBelakang, socialSecurityNumber)

        {
            Upah = gajiperjam;
            Jam = jumlahjamkerja;
        }


        public decimal Upah
        {
            get
            {
                return upah;
            }
            set
            {

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(Upah)} must be >= 0");
                }

                upah = value;
            }
        }


        public decimal Jam
        {
            get
            {
                return jam;
            }
            set
            {
                if (value < 0 || value > 168)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(Jam)} must be >= 0 and <= 168");
                }

                jam = value;
            }
        }
        public override decimal Pendapatan()
        {
            if (Jam <= 40)
            {
                return Upah * Jam;
            }
            else
            {
                return (40 * Upah) + ((Jam - 40) * Upah * 1.5M);
            }
        }


        public override string ToString() =>
        $"Pegawai dengan gaji per jam: {base.ToString()}\n" +
        $"upah per jam : {Upah:C}\njumlah jam kerja: {Jam:F2}";
    }

    public class Pegawaidengankomisi : Pegawai
    {
        private decimal penjualanKotor;
        private decimal tingkatKomisi;


        public Pegawaidengankomisi(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal penjualanKotor, decimal tingkatKomisi) : base(namaDepan, namaBelakang, socialSecurityNumber)
        {
            PenjualanKotor = penjualanKotor;
            TingkatKomisi = tingkatKomisi;
        }


        public decimal PenjualanKotor
        {
            get
            {
                return penjualanKotor;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(PenjualanKotor)} must be >= 0");
                }
                penjualanKotor = value;
            }
        }
        public decimal TingkatKomisi
        {
            get
            {
                return tingkatKomisi;
            }
            set
            {

                if (value <= 0 || value >= 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(TingkatKomisi)} must be > 0 and < 1");
                }

                tingkatKomisi = value;
            }
        }

        public override decimal Pendapatan() => TingkatKomisi * PenjualanKotor;


        public override string ToString() => $"komisi pegawai: {base.ToString()}\n" + $"penjualan kotor: {PenjualanKotor:C}\n" + $"tingkat komisi: {TingkatKomisi:F2}";
    }


    public interface IPayable
    {
        decimal JumlahPembayaranDidapat();
    }

    public class Faktur : IPayable
    {
        public string No_Barang { get; }
        public string DeskripsiBarang { get; }
        private int kuantitas;
        private decimal hargaperItem;


        public Faktur(string no_Barang, string deskripsiBarang, int kuantitas, decimal hargaperItem)
        {
            No_Barang = no_Barang;
            DeskripsiBarang = deskripsiBarang;
            Kuantitas = kuantitas;
            HargaPerItem = hargaperItem;
        }
        public int Kuantitas
        {
            get
            {
                return kuantitas;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(Kuantitas)} must be >= 0");
                }

                kuantitas = value;
            }
        }


        public decimal HargaPerItem
        {
            get
            {
                return hargaperItem;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(HargaPerItem)} must be >= 0");
                }

                hargaperItem = value;
            }
        }


        public override string ToString() =>
        $"Faktur:\nNo Barang: {No_Barang} ({DeskripsiBarang})\n" +
        $"kuantitas: {Kuantitas}\nharga per item: {HargaPerItem:C}";
        public decimal JumlahPembayaranDidapat() => Kuantitas * HargaPerItem;
    }

    class PayableInterfaceTest
    {
        static void Main()
        {
            var payableObjects = new List<IPayable>() {
                new Faktur("01234", "seat", 2, 375.00M),
                new Faktur("56789", "tire", 4, 79.95M),
                new PegawaiGajian("John", "Smith", "111-11-1111", 800.00M),
                new PegawaiGajian("Lisa", "Barnes", "888-88-8888", 1200.00M)};
            Console.WriteLine("Faktur dan Pegawai yang diproses secara polymorpishm:\n");

            foreach (var payable in payableObjects)
            {
                Console.WriteLine($"{payable }");
                Console.WriteLine($"Jumlah Tagihan: { payable.JumlahPembayaranDidapat():C}\n");
            }
        }
    }

}




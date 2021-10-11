using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_pegawai
{
    public class KomisiPegawai
    {
        public string NamaDepan { get; }
        public string NamaBelakang { get; }
        public string SocialSecurityNumber { get; }
        private decimal penjualanKotor;
        private decimal tingkatKomisi;

        public KomisiPegawai(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal penjualanKotor, decimal tingkatKomisi)
        {


            NamaDepan = namaDepan;
            NamaBelakang = namaBelakang;
            SocialSecurityNumber = socialSecurityNumber;
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
                if (value < 0 )
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


        public virtual decimal Pendapatan() => tingkatKomisi * penjualanKotor;


        public override string ToString() =>
        $"komisi pegawai: {NamaDepan} {NamaBelakang}\n" +
        $"social security number: {SocialSecurityNumber}\n" +
        $"penjualan kotor: {penjualanKotor:C}\n" +
        $"tingkat komisi: {tingkatKomisi:F2}";
    }
    public class Gajipluskomisipegawai : KomisiPegawai
    {

        private decimal gajipokok;


        public Gajipluskomisipegawai(string namaDepan, string namaBelakang,
        string socialSecurityNumber, decimal penjualanKotor,
        decimal tingkatKomisi, decimal gajiPokok)
        : base(namaDepan, namaBelakang, socialSecurityNumber,
        penjualanKotor, tingkatKomisi)
        {
            GajiPokok = gajiPokok;
        }


        public decimal GajiPokok
        {
            get
            {
                return gajipokok;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                      value, $"{nameof(GajiPokok)} must be >= 0");
                }

                gajipokok = value;
            }
        }


        public override decimal Pendapatan() => GajiPokok + base.Pendapatan();


        public override string ToString() =>
        $"base-salaried {base.ToString() }\ngaji pokok: {GajiPokok:C}";
    }

    class BasePlusCommissionEmployeeTest
    {
        static void Main()
        {
            // instantiate BasePlusCommissionEmployee object
            var pegawai = new Gajipluskomisipegawai("Bob", "Lewis",
            "333-33-3333", 5000.00M, .04M, 300.00M);
            // display BasePlusCommissionEmployee's data
            Console.WriteLine(
            "Informasi pegawai yang didapatkan dari properties dan method : \n");
            Console.WriteLine($"Nama Depan : {pegawai.NamaDepan }");
            Console.WriteLine($"Nama Belakang {pegawai.NamaBelakang }");
            Console.WriteLine(
            $"Social security number : {pegawai.SocialSecurityNumber }");
            Console.WriteLine($"Penjualan Kotor : {pegawai.PenjualanKotor:C}");
            Console.WriteLine(
            $"Tingkat Komisi is {pegawai.TingkatKomisi:F2}");
            Console.WriteLine($"Pendapatan : {pegawai.Pendapatan():C}");
            Console.WriteLine($"Gaji Pokok {pegawai.GajiPokok:C}");

            pegawai.GajiPokok = 1000.00M; // set base salary

            Console.WriteLine("\nUpdate informasi pegawai oleh ToString:\n");
            Console.WriteLine();
            Console.WriteLine($"Pendapatan : {pegawai.Pendapatan():C}");
            Console.ReadLine();
        }

    }
}
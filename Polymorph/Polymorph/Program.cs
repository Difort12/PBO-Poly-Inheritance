using System;

namespace Polymorph
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
        $"Pegawai Dengan Gaji plus {base.ToString() }\ngaji pokok: {GajiPokok:C}";
    }

    class PolymorphismTest
    {
        static void Main()
        {
            var komisiPegawai = new KomisiPegawai("Sue", "Jones", "222-22-2222", 10000.00M, .06M);
            var gajiPlusKomisiPegawai = new Gajipluskomisipegawai("Bob", "Lewis", "333-33-3333", 5000.00M, .04M, 300.00M);

            Console.WriteLine("Memanggil Pegawai dengan komisi ke ToSting " + "dengan base class yang mereferensikan base class object\n");
            Console.WriteLine(komisiPegawai.ToString());
            Console.WriteLine($"Pendapatan: {komisiPegawai.Pendapatan()}\n");


            Console.WriteLine("Memanggil Pegawai dengan Gaji plus Komisi ke ToString dan" + " Method Pendapatan dengan class turunan yang mereferensikan" + " objek class turunan\n");
            Console.WriteLine(gajiPlusKomisiPegawai.ToString());
            Console.WriteLine($"Pendapatan: {gajiPlusKomisiPegawai.Pendapatan()}\n");

            Console.WriteLine("Memanggil Pegawai dengan Gaji plus Komisi ke ToString dan " + " Method Pendapatan dengan base class yang mereferensikan objek class turunan");
            KomisiPegawai komisiPegawai2 = gajiPlusKomisiPegawai;

            Console.WriteLine(komisiPegawai2.ToString());
            Console.WriteLine($"Pendapatan: {gajiPlusKomisiPegawai.Pendapatan() }\n");
        }
    }
}

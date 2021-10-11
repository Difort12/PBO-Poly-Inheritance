using System;
using System.Collections.Generic;

namespace Payroll_System
{
    public abstract class Pegawai 
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
        

    }

    public class PegawaiDenganGaji : Pegawai

    {
        private decimal gajiMingguan;


        public PegawaiDenganGaji(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal gajiMingguan)
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
        $"Pegawai dengan Gaji: {base.ToString()}\n" + $"Gaji Mingguan: {GajiMingguan:C}";
    }


    public class PegawaiPerJam : Pegawai
    {

        private decimal upah;
        private decimal jam;


        public PegawaiPerJam(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal upahperJam, decimal jumlahjamKerja)
       : base(namaDepan, namaBelakang, socialSecurityNumber)

        {
            Upah = upahperJam;
            Jam = jumlahjamKerja;
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
        $"Pegawai per Jam: {base.ToString()}\n" +
        $"Upah per jam: {Upah:C}\nJumlah Jam Kerja: {Jam:F2}";
    }

    public class PegawaiDenganKomisi : Pegawai
    {
        private decimal penjualanKotor;
        private decimal tingkatKomisi;


        public PegawaiDenganKomisi(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal penjualanKotor, decimal tingkatKomisi) : base(namaDepan, namaBelakang, socialSecurityNumber)
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


        public override string ToString() => $"Pegawai dengan Komisi: {base.ToString()}\n" + $"Penjualan kotor: {PenjualanKotor:C}\n" + $"tingkat komisi: {TingkatKomisi:F2}";
    }
    public class PegawaiDenganGajidanKomisi : PegawaiDenganKomisi
    {
        private decimal gajiPokok; 
        public PegawaiDenganGajidanKomisi(string namaDepan, string namaBelakang, string socialSecurityNumber, decimal penjualanKotor, decimal tingkatKomisi, decimal gajiPokok)
            : base(namaDepan, namaBelakang, socialSecurityNumber, penjualanKotor, tingkatKomisi)
        {
            GajiPokok = gajiPokok;
        }

        public decimal GajiPokok
        {
            get
            {
                return gajiPokok;
            }
            set
            {
                if (value < 0) 
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(GajiPokok)} must be >= 0");
                }
                gajiPokok = value;
            }
        }
        public override decimal Pendapatan() => GajiPokok + base.Pendapatan();
       
        public override string ToString() => $"Pegawai dengan Gaji {base.ToString()}\nGaji Pokok: {GajiPokok:C}";

        static void Main()
        {
            var pegawaiDenganGaji = new PegawaiDenganGaji("John", "Smith", "111-11-1111", 800.00M);
            var pegawaiperJam = new PegawaiPerJam("Karen", "Price", "222-22-2222", 16.75M, 40.0M);
            var pegawaidenganKomisi = new PegawaiDenganKomisi("Sue", "Jones", "333-33-3333", 10000.00M, .06M);
            var pegawaidenagnGajidankomisi = new PegawaiDenganGajidanKomisi("Bob", "Lewis", "444-44-4444", 5000.00M, .04M, 300.00M);


            Console.WriteLine("Pegawai yang diproses secara individu:\n");
            Console.WriteLine($"{pegawaiDenganGaji}\nPendapatan: " + $"{pegawaiDenganGaji.Pendapatan():C}\n");
            Console.WriteLine($"{pegawaiperJam}\nPendapatan: {pegawaiperJam.Pendapatan():C}\n");
            Console.WriteLine($"{pegawaidenganKomisi}\nPendapatan: " + $"{pegawaidenganKomisi.Pendapatan():C}\n");
            Console.WriteLine($"{pegawaidenagnGajidankomisi}\nPendapatan: " + $"{pegawaidenagnGajidankomisi.Pendapatan():C}\n");
            Console.WriteLine("Pegawai yang diproses secara Polymorphism:\n");

            var Pegawai = new List<Pegawai>() { pegawaiDenganGaji, pegawaiperJam, pegawaidenganKomisi, pegawaidenagnGajidankomisi };
            
            foreach (var PegawaiSekarang in Pegawai)
            {
                Console.WriteLine(PegawaiSekarang); 
                                  
                if (PegawaiSekarang is PegawaiDenganGajidanKomisi)
                {
                    var employee = (PegawaiDenganGajidanKomisi)PegawaiSekarang;
                    employee.GajiPokok *= 1.10M;
                    Console.WriteLine("Jumlah Gaji pokok setelah dinaikkan 10%: " +
                    $"{employee.GajiPokok:C}");
                }
                Console.WriteLine($"Gaji yang Didapat: { PegawaiSekarang.Pendapatan():C}\n");
            }
            for (int j = 0; j < Pegawai.Count; j++)
            {
                Console.WriteLine($"Pegawai {j} adalah tipe {Pegawai[j].GetType() }");
            }
            Console.ReadLine();
        }

    }

}
    


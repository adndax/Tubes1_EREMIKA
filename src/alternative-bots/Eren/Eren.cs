using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// ------------------------------------------------------------------
// Eren Bot
// ------------------------------------------------------------------
// Eren bot made for Robocode by EREMIKA.
// Ported to Robocode Tank Royale by EREMIKA.
//
// Bot dengan pola gerak acak dan penanganan dinding yang canggih.
// Bot ini dapat bergerak dalam pola melingkar dan kemudian maju secara acak
// untuk menghindari pola tetap, serta memiliki penanganan untuk tabrakan
// dengan bot lain dan peluru.
// ------------------------------------------------------------------

public class Eren : Bot
{
    double arenaWidth, arenaHeight;         // Ukuran arena
    private Random random = new Random();   // Generator angka acak
    private int moveCounter = 0;            // Hitung jumlah gerakan acak

    //Main method untuk memulai bot
    static void Main()
    {
        new Eren().Start();
    }

    //Konstruktor dengan inisialisasi menggunakan file konfigurasi JSON
    Eren() : base(BotInfo.FromFile("Eren.json")) { }

    // Fungsi utama yang dieksekusi saat bot dijalankan
    public override void Run()
    {
        // Mengatur warna bot
        BodyColor = Color.Black;
        TurretColor = Color.Black;
        RadarColor = Color.White;
        BulletColor = Color.Green;
        ScanColor = Color.Green;

        // Mendapatkan ukuran arena
        arenaWidth = ArenaWidth;
        arenaHeight = ArenaHeight;

        // Mengulangan fungsi MoveInCircleRandom selama bot masih berjalan 
        while (IsRunning)
        {
            MoveInCircleRandom();
        }
    }

    // Fungsi untuk bergerak dalam pola melingkar dengan variasi acak
    private void MoveInCircleRandom()
    {
        // Randomisasi arah putaran acak (kiri/kanan) dan besar sudut putaran
        int turnDirection = random.Next(0, 2) == 0 ? 1 : -1;  // 1 = Left, -1 = Right
        int spinAngle = random.Next(5, 45);

        // Putar arah dalam jumlah yang besar
        SetTurnLeft(turnDirection * 10000);
        MaxSpeed = random.Next(5, 7); // Kecepatan random untuk variasi

        // Menentukan jarak untuk maju dengan mempertimbangkan posisi terhadap dinding
        double moveDistance = 500;
        moveDistance = CheckWallSmoothing(moveDistance);
        Forward(moveDistance); 

        // Setiap 5 kali menjalankan fungsi MoveInCircleRandom dilakukan perubahan posisi secara acak
        moveCounter++;
        if (moveCounter % 5 == 0)
        {
            RandomMove();
        }
    }

    // Fungsi untuk bergerak secara acak 
    private void RandomMove()
    {
        int randomAngle = random.Next(0, 360);      // Putar arah secara acak
        double moveDistance = 200;                  // Bergerak maju jarak acak

        // Putar ke arah acak dan memeriksa jarak dari dinding kemudian maju sesuai kondisi
        TurnRight(randomAngle);
        moveDistance = CheckWallSmoothing(moveDistance);
        Forward(moveDistance);
    }

    // Fungsi untuk menghhindari tabrakan dengan dinding
    private double CheckWallSmoothing(double distance)
    {
        double x = X;
        double y = Y;
        arenaWidth = ArenaWidth;
        arenaHeight = ArenaHeight;
        
        double buffer = 50;  // Jarak minimum dari dinding
        double safeDistance = distance;

        // Cek jarak aman dari dinding kiri
        if (x < buffer)
        {
            safeDistance = Math.Min(safeDistance, x - 20);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
            Forward(50);
        }
        // Cek jarak aman dari dinding kanan
        if (x > arenaWidth - buffer)
        {
            safeDistance = Math.Min(safeDistance, arenaWidth - x - 20);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
            Forward(50);
        }
        // Cek jarak aman dari dinding bawah
        if (y < buffer)
        {
            safeDistance = Math.Min(safeDistance, y - 20);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
            Forward(50);
        }
        // Cek jarak aman dari dinding atas
        if (y > arenaHeight - buffer)
        {
            safeDistance = Math.Min(safeDistance, arenaHeight - y - 20);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
            Forward(50);
        }

        // Jika jarak aman terlalu kecil, batasi antara 20-40
        if (safeDistance < 20)
        {
            Back(20)
            TurnRight(50);  // Ubah sudut agar menjauhi dinding
            Forward(50);
        }

        return safeDistance;
    }

    // Menembak ketika mendeteksi bot lain berdasarkan jarak terhadap lawan
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firepower = Math.Max(1, 3 - (distance / 400));
        SetFire(firepower);
    }

    // Menghindar ketika terkena tembakan
    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Jika ditembak, ubah arah untuk menghindari pola tetap
        TurnRight(90);
        double moveDistance = 100;
        moveDistance = CheckWallSmoothing(moveDistance);
        Forward(moveDistance);
    }

    // Menghindar ketika terkena tembakan
    public override void OnHitBot(HitBotEvent e)
    {    
        SetFire(3); // Tembak dengan kekuatan maksimal
        Back(20); // Mundur sedikit untuk menghindari tabrakan terus-menerus
        TurnRight(45);
    }

    // Menangani tabrakan dengan dinding
    public override void OnHitWall(HitWallEvent e)
    {
        Back(30); // Mundur sedikit
        TurnRight(45); // Ubah arah agar tidak terjebak
    }
}

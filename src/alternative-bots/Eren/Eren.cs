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
    int turnDirection = 1;

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
        CheckWallSmoothing();
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
        CheckWallSmoothing();
        Forward(moveDistance);
    }

    // Fungsi untuk menghhindari tabrakan dengan dinding
    private void CheckWallSmoothing()
    {
        double x = X;
        double y = Y;
        arenaWidth = ArenaWidth;
        arenaHeight = ArenaHeight;
        
        double buffer = 10;  // Jarak minimum dari dinding

        // Cek jarak aman dari dinding kiri
        if (x < buffer)
        {
            Back(50);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
        }
        // Cek jarak aman dari dinding kanan
        else if (x > arenaWidth - buffer)
        {
            Back(50);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
            //Forward(50);
        }
        // Cek jarak aman dari dinding bawah
        else if (y < buffer)
        {
            Back(50);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
        }
        // Cek jarak aman dari dinding atas
        else if (y > arenaHeight - buffer)
        {
            Back(50);
            TurnRight(45);  // Ubah sudut agar menjauhi dinding
        }
    }

    // Menembak ketika mendeteksi bot lain berdasarkan jarak terhadap lawan
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firepower = Math.Max(1, 3 - (distance / 400));
        SetFire(firepower);
    }

    // Menyerang dan menjauh ketika tertabrak bot 
    public override void OnHitBot(HitBotEvent e)
    {    
        TurnToFaceTarget(e.X, e.Y); //Mengarahkan arah tembakan ke lawan
        Fire(3); // Tembak dengan kekuatan maksimal
        Back(30); // Mundur sedikit untuk menghindari tabrakan terus-menerus
        TurnRight(45);
        Forward(30);   
    }
    
    // Menangani tabrakan dengan dinding
    public override void OnHitWall(HitWallEvent e)
    {
        Back(50); // Mundur sedikit
        TurnRight(90); // Ubah arah agar tidak terjebak
        Forward(100); // Maju
    }

    // Mengarahkan arah tembakan ke lawan
    public void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        if (bearing >= 0)
            turnDirection = 1;
        else
            turnDirection = -1;

        TurnLeft(bearing);
    }
}

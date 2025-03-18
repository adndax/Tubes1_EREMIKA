using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Eren : Bot
{
    double arenaWidth, arenaHeight;
    double sideLength;
    int step = 0;

    static void Main()
    {
        new Eren().Start();
    }

    private Random random = new Random();
    private int moveCounter = 0;

    Eren() : base(BotInfo.FromFile("Eren.json")) { }

    public override void Run()
    {
        // Atur warna bot
        BodyColor = Color.Black;
        TurretColor = Color.Black;
        RadarColor = Color.Orange;
        BulletColor = Color.Red;
        ScanColor = Color.Red;

        // Dapatkan ukuran arena
        arenaWidth = ArenaWidth;
        arenaHeight = ArenaHeight;

        // Tentukan panjang sisi persegi (menggunakan 80% dari ukuran terkecil arena)
        sideLength = Math.Min(arenaWidth, arenaHeight) * 0.7;


        while (IsRunning)
        {
            MoveInSquare();
        }
    }

    private void MoveInSquare()
    {
        // Randomize spin direction and angle
            int turnDirection = random.Next(0, 2) == 0 ? 1 : -1;  // 1 = Left, -1 = Right
            int spinAngle = random.Next(5, 45);

            // Set random spin movement
            SetTurnLeft(turnDirection * 10000);
            MaxSpeed = random.Next(5, 7); // Kecepatan random untuk variasi
            Forward(500);

            moveCounter++;

            // Setiap beberapa gerakan, pindah posisi secara acak
            if (moveCounter % 5 == 0)
            {
                RandomMove();
            }
    }

    private void RandomMove()
    {
        int randomAngle = random.Next(0, 360);      // Putar arah secara acak
        double randomDistance = 300; // Bergerak maju jarak acak

        TurnRight(randomAngle);
        Forward(randomDistance);

        // Tambahkan variasi putar balik setelah berpindah posisi
        //TurnRight(random.Next(45, 90));
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firepower = Math.Max(1, 3 - (distance / 400));
        SetFire(firepower);
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Jika ditembak, ubah arah untuk menghindari pola tetap
        TurnRight(90);
        Forward(50);
    }

    public override void OnHitBot(HitBotEvent e)
    {    
        SetFire(3); // Tembak dengan kekuatan maksimal
        Back(20); // Mundur sedikit untuk menghindari tabrakan terus-menerus
        TurnRight(45);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(30); // Mundur sedikit
        TurnRight(45); // Ubah arah agar tidak terjebak
    }
}

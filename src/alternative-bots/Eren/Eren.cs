using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Eren : Bot
{
    double sideLength;

    static void Main()
    {
        new Eren().Start();
    }

    Eren() : base(BotInfo.FromFile("Eren.json")) { }

    public override void Run()
    {
        // Atur warna bot
        BodyColor = Color.Black;
        TurretColor = Color.Black;
        RadarColor = Color.Orange;
        BulletColor = Color.lime;
        ScanColor = Color.lime;

        // Tentukan panjang sisi persegi (menggunakan 70% dari ukuran terkecil arena)
        sideLength = Math.Min(ArenaWidth,ArenaHeight) * 0.7;


        while (IsRunning)
        {
            MoveInSquare();
        }
    }

    private void MoveInSquare()
    {
        // Gerakan dalam bentuk persegi
        Forward(sideLength);
        TurnRight(60); // Belok kanan 60 derajat
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
        Forward(100);
    }

    public override void OnHitBot(HitBotEvent e)
    {    
        SetFire(3); // Tembak dengan kekuatan maksimal
        Back(20); // Mundur sedikit untuk menghindari tabrakan terus-menerus
        TurnRight(45);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(20); // Mundur sedikit
        TurnRight(45); // Ubah arah agar tidak terjebak
    }
}

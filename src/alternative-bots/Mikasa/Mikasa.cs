using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// -------------------------------------------------------------------
// Mikasa
// -------------------------------------------------------------------
// Mikasa adalah bot tempur untuk Robocode Tank Royale dengan 
// strategi greedy. Bot ini bergerak secara memutar untuk menghindari
// tembakan musuh. Apabila lawan berada pada jarak yang cukup dekat,
// bot akan menabrak dan menembak lawan dengan kekuatan yang
// disesuaikan dengan energi musuh.
// -------------------------------------------------------------------

// class Mikasa sebagai subclass dari class Bot
public class Mikasa : Bot
{
    private Random random = new Random();
    private bool moveRight = true;
    private double moveDistance = 150;
    private int moveCount = 0;
    private int maxMoveCount = 5;

    static void Main()
    {
        new Mikasa().Start();
    }

    Mikasa() : base(BotInfo.FromFile("Mikasa.json")) { }

    public override void Run()
    {
        // Set warna
        BodyColor = Color.Red;
        TurretColor = Color.Black;
        RadarColor = Color.Black;
        ScanColor = Color.Black;
        BulletColor = Color.Red;

        // Pergi ke dinding terdekat
        MoveToWall();

        // Jalankan patrol dan scan terus menerus
        while (IsRunning)
        {
            if (moveRight)
                Forward(moveDistance + random.Next(50));
            else
                Back(moveDistance + random.Next(50));

            moveRight = !moveRight;
            moveCount++;

            if (moveCount >= maxMoveCount)
            {
                Forward(Math.Max(ArenaWidth, ArenaHeight));
                TurnRight(90);
                moveCount = 0;
            }
            // Lakukan scan 360 derajat secara terus menerus
            Scan360Degrees();
        }
    }

    private void MoveToWall()
    {
        double angleToWall = Direction % 90;
        TurnRight(angleToWall);
        Forward(Math.Max(ArenaWidth, ArenaHeight)); // pergi ke dinding terdekat

        // posisikan meriam agar siap menembak
        TurnGunRight(90);
        TurnRight(90);
    }

    private void Scan360Degrees()
    {
        TurnGunRight(360);
    }


    public override void OnHitBot(HitBotEvent e)
    {
        var bearing = BearingTo(e.X, e.Y);
        TurnLeft(bearing);
        double distance = DistanceTo(e.X, e.Y);
        Back(100 + random.Next(50));
        Fire(CalculateFirePower(distance));
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        Fire(CalculateFirePower(distance));

        if (distance < 150 && Energy > 50) 
        {
            Fire(3);
            Fire(3);
        }
        Rescan();
    }

    private double CalculateFirePower(double distance)
    {
        if (Energy < 20) // tembak dengan kekuatan rendah jika energi rendah
            return 1;
        if (distance < 200) // tembak dengan kekuatan maks utk jarak dekat
            return 3; 
        if (distance < 500) // tembak dengan kekuatan sedang utk jarak menengah
            return 2;
        return 1;
    }
}
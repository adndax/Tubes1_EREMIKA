using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Levi : Bot
{
    private Random random = new Random();
    private bool moveRight = true;
    private double moveDistance = 150;
    private int moveCount = 0;
    private int maxMoveCount = 5;

    static void Main()
    {
        new Levi().Start();
    }

    Levi() : base(BotInfo.FromFile("Levi.json")) { }

    public override void Run()
    {
        // Set warna
        BodyColor = Color.Black;
        TurretColor = Color.Black;
        RadarColor = Color.Orange;
        BulletColor = Color.Cyan;
        ScanColor = Color.Cyan;

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
            // Lakukan scan 180 derajat secara terus menerus
            Scan360Degrees();
        }
    }

    private void MoveToWall()
    {
        double angleToWall = Direction % 90;
        TurnRight(angleToWall);
        Forward(Math.Max(ArenaWidth, ArenaHeight));

        // Posisikan turret agar siap melakukan scan
        TurnGunRight(90);
        TurnRight(90);
    }

     private void MoveAlongWall()
    {
        double shiftDistance = 200 + random.Next(100);
        TurnRight(90);
        Forward(shiftDistance);
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
        if (Energy < 20)
            return 1;
        if (distance < 200)
            return 3;
        if (distance < 500)
            return 2;
        return 1;
    }
}
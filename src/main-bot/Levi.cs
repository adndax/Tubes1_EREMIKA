using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Levi : Bot
{
    // arah putaran bot, dengan default 1, yaitu putaran searah jarum jam
    int turnDirection = 1; 

    //  konstruktor untuk menginisialisasi bot dengan konfigurasi dan file Levi.json
    public Levi() : base(BotInfo.FromFile("Levi.json")) { }

    // main method untuk memulai bot
    static void Main(string[] args)
    {
        new Levi().Start();
    }

    // method utama yang dijalankan selama bot aktif
    public override void Run()
    {
        // warna 
        BodyColor = Color.Red;
        TurretColor = Color.Black;
        RadarColor = Color.Black;
        ScanColor = Color.Black;
        BulletColor = Color.Red;
        
        // loop pergerakan utama yang berjalan selama bot aktif
        while (IsRunning)
        {
            SetTurnLeft(10_000);
            MaxSpeed = 5;
            Forward(10_000);
        }
    }

    // method yang dijalankan ketika bot lain terdeteksi oleh radar
    public override void OnScannedBot(ScannedBotEvent e)
    {
        // menghitung jarak dengan bot yang terdeteksi
        double distance = DistanceTo(e.X, e.Y);
        
        // jika jarak kurang dari 100 unit, bot akan bergerak maju ke arah bot lawan
        if (distance < 100)
        {
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance + 5);
        }

        // jika jarak antara 100 dan 500 unit, bot akan menembak
        else if (distance < 500)
        {
            Fire(3);
        }
    }

    // method yang dijalankan ketika bot bertabrakan dengan bot lain
    public override void OnHitBot(HitBotEvent e)
    {
        // mengarahkan bot ke arah bot yang tertabrak
        TurnToFaceTarget(e.X, e.Y);
        
        // menembak berdasarkan energi bot yang tertabrak
        if (e.Energy > 16)
            Fire(3);
        else if (e.Energy > 10)
            Fire(2);
        else if (e.Energy > 4)
            Fire(1);
        else if (e.Energy > 2)
            Fire(.5);
    }

    // method untuk memutar bot agar menghadap ke posisi bot lawan
    private void TurnToFaceTarget(double x, double y)
    {
        // menghitung arah sudut untuk menghadap ke bot lawan
        var bearing = BearingTo(x, y);

        // menghitung arah putaran bot berdasarkan nilai bearing
        turnDirection = bearing >= 0 ? 1 : -1;

        // memutar bot berdasarkan arah putaran
        TurnLeft(bearing);
    }

}
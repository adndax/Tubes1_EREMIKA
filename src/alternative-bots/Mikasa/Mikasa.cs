using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// -------------------------------------------------------------------
// MikasaBot
// -------------------------------------------------------------------
// MikasaBot adalah bot tempur untuk Robocode Tank Royale dengan 
// strategi greedy. Bot ini bergerak secara memutar untuk menghindari
// tembakan musuh. Apabila lawan berada pada jarak yang cukup dekat,
// bot akan menabrak dan menembak lawan dengan kekuatan yang
// disesuaikan dengan energi musuh.
// -------------------------------------------------------------------

// class MikasaBot sebagai subclass dari class Bot
public class Mikasa : Bot
{
    // arah putaran bot, dengan default 1, yaitu putaran searah jarum jam
    int turnDirection = 1;

    // jumlah iterasi dalam mode evasive maneuver (penghindaran)
    int turnCounter = 0;

    //  konstruktor untuk menginisialisasi bot dengan konfigurasi dan file MikasaBot.json
    public Mikasa() : base(BotInfo.FromFile("Mikasa.json")) { }

    // main method untuk memulai bot
    static void Main(string[] args)
    {
        new Mikasa().Start();
    }

    // method utama yang dijalankan selama bot aktif
    public override void Run()
    {

        // memberi warna pada body, turret, dan radar bot
        BodyColor = Color.Red;
        TurretColor = Color.Cyan;
        RadarColor = Color.Lime;
        
        // loop pergerakan utama yang berjalan selama bot aktif
        while (IsRunning)
        {

            // jika energi kurang dari 30, bot melakukan penghindaran
            if (Energy < 30)
            {
                EvasiveManeuver();
            }

            // jika energi >= 30, bot akan terus bergerak maju secara memutar terus menerus
            else
            {
                SetTurnLeft(10_000);
                Forward(10_000);
                MaxSpeed = 4;
            }
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
        else if (e.Energy > .4)
            Fire(.1);
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

    // method untuk melakukan penghindaran ketika bot dalam energi rendah
    private void EvasiveManeuver()
    {

        // melakukan increment terhadap turnCounter ketika method dijalankan
        turnCounter++;

        // melakukan gerakan penghindaran berdasarkan jumlah iterasi dalam mode evasive maneuver (penghindaran)
        if (turnCounter % 64 == 0)
        {
            TurnRate = 0;  
            TargetSpeed = 4; 
        }
        if (turnCounter % 64 == 32)
        {
            TargetSpeed = -6; 
        }
        
        TurnRate = 5; 
        MaxSpeed = 3; 
        Forward(50);  
    }
}

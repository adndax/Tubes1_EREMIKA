using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Armin : Bot {
    static void Main(string[] args) => new Armin().Start();

    Armin() : base(BotInfo.FromFile("Armin.json")) { }

    public override void Run(){
        BodyColor = Color.FromArgb(0x79, 0x94, 0x97); // #799497
        GunColor = Color.FromArgb(0x32, 0x3A, 0x39); // #323A39
        TurretColor = Color.FromArgb(0xED, 0xD0, 0x83); // #EDD083
        RadarColor = Color.FromArgb(0x32, 0x3A, 0x39); // #323A39
        ScanColor = Color.Green;
        BulletColor = Color.Yellow;

        while (IsRunning){
            SetTurnLeft(10_000); // move in an oval pattern
            MaxSpeed = 8; 
            Forward(5_000);
        }
    }

    // shoot at enemy when found by radar
    public override void OnScannedBot(ScannedBotEvent e){
        Fire(3); // shoot at enemy with max power

        // randomly turn gun left or right
        if (Random.Shared.Next(2) == 0){ 
            SetTurnGunLeft(30);
        } else {
            SetTurnGunRight(30);
        }
        Rescan(); // shoot again as soon as possible
    }

    // move after being hit by a bullet
    public override void OnHitByBullet(HitByBulletEvent e){
        // randomly turn left or right
        if (Random.Shared.Next(2) == 0){
            TurnRight(Random.Shared.Next(60, 120)); 
        } else {
            TurnLeft(Random.Shared.Next(60, 120));
        }
        Forward(Random.Shared.Next(200, 400)); // move forward
    }

    // shoot at enemy when collided with another bot
    public override void OnHitBot(HitBotEvent e)
    {
        Fire(3); // shoot at enemy with max power
        if (e.IsRammed){ // armin ran into another bot
            Forward(150);
        } else { // armin rammed by another bot
            TurnLeft(45); // run away
            Forward(200);
        }
    }
}
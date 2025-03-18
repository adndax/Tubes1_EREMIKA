using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
//how biar kurangin hit the wall
public class ArminBot : Bot
{
    static void Main(string[] args) => new ArminBot().Start();

    ArminBot() : base(BotInfo.FromFile("ArminBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.FromArgb(0x79, 0x94, 0x97); // #799497
        GunColor = Color.FromArgb(0x32, 0x3A, 0x39); // #323A39
        TurretColor = Color.FromArgb(0xED, 0xD0, 0x83); // #EDD083
        RadarColor = Color.FromArgb(0x32, 0x3A, 0x39); // #323A39
        ScanColor = Color.Green;
        BulletColor = Color.Yellow;

        while (IsRunning)
        {
            SetTurnLeft(5_000);  
            MaxSpeed = 8;        
            Forward(5_000);      
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        Fire(3);              
        SetTurnGunLeft(15);   
        Rescan();             
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        TurnLeft(30);       
        Forward(100);       
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Fire(3);            
        if (e.IsRammed)     
        {
            TurnLeft(20);   
        }
    }
}

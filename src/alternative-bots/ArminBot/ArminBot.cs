using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class ArminBot : Bot
{
    static void Main(string[] args) => new ArminBot().Start();

    ArminBot() : base(BotInfo.FromFile("ArminBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Black;
        GunColor = Color.Red;
        TurretColor = Color.Red;
        RadarColor = Color.Blue;
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

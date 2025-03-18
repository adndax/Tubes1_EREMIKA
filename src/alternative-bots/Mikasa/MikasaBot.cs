using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class MikasaBot : Bot
{
    int turnDirection = 1; 
    int turnCounter = 0; 
    const double MaxDistanceToRam = 100;
    const double FireDistance = 500;
    const double LowHealthThreshold = 30; 

    public MikasaBot() : base(BotInfo.FromFile("MikasaBot.json")) { }

    static void Main(string[] args)
    {
        new MikasaBot().Start();
    }

    public override void Run()
    {
        BodyColor = Color.Red;
        TurretColor = Color.Cyan;
        RadarColor = Color.Lime;
        
        while (IsRunning)
        {
            if (Energy < LowHealthThreshold)
            {
                EvasiveManeuver();
            }
            else
            {
                SetTurnLeft(10_000);
                MaxSpeed = 5;
                Forward(10_000);
            }
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        
        if (distance < MaxDistanceToRam)
        {
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance + 5);
        }
        else if (distance < FireDistance)
        {
            Fire(3);
        }
    }

    public override void OnHitBot(HitBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);
        
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

        Forward(40);
    }

    private void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        turnDirection = bearing >= 0 ? 1 : -1;
        TurnLeft(bearing);
    }

    private void EvasiveManeuver()
    {
        turnCounter++;

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

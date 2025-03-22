using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Levi : Bot
{
    private const int BINS = 31;
    private const double MAX_GUESS_FACTOR = 1.0;
    private const double MIN_GUESS_FACTOR = -1.0;
    private const double MAX_VELOCITY = 8.0;

    private double[] guessFactorStats = new double[BINS];
    private double lastEnemyX, lastEnemyY, lastEnemyVelocity;
    private int stationaryCounter = 0;
    private int moveDirection = 1;
    private Random random = new Random();

    static void Main() => new Levi().Start();

    Levi() : base(BotInfo.FromFile("Levi.json")) { }

    public override void Run()
    {
        BodyColor = Color.Black;
        TurretColor = Color.Black;
        RadarColor = Color.White;
        BulletColor = Color.Red;
        ScanColor = Color.White;

        while (IsRunning)
        {
            // Asynchronous continuous movement
            MoveInWave();

            // Continuous radar scanning (non-blocking)
            SetTurnRadarRight(360);

            Execute(); // Apply all asynchronous commands
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double enemyX = e.X;
        double enemyY = e.Y;
        double enemyVelocity = e.Speed;
        double enemyHeading = e.Direction;
        double enemyDistance = DistanceTo(enemyX, enemyY);

        if (enemyX == lastEnemyX && enemyY == lastEnemyY && enemyVelocity == 0)
            stationaryCounter++;
        else
            stationaryCounter = 0;

        if (stationaryCounter > 2) // Fire directly if stationary
        {
            SetFire(3.0);
        }
        else
        {
            double lateralDirection = Math.Sign(enemyVelocity * Math.Sin(DegToRad(enemyHeading - Direction)));
            double guessFactor = lateralDirection * (enemyVelocity / MAX_VELOCITY);

            int index = (int)((guessFactor - MIN_GUESS_FACTOR) / (MAX_GUESS_FACTOR - MIN_GUESS_FACTOR) * (BINS - 1));
            index = Math.Max(0, Math.Min(BINS - 1, index));
            guessFactorStats[index]++;

            int bestIndex = 0;
            for (int i = 0; i < BINS; i++)
            {
                if (guessFactorStats[i] > guessFactorStats[bestIndex])
                    bestIndex = i;
            }

            double fireGuessFactor = MIN_GUESS_FACTOR + ((double)bestIndex / (BINS - 1)) * (MAX_GUESS_FACTOR - MIN_GUESS_FACTOR);
            double fireAngle = enemyHeading + fireGuessFactor * 30;
            double absoluteFireAngle = Direction + fireAngle;

            SetTurnGunRight(NormalizeBearing(absoluteFireAngle - GunDirection));

            double firepower = Math.Min(3.0, 500 / enemyDistance);
            SetFire(firepower);
        }

        lastEnemyX = enemyX;
        lastEnemyY = enemyY;
        lastEnemyVelocity = enemyVelocity;

        Execute(); // Apply all set commands
    }

    private void MoveInWave()
    {
        if (random.Next(100) < 5)
            moveDirection *= -1;

        double angle = 30 * Math.Sin(Time / 20.0);
        double distance = 50 * moveDirection;

        SetTurnRight(angle);
        SetForward(distance);
    }

    private double NormalizeBearing(double angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;
        return angle;
    }

    private double DegToRad(double angle) => angle * Math.PI / 180.0;
}

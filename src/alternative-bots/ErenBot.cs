
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class ErenBot : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new ErenBot().Start();
    }

    // Constructor, which loads the bot config file
    ErenBot() : base(BotInfo.FromFile("ErenBot.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor = Color.FromArgb(0x00, 0x00, 0x00);
        TurretColor = Color.FromArgb(0x00, 0x00, 0x00);
        RadarColor = Color.FromArgb(0x00, 0x00, 0x00);
        BulletColor = Color.FromArgb(0x00, 0x00, 0x00);
        ScanColor = Color.FromArgb(0x00, 0x00, 0x00);
        TracksColor = Color.FromArgb(0x00, 0x00, 0x00);
        GunColor = Color.FromArgb(0x00, 0x00, 0x00);

        // Repeat while the bot is running
        while (IsRunning)
        {
            // Write your bot logic
        }
    }
}

namespace Adfcf.ExplorerCraft.World
{
    internal class DayCycle
    {

        public static float SecondsPerDay { get; } = 86400;
        public static float TicksPerDay { get; } = SecondsPerDay * 60.0f;
        public static float TimePerTick { get; } = 1.0f / TicksPerDay;

        public int Day { get; private set; }
        public float Current { get; private set; }

        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public float Speed { get; set; } = 1.0f;

        public void Tick()
        {
            Current += TimePerTick * Speed;
            if (Current >= 1)
            {
                Current = 0;
                ++Day;
            }

            int totalTime = (int)Math.Round(Current * SecondsPerDay);

            Hour = (totalTime / (60 * 60));
            totalTime %= (60 * 60);

            Minute = (totalTime / 60);
            totalTime %= 60;

            Second = totalTime;

        }

        public override string ToString() => string.Format("Day {0} | {1}h {2}m {3}s", Day, Hour, Minute, Second);
        

    }
}

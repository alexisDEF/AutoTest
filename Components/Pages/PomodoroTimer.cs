namespace AutoTest.Components.Pages
{
    public class PomodoroTimer
    {
        public int WorkDuration { get; set; } = 25;
        public int BreakDuration { get; set; } = 5;
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public bool IsTimerRunning { get; private set; }
        public bool IsWorkSession { get; private set; } = true;
        public int SessionCount { get; private set; }

        private System.Timers.Timer Timer;

        public PomodoroTimer()
        {
            ResetTimer();
        }

        public void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Seconds == 0)
            {
                if (Minutes == 0)
                {
                    Timer.Stop();
                    IsTimerRunning = false;
                    if (IsWorkSession)
                    {
                        IsWorkSession = false;
                        SessionCount++;
                        Minutes = BreakDuration;
                    }
                    else
                    {
                        IsWorkSession = true;
                        Minutes = WorkDuration;
                    }
                    Seconds = 0;
                }
                else
                {
                    Minutes--;
                    Seconds = 59;
                }
            }
            else
            {
                Seconds--;
            }
        }

        public async Task StartTimer()
        {
            IsTimerRunning = true;
            Timer.Start();
            await Task.CompletedTask; // Simulate async call
        }

        public async Task ResetTimer()
        {
            Timer?.Stop();
            IsTimerRunning = false;
            IsWorkSession = true;
            Minutes = WorkDuration;
            Seconds = 0;
            Timer = new System.Timers.Timer(1000);
            Timer.Elapsed += TimerElapsed;
            await Task.CompletedTask; // Simulate async call
        }

        public void UpdateWorkDuration(int workDuration)
        {
            WorkDuration = workDuration;
            if (IsWorkSession)
            {
                Minutes = WorkDuration;
                Seconds = 0;
            }
        }

        public void UpdateBreakDuration(int breakDuration)
        {
            BreakDuration = breakDuration;
            if (!IsWorkSession)
            {
                Minutes = BreakDuration;
                Seconds = 0;
            }
        }
    }
}


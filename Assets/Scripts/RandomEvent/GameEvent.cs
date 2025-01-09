using System;

public class GameEvent
{
    public GameEvent Next { get; set; }
    public string EventName { get; set; }
    public float EventTime { get; set; }
    public int Priority { get; set; }


    public GameEvent(string eventName, float eventTime, int priority)
    {
        Next = null;
        EventName = eventName;
        EventTime = eventTime;
        Priority = priority;
    }
}
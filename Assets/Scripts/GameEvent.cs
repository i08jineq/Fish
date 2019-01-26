using System.Collections;
using System.Collections.Generic;

public class GameEvent
{
    public EventPublisher gameOver = new EventPublisher();
    public EventPublisher gamePaused = new EventPublisher();

    public EventPublisher<float> shakeCamera = new EventPublisher<float>();
}

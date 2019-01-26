using System.Collections;
using System.Collections.Generic;

public class GameEvent
{
    public EventPublisher gameOver = new EventPublisher();
    public EventPublisher gamePaused = new EventPublisher();
    public EventPublisher onStageCleared = new EventPublisher();

    public EventPublisher<Pawn> onPlayerJoinedStage = new EventPublisher<Pawn>();
    public EventPublisher<Pawn> onSpawnedPawn = new EventPublisher<Pawn>();

    public EventPublisher<float> shakeCamera = new EventPublisher<float>();

    public EventPublisher<Pawn, float> takeDamageEvent = new EventPublisher<Pawn, float>();
    public EventPublisher<Pawn> deadEvent = new EventPublisher<Pawn>();

    public EventPublisher<AttackObject> onSpawnedAttackObject = new EventPublisher<AttackObject>();
    public EventPublisher<AttackObject> onDeadAttackObject = new EventPublisher<AttackObject>();
}

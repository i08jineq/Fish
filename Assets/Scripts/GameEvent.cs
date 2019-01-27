using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameEvent
{
    public EventPublisher gameOver = new EventPublisher();
    public EventPublisher gamePaused = new EventPublisher();
    public EventPublisher onStageCleared = new EventPublisher();

    public EventPublisher<Pawn> onPlayerJoinedStage = new EventPublisher<Pawn>();
    public EventPublisher<Pawn> onSpawnedPawn = new EventPublisher<Pawn>();

    public EventPublisher<Pawn> onPawnStateChange = new EventPublisher<Pawn>();

    public EventPublisher<float> shakeCamera = new EventPublisher<float>();

    public EventPublisher<Pawn, float> takeDamageEvent = new EventPublisher<Pawn, float>();
    public EventPublisher<Pawn> deadEvent = new EventPublisher<Pawn>();

    public EventPublisher<AttackObject> onSpawnedAttackObject = new EventPublisher<AttackObject>();
    public EventPublisher<AttackObject> onAttackObjectDestroyed = new EventPublisher<AttackObject>();

    public EventPublisher<AudioClip> onPlaySoundEffect = new EventPublisher<AudioClip>();
    public EventPublisher<AudioClip> onPlayBGM = new EventPublisher<AudioClip>();
    public EventPublisher onStopBGM = new EventPublisher();
}

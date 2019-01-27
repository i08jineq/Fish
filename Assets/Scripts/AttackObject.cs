using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [SerializeField] protected AttackObjectData _attackObjectState;
    [SerializeField] protected GameObject _hitParticleEffect;
    [SerializeField] protected float hitEffectLifeTime = 1;
    [SerializeField] protected AudioClip attackSound;
    [SerializeField] protected List<AudioClip> hitSounds = new List<AudioClip>();
    Pawn _ownerPawn;

    public void Init(Pawn ownerPawn)
    {
        _ownerPawn = ownerPawn;
        transform.forward = ownerPawn.transform.forward;
        _attackObjectState.startPosition = ownerPawn.transform.position;
        _attackObjectState.startForward = ownerPawn.transform.forward;
        if (attackSound != null)
            Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(attackSound);
    }

    public void OnUpdate(float deltaTime)
    {
        _attackObjectState.lifeTime += deltaTime;
        if (_attackObjectState.lifeTime >= _attackObjectState.destroyTime)
        {
            Singleton.instance.gameEvent.onAttackObjectDestroyed.Invoke(this);
            Destroy(gameObject);
        }
        transform.position += transform.forward * deltaTime * _attackObjectState.speed;
    }

    protected virtual void Attack(Pawn attackedPawn)
    {
        if (hitSounds.Count > 0)
        {
            int index = Random.Range(0, hitSounds.Count);
            Singleton.instance.gameEvent.onPlaySoundEffect.Invoke(hitSounds[index]);
        }

        attackedPawn.TakeDamage(_attackObjectState.damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        var pawn = other.GetComponent<Pawn>();

        if (pawn != null && pawn != _ownerPawn)
        {
            Attack(pawn);

            GameObject hit = GameObject.Instantiate<GameObject>(_hitParticleEffect);
            hit.transform.position = transform.position;
            GameObject.Destroy(hit, hitEffectLifeTime);
            Singleton.instance.gameEvent.onAttackObjectDestroyed.Invoke(this);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    public AnimationClip replaceableAttackAnimation;
    public AnimationClip[] defaultAttackAnimationSet;
    protected AnimationClip[] currentAttackAnimationSet;

    protected Animator animator;
    NavMeshAgent agent;

    const float locomotionAnimationSmoothTime = .1f;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        if(overrideController==null)
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimationSet = defaultAttackAnimationSet;
        combat.OnAttack += OnAttack;

    }

    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack(){
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimationSet.Length);
        overrideController[replaceableAttackAnimation.name] = currentAttackAnimationSet[attackIndex];

    }
}

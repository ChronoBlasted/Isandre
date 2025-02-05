using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Component")]
    public Alive hpScript;
    public Animator animator;
    public CollectableSpawner Spawner;

    [Header("Data")]
    public ScriptableEnnemy enemyData;
    public Transform target; //Change with player Class
    protected bool inAction;

    #region Nouvelle Machine à états    

    [Header("StateMachine")]
    public FiniteStateMachine<Enemy> _stateMachine;
    #endregion
    
    private void Update()
    {
        faceToPlayer();
        _stateMachine.Update();
    }

    private void faceToPlayer()
    {
        transform.LookAt(target);
    }

    public virtual void Awake()
    {
        #region LifeGestion
        if (TryGetComponent<Alive>(out Alive _hpScript))
        {
            hpScript = _hpScript;
            hpScript.Init(enemyData.enemyLife);
            //hpScript.dieEvent.AddListener(Die);
            //hpScript.hitEvent.AddListener(Hitted);
        }
        #endregion

        #region GetAnimator
        if (animator == null && TryGetComponent(out Animator _Animator))
            animator = _Animator;
        #endregion

        #region StateMachine
        _stateMachine = new FiniteStateMachine<Enemy>(this);
        _stateMachine.AddState(new EnemyHitState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyDieState());
        #endregion
    }
    private void Start()
    {
        _stateMachine.SetState<EnemyMoveState>();        
    }

    private void OnDestroy()
    {
        //hpScript.dieEvent.RemoveAllListeners();w
    }

    public virtual void Attack()
    {
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    #region StateMachine
    public void ChangeStateToHit(){
        _stateMachine.SetState<EnemyHitState>();
    }
    
    public void ChangeStateToAttack()
    {            
        _stateMachine.SetState<EnemyAttackState>();    
    }
    
    public void changeStateToMove(){
        _stateMachine.SetState<EnemyMoveState>();    
    }
    
    public void changeStateToDie(){
        _stateMachine.SetState<EnemyDieState>();    
    }

    public void AnimationChange(string _animTrigger)
    {
        animator.SetTrigger(_animTrigger);
    }
    #endregion

}

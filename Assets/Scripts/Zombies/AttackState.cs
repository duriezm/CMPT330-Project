using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float meleeTimer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        // player can be seen
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            meleeTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            if (meleeTimer > 1)
            {
                Melee();
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 1)
            {
                // change to the search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Melee()
    {
        meleeTimer = 0;
    }
}

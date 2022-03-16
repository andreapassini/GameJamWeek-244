using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private FSM _fsm;


    // Start is called before the first frame update
    void Start()
    {
        // State 1 Quisecent
        FSMState _quiescent = new FSMState();

        FSMState _attack = new FSMState();

        FSMState _runAway = new FSMState();

        FSMTransition t1 = new FSMTransition(PlayerInRange);

        _fsm = new FSM(_quiescent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region FSM Transition
    public bool PlayerInRange()
    {
        // Check if Player is in range


        // Check if Player is visible


        return false;
    }

    #endregion
}


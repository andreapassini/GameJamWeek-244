using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float LowHealth = 25f;
    
    [SerializeField] private float visionRange = 10f;
    [SerializeField] LayerMask whatIsPlayer;

    private FSM _fsm;

    private EnemyHealthController _enemyHealthController;

    // Start is called before the first frame update
    void Start()
    {
        _enemyHealthController = GetComponent<EnemyHealthController>();
        
        // State 1 Quisecent
        FSMState _quiescent = new FSMState();

        // State 2 Attack
        FSMState _attack = new FSMState();

        // State 3 Run Back to position
        FSMState _runAway = new FSMState();

        FSMTransition t1 = new FSMTransition(PlayerInRange);
        FSMTransition t2 = new FSMTransition(PlayerNotInRange);
        FSMTransition t3 = new FSMTransition(EnemyHealthLow);

        _fsm = new FSM(_quiescent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region FSM Transition
    public bool PlayerInRange()
    {
        bool _inRange = false;
        Vector3 _playerPos = new Vector3(0f, 0f, 0f);

        // Check if Player is in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, whatIsPlayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out HealthController h))
            {
                _playerPos = hitCollider.transform.position;
                _inRange = true;
            }
                
        }

        if (!_inRange)
        {
            return false;
        }

        // Check if Player is visible
        RaycastHit hit;
        Vector3 ray = _playerPos - transform.position;
        if (Physics.Raycast(transform.position, ray, out hit))
        {
            if(hit.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

    public bool PlayerNotInRange()
    {
        return !PlayerInRange();
    }

    public bool EnemyHealthLow()
    {
        if(_enemyHealthController.GetHealth() < LowHealth)
        {
            return true;
        }

        return false;
    }

    #endregion
}


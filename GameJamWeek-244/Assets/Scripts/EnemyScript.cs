using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    public float AIFrameRate = 1f;

    public float LowHealth = 25f;

    public Transform StartPatrol;
    public Transform EndPatrol;
    
    [SerializeField] private float visionRange = 10f;
    [SerializeField] float nearRange = 2f;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] LayerMask whatIsNotPlayer;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpForce = 3f;


    private FSM _fsm;
    private DecisionTree _dtRonda;

    private EnemyHealthController _enemyHealthController;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    private bool _goingToStart = true;

    // Start is called before the first frame update
    void Start()
    {
        _enemyHealthController = GetComponent<EnemyHealthController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        
        // State 1 Quisecent
        FSMState _quiescent = new FSMState();
        _quiescent.enterActions.Add(WalkRondaDT);

        // State 2 Attack
        FSMState _attack = new FSMState();

        // State 3 Run Back to position
        FSMState _runAway = new FSMState();

        FSMTransition t1 = new FSMTransition(PlayerInRange);
        FSMTransition t2 = new FSMTransition(PlayerNotInRange);
        FSMTransition t3 = new FSMTransition(EnemyHealthLow);
        FSMTransition t4 = new FSMTransition(EnemyNear);

        _quiescent.AddTransition(t1, _attack);

        _attack.AddTransition(t2, _quiescent);
        _attack.AddTransition(t3, _runAway);

        _runAway.AddTransition(t4, _quiescent);


        // DT Ronda

        //root
        DTDecision _dtRonda_d1 = new DTDecision(IsAtStart);
        DTDecision _dtRonda_d2 = new DTDecision(NeedToJump);
        DTDecision _dtRonda_d3 = new DTDecision(NeedToJump);


        DTAction _dtRonda_a1 = new DTAction(GoToStart);
        DTAction _dtRonda_a2 = new DTAction(GoToEnd);
        DTAction _dtRonda_a3 = new DTAction(Jump);

        _dtRonda_d1.AddLink(false, _dtRonda_d2);
        _dtRonda_d1.AddLink(true, _dtRonda_d3);

        _dtRonda_d2.AddLink(true, _dtRonda_a3);
        _dtRonda_d2.AddLink(false, _dtRonda_a1);

        _dtRonda_d3.AddLink(true, _dtRonda_a3);
        _dtRonda_d3.AddLink(true, _dtRonda_a2);

        _dtRonda = new DecisionTree(_dtRonda_d1);

        _fsm = new FSM(_quiescent);

        // Start coroutine, start Monitoring FSM
        StartCoroutine(Patrol());

    }


    #region FSM Conditions
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

    public bool EnemyNear()
	{
        // Check if Player is in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, whatIsPlayer);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.TryGetComponent(out EnemyHealthController h)) {
                return true;
            }

        }

        return false;
	}

	#endregion

	#region FSM Actions
    public void WalkRondaDT()
	{
        StartCoroutine(PatrolDTRonda());
	}

	#endregion

	#region DT Decision
    public object IsAtStart(object o)
	{
		if (_goingToStart) {
            if (Vector2.Distance(transform.position, StartPatrol.position) <= nearRange) {
                _goingToStart = false;
                Debug.Log(_goingToStart);
                return true;
            }
        }

        if (!_goingToStart) {
            if (Vector2.Distance(transform.position, EndPatrol.position) <= nearRange) {
                _goingToStart = true;
                Debug.Log(_goingToStart);
                return false;
            }
        }

        return !_goingToStart;
    }

    public object NeedToJump(object o)
    {
        if (Physics2D.CircleCast(transform.position, transform.localScale.x, transform.forward, nearRange * 2, whatIsNotPlayer)) {
            return true;
        }

        return false;
    }


    #endregion

    #region DT Actions
    public object GoToStart(object o)
	{

        if(StartPatrol.position.x < transform.position.x) 
        {
            //Go left
            _rigidbody2D.velocity = new Vector2(-1 * speed, _rigidbody2D.velocity.y);

        } else{
            //Go right
            _rigidbody2D.velocity = new Vector2(1 * speed, _rigidbody2D.velocity.y);
        }

  //      RaycastHit2D[] _raycastHit2D;

  //      ContactFilter2D _contactFilter2D = new ContactFilter2D();
  //      _contactFilter2D.SetLayerMask(whatIsPlayer);
  //      _contactFilter2D.useLayerMask = true;

		//int v = _collider2D.Cast(transform.forward, _raycastHit2D, _contactFilter2D);

        return null;
	}

    public object GoToEnd(object o)
    {

        if (EndPatrol.position.x < transform.position.x) {
            //Go left
            _rigidbody2D.velocity = new Vector2(-1 * speed, _rigidbody2D.velocity.y);
        } else {
            //Go right
            _rigidbody2D.velocity = new Vector2(1 * speed, _rigidbody2D.velocity.y);
        }

        return null;
    }

    public object Jump(object o)
	{
        _rigidbody2D.AddForce(transform.up * (jumpForce), ForceMode2D.Impulse);

        return null;
	}

	#endregion

    IEnumerator Patrol()
	{
        while (true) {
            _fsm.Update();
            yield return new WaitForSeconds(AIFrameRate);
        }
    }

    IEnumerator PatrolDTRonda()
	{
        while (true) {
            _dtRonda.walk();
            yield return new WaitForSeconds(AIFrameRate);
        }
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, nearRange);
    }
}


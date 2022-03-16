using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    [SerializeField] private float startingHealth = 100f;

    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public float GetHealth()
    {
        return _health;
    }
}

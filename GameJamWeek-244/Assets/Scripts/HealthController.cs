using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private float tinyScale = .5f;
    
    public GameObject ParticleDeath;

    private float _health;
    private Animator _animator;
    private Vector3 _startingScale;
    private bool _isTiny;

    // Start is called before the first frame update
    void Start()
    {
        _health = startingHealth;
        _isTiny = false;

        _startingScale = transform.localScale;
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;
        
        if(_health <= 0 || _isTiny)
        {
            Die();
        } 
        else if (!_isTiny)
        {
            _isTiny = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !_isTiny)
        {
            MakeTiny();
            _isTiny = true;
        } else if(Input.GetMouseButtonDown(1) && _isTiny)
        {
            ReverseTiny();
            _isTiny = false;
        }
    }

    public void MakeTiny()
    {
        transform.localScale *= - tinyScale;
    }

    public void ReverseTiny()
    {
        transform.localScale = _startingScale;
    }

    public void Die()
    {
        Instantiate(ParticleDeath, transform.position, Quaternion.identity);

        // PLay animation
        _animator.SetBool("isDead", true);

        // Destroy the object
        Destroy(gameObject);
    }
}

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

        if (!_isTiny)
        {
            _isTiny = true;
            MakeTiny();
        }
    }

    public void TakeHealth(float hp)
	{
        _health += hp;

		if (_isTiny) 
        {
            _isTiny = false;
            ReverseTiny();
		}

        if (_health >= startingHealth) 
        {
            _health = startingHealth;
        }        
	}

    private void Update()
    {

    }   

    public void MakeTiny()
    {
        transform.localScale = new Vector3(tinyScale, tinyScale, tinyScale);
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

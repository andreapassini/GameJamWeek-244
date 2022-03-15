using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] LayerMask whatIsHit;

    public float ThuderRange = 5f;

    private bool _shoot = false;

    [SerializeField] AudioSource audioSource;

    void Update()
    {
		if (Input.GetMouseButtonDown(0)) 
        {
            _shoot = !_shoot;
		}
        
    }

	private void FixedUpdate()
	{
		if (_shoot) 
        {
            _shoot = !_shoot;
            ShootThunder();
		}
	}

	public void ShootThunder()
	{
        audioSource.Play();

        RaycastHit2D hit = Physics2D.Raycast(transform.forward, gun.forward, ThuderRange * 10);

        if (hit.collider != null) 
        {
            
		}
	}

	private void OnDrawGizmos()
	{
        // Thuder range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ThuderRange);

        // Thuder strating point
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gun.position, .5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] LayerMask whatIsHit;

    public float ThuderRange = 5f;

    private bool _shoot = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Jump")) 
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
        Debug.Log("Shoot");

        RaycastHit2D hit = Physics2D.Raycast(transform.forward, transform.forward * 5, ThuderRange);

        if (hit.collider != null) 
        {
            Debug.Log("hit");
		}
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, ThuderRange);
    }
}

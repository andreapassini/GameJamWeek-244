using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject lightningPrefabs;
    [SerializeField] Transform gunPos;
    [SerializeField] float lightningBoltDuration = .5f;

    private Vector2 _startLightningBolt;
    private Vector2 _endLightningBolt;
    private bool _isShoothing;

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isShoothing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShoothing)
        {
            ShootThunderbolt();
            _isShoothing = true;
        }
    }

    private void ShootThunderbolt()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        
        // Start LightningBolt
        lightningPrefabs.transform.GetChild(0).position = gunPos.position;

        // End LightningBolt
        lightningPrefabs.transform.GetChild(1).position = gunPos.position + new Vector3(7f, 0f, 0f);

        GameObject lightningBoltInst = Instantiate(lightningPrefabs);

        StartCoroutine(LightningBoltDuration(lightningBoltInst));
    }

    IEnumerator LightningBoltDuration(GameObject lightningBoltToKill)
    {
        yield return new WaitForSeconds(lightningBoltDuration);

        Destroy(lightningBoltToKill);

        _rigidbody2D.constraints = 0;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        _isShoothing = false;
    }
}

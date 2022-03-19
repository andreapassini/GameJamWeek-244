using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject shootThuderBoltPrefab;
    [SerializeField] GameObject selfHitThunderBoltPrefab;
    [SerializeField] Transform gunPos;
    [SerializeField] float lightningBoltDuration = .5f;

    private Vector2 _startLightningBolt;
    private Vector2 _endLightningBolt;
    private bool _isShoothing;

    private Rigidbody2D _rigidbody2D;

    public HealthController HealthController;

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

		if (Input.GetMouseButtonDown(1)) 
        {
            SelfHitThunder();
		}
    }

    public void ShootThunderbolt()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        
        // Start LightningBolt
        shootThuderBoltPrefab.transform.GetChild(0).position = gunPos.position;

        // End LightningBolt
        shootThuderBoltPrefab.transform.GetChild(1).position = gunPos.position + new Vector3(7f, 0f, 0f);

        GameObject lightningBoltInst = Instantiate(shootThuderBoltPrefab);

        StartCoroutine(LightningBoltDuration(lightningBoltInst, lightningBoltDuration));
    }

    public void SelfHitThunder()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        selfHitThunderBoltPrefab.GetComponent<SelfThunderController>().SelfShootThunder();

        StartCoroutine(SelfLightningBoltDuration(null, selfHitThunderBoltPrefab.GetComponent<SelfThunderController>().ThunderBoltDuration));
    }

    IEnumerator LightningBoltDuration(GameObject lightningBoltToKill, float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(lightningBoltToKill);

        _rigidbody2D.constraints = 0;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        _isShoothing = false;
    }

    IEnumerator SelfLightningBoltDuration(GameObject lightningBoltToKill, float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(lightningBoltToKill);

        _rigidbody2D.constraints = 0;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        HealthController.TakeDamage(0f);
    }
}

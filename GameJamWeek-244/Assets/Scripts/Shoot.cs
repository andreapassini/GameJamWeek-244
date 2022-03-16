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

    // Start is called before the first frame update
    void Start()
    {
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
        // Start LightningBolt
        lightningPrefabs.transform.GetChild(0).position = gunPos.position;

        // End LightningBolt
        lightningPrefabs.transform.GetChild(1).position = gunPos.position + new Vector3(5f, 0f, 0f);

        GameObject lightningBoltInst = Instantiate(lightningPrefabs);

        StartCoroutine(LightningBoltDuration(lightningBoltInst));
    }

    IEnumerator LightningBoltDuration(GameObject lightningBoltToKill)
    {

        yield return new WaitForSeconds(lightningBoltDuration);

        Destroy(lightningBoltToKill);

        _isShoothing = false;
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    public bool FollowVertical;
    public bool FollowHorizontal;

    public float XOffset;
    public float YOffset;

     // Update is called once per frame
    void Update()
    {

        if (Player)
        {
            if (FollowHorizontal && FollowVertical)
            {
                transform.position = new Vector3(Player.position.x + XOffset, Player.position.y + YOffset, transform.position.z);
                return;
            }


            if (FollowHorizontal)
                transform.position = new Vector3(Player.position.x + XOffset, transform.position.y, transform.position.z);

            if (FollowVertical)
                transform.position = new Vector3(transform.position.x, Player.position.y + YOffset, transform.position.z);
        }
        
    }
}

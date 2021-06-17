using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
       
            FollowCubePositionY();
    }

    public void FollowCubePositionY()
    {
        Vector3 newPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        player.position = newPos;
    }
}

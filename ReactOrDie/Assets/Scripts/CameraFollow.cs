using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_characterToFollow;

    private void Update()
    {
        Vector3 cameraMovement = new Vector3(m_characterToFollow.position.x, m_characterToFollow.position.y, -10);
        transform.position = cameraMovement;
    }
}

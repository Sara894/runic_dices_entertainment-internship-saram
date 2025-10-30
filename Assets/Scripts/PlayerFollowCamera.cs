// This script demonstrates how to set up a camera to follow the player smoothly.
// In this game, this script is not used, and I use cinemamachine instead.

using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 followPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
        transform.position = followPosition;
    }
}

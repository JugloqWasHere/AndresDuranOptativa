using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public GameObject Tooltip;

    [Header("Destino")]
    public Transform teleportPoint;

    private bool playerInside;
    private GameObject player;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        Tooltip.SetActive(false);

        playerInside = false;

        if (player == null || teleportPoint == null)
            return;

        CharacterController cc = player.GetComponent<CharacterController>();
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (cc != null)
            cc.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        player.transform.position = teleportPoint.position;
        player.transform.rotation = teleportPoint.rotation;

        if (cc != null)
            cc.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            Tooltip.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tooltip.SetActive(false);
            playerInside = false;
        }
    }
}
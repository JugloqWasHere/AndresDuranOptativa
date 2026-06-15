using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;

    [Header("Animación")]
    public Animator animator;

    private bool opened;

    private void Update()
    {
        if (isOpen && !opened)
        {
            opened = true;

            if (animator != null)
                animator.SetTrigger("Open");
        }
    }
}
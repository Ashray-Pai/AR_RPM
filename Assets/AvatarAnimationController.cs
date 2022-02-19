using UnityEngine;

public class AvatarAnimationController : MonoBehaviour
{
    [SerializeField] private AvatarImporter avatarImporter;

    [SerializeField] private RuntimeAnimatorController idleController;
    [SerializeField] private RuntimeAnimatorController walkingController;

    private Animator animator;

    private bool isAnimatorAssigned = false;

    private void Update()
    {
        // make sure that the avatar is loaded before getting the Animator component
        if (avatarImporter.ImportedAvatar != null && !isAnimatorAssigned)
        {
            animator = avatarImporter.ImportedAvatar.transform.GetComponent<Animator>();
            isAnimatorAssigned= true;
        }
    }

    public void StartAnimation()
    {
        animator.runtimeAnimatorController = walkingController;
        animator.Play("Walk"); // Passing the state name of walkingController Animator controller
    }

    public void StopAnimation()
    {
        animator.runtimeAnimatorController = idleController;
        animator.Play("Idle");// Passing the state name of idleController Animator controller
    }
}

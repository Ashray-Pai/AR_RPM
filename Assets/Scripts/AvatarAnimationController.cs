using UnityEngine;

public class AvatarAnimationController : MonoBehaviour
{
    [SerializeField] private AvatarImporter avatarImporter;
    [SerializeField] private RuntimeAnimatorController avatarController;

    private Animator animator;

    private bool isAnimatorAssigned = false;

    private void Update()
    {
        // make sure that the avatar is loaded before getting the Animator component
        if (avatarImporter.ImportedAvatar != null && !isAnimatorAssigned)
        {
            animator = avatarImporter.ImportedAvatar.transform.GetComponent<Animator>();
            animator.runtimeAnimatorController = avatarController;
            isAnimatorAssigned = true;
        }
    }

    public void StartAnimation()
    {
        this.animator.SetBool("isMoving", true);
    }

    public void StopAnimation()
    {
        this.animator.SetBool("isMoving", false);
    }
}

using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    [SerializeField] private Animator animator;



    private void Awake()
    {
        animator.SetBool(IS_WALKING, false);
    }


    public void UpdateIsWalking(bool value)
    {
        animator.SetBool(IS_WALKING, value);
    }
}

using UnityEngine;

public class Eye : MonoBehaviour
{
public Animator animator;
    void Start()
    {
        //OpenEye();
    }

    public void OpenEye()
    {
        animator.SetTrigger("open");
    }
    public void CloseEye()
    {
        animator.SetTrigger("close");
    }
}

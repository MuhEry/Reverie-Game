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
        animator.SetFloat("speed",-1);
        animator.SetTrigger("play");
    }
    public void CloseEye()
    {
        animator.SetFloat("speed",1);
        animator.SetTrigger("play");
    }
}

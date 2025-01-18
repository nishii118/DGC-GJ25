using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isAlive;
    [SerializeField] private Animator animator;
    void Start()
    {
        isAlive = true;
        animator.SetBool("isAlive", isAlive);
    }

    void OnEnable()
    {
        Messenger.AddListener(EventKey.ONBREAKBUBBLE, Process);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONBREAKBUBBLE, Process);
    }
    // Update is called once per frame
    private void Process() {
        isAlive = false;
        animator.SetBool("isAlive", isAlive);
        gameObject.SetActive(false);
    }
}

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
        
    }

    void OnEnable()
    {
        isAlive = true;
        Debug.Log("Bubble is alive");
        animator.SetBool("isAlive", isAlive);
        Messenger.AddListener(EventKey.ONBREAKBUBBLE, Process);
        Messenger.AddListener(EventKey.ONBREAKBUBBLE2, Process2);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONBREAKBUBBLE, Process);
        Messenger.RemoveListener(EventKey.ONBREAKBUBBLE2, Process2);
    }
    // Update is called once per frame
    private void Process()
    {
        Debug.Log("Process 1");
        isAlive = false;
        animator.SetBool("isAlive", isAlive);
        Messenger.Broadcast<string>(EventKey.ONBREAKBUBBLESFX, "Break Bubble");
        StartCoroutine(DisableBubble());
    }

    private void Process2() {
        Debug.Log("Process 2");
        if (!isAlive) return;
        Process();
    }
    private IEnumerator DisableBubble()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}

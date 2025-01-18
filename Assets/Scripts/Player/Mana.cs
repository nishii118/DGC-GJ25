using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    [SerializeField] float maxValueMana = 100f;
    [SerializeField] float manaRegenRate = 5f;
    [SerializeField] float manaUseRate = 10f;
    [SerializeField] private Slider slider;
    private float manaValue;
    [SerializeField] private bool isUseMana = false;
    [SerializeField] private bool isManaEmpty = false;
    // [SerializeField] private bool canBreakeBubble = true;
    // Start is called before the first frame update
    void Start()
    {
        manaValue = maxValueMana;
    }

    void OnEnable()
    {
        Messenger.AddListener(EventKey.ONUSEMANA, OnUseMana);
        Messenger.AddListener(EventKey.ONREGETMANA, OnRegetMana);
        Messenger.AddListener<float>(EventKey.ONUSEMANAFORDASH, UseManaForDash);
        // Messenger.AddListener(EventKey.ONBREAKBUBBLEABLE, OnBreakable);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONUSEMANA, OnUseMana);
        Messenger.RemoveListener(EventKey.ONREGETMANA, OnRegetMana);
        Messenger.RemoveListener<float>(EventKey.ONUSEMANAFORDASH, UseManaForDash);
        // Messenger.RemoveListener(EventKey.ONBREAKBUBBLEABLE, OnBreakable);
    }

    // Update is called once per frame
    void Update()
    {
        if (isUseMana)
        {
            UseMana();
        }
        else
        {
            RegetMana();
        }

        ProcessManaSlider();
        // Debug.Log("Mana: " + manaValue);
    }

    private void UseMana()
    {
        if (manaValue > 0)
        {
            manaValue -= manaUseRate * Time.deltaTime; // Use mana
            // manaValue = Mathf.Max(manaValue, 0); // Clamp to 0
            isManaEmpty = false;
        }
        if (manaValue < 0) {
            manaValue = Mathf.Max(manaValue, 0); // Clamp to 0
        }

        if (manaValue <= 0 && !isManaEmpty)
        {
            isManaEmpty = true;
            Debug.Log("Mana is empty");
            Messenger.Broadcast(EventKey.ONBREAKBUBBLE);

        }
    }

    private void UseManaForDash(float amount) {
        if (manaValue > 0)
        {
            manaValue -= manaUseRate * Time.deltaTime + amount; // Use mana
            // manaValue = Mathf.Max(manaValue, 0); // Clamp to 0
            isManaEmpty = false;
        }
    }
    private void RegetMana()
    {
        if (manaValue < maxValueMana)
        {
            manaValue += manaRegenRate * Time.deltaTime; // Regenerate mana
            manaValue = Mathf.Min(manaValue, maxValueMana); // Clamp to maxMana
        }
    }

    private void OnUseMana()
    {
        isUseMana = true;
    }

    private void OnRegetMana()
    {
        isUseMana = false;
    }

    // private void OnBreakable() {
    //     canBreakeBubble = true;
    // }

    private void ProcessManaSlider() {
        slider.value = manaValue / maxValueMana;
    }
}

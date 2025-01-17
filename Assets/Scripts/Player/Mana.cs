using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] float maxValueMana = 100f;
    [SerializeField] float manaRegenRate = 1f;
    private float manaValue;
    [SerializeField] private bool isUseMana = false;
    // Start is called before the first frame update
    void Start()
    {
        manaValue = maxValueMana;
    }

    void OnEnable()
    {
        Messenger.AddListener(EventKey.ONUSEMANA, OnUseMana);
        Messenger.AddListener(EventKey.ONREGETMANA, OnRegetMana);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONUSEMANA, OnUseMana);
        Messenger.RemoveListener(EventKey.ONREGETMANA, OnRegetMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (isUseMana)
        {
            UseMana(manaRegenRate);
        }
        else
        {
            RegetMana();
        }

        // Debug.Log("Mana: " + manaValue);
    }

    private void UseMana(float amount)
    {
        if (manaValue >= amount)
        {
            manaValue -= manaRegenRate * Time.deltaTime; // Use mana
            manaValue = Mathf.Max(manaValue, 0); // Clamp to 0
        }

        if (manaValue <= 0)
        {
            Messenger.Broadcast(EventKey.ONBREAKBUBBLE);
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
}

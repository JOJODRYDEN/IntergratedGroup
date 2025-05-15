using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_PasscodePuzzleInput : MonoBehaviour
{
    [SerializeField] private int inputNum;

    [SerializeField] private ZZ_PasscodePuzzle puzzleScript;

    private bool canBeTriggered;
    private float cooldownTimer;
    [SerializeField] private float cooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        canBeTriggered = true;
        cooldownTimer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeTriggered)
        {
            if (cooldownTimer <= 0)
            {
                canBeTriggered = true;
                cooldownTimer = cooldown;
            }
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter()
    {
        if (canBeTriggered)
        {
            canBeTriggered = false;
            puzzleScript.SetInputCode(inputNum);
        }
    }
}

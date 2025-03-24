using System;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// This class handles player input and move helicopter and casts ability to the map
/// Also handles UI control.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject[] abilitiesTimerObject = new GameObject[5]; // 0 = Snipe, 1 =
    public float[] abilitiesRechargeTime = new float[5] {45f,30f,30f,30f,30f};
    public AbilityTimer[] abilitiesTimers = new AbilityTimer[5];
    // Contains all inputs
    public InputAction moveAction;
    public InputAction snipeAction;
    public InputAction lookAction;
    public InputAction searchlightAction;
    public InputAction healthAction;
    public InputAction ammoAction;
    public InputAction flareAction;
    public InputAction scanAction;
    public int snipeCount = 3;
    private void Awake()
    {
        for(int i = 0; i < abilitiesTimerObject.Length; i++)
        {
            AbilityTimer abilityTimer = abilitiesTimerObject[i].GetComponent<AbilityTimer>();
            abilitiesTimers[i] = abilityTimer;
            abilitiesTimers[i].recharging = false;
            abilitiesTimers[i].TimerActual = abilitiesRechargeTime[i];
        }
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        snipeAction = InputSystem.actions.FindAction("Snipe");
        lookAction = InputSystem.actions.FindAction("Look");
        searchlightAction = InputSystem.actions.FindAction("Searchlight");
        healthAction = InputSystem.actions.FindAction("Health");
        ammoAction = InputSystem.actions.FindAction("Ammo");
        flareAction = InputSystem.actions.FindAction("Flare");
        scanAction = InputSystem.actions.FindAction("Scan");


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue.x != 0 || moveValue.y != 0)
        {
            transform.position += new Vector3(moveValue.x, 0, moveValue.y) * Time.deltaTime;
        }

        if (snipeAction.IsPressed() && snipeCount>0)
        {
            if (!abilitiesTimers[0].recharging)
            {
                snipeCount--;
                abilitiesTimers[0].TriggerAbility();
            }
        }

        if (healthAction.IsPressed())
        {
            if (!abilitiesTimers[1].recharging)
            {
                abilitiesTimers[1].TriggerAbility();
            }
        }

        if (ammoAction.IsPressed())
        {
            if (!abilitiesTimers[2].recharging)
            {
                abilitiesTimers[2].TriggerAbility();
            }
        }

        if (flareAction.IsPressed())
        {
            if (!abilitiesTimers[3].recharging)
            {
                abilitiesTimers[3].TriggerAbility();
            }
        }

        if (scanAction.IsPressed())
        {
            if (!abilitiesTimers[4].recharging)
            {
                abilitiesTimers[4].TriggerAbility();
            }
        }

        if (searchlightAction.IsPressed())
        {
            
        }
    }
}

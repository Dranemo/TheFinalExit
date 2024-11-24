using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RechargableItem : ItemUsable
{
    [SerializeField] protected float charge = 100f;

    protected bool isOn = false;






    public override void Use(InputAction.CallbackContext context)
    {
        base.Use(context);

        isOn = !isOn;
        if (charge <= 0)
        {
            isOn = false;
        }

        StateItem(isOn);
    }




    private void Update()
    {
        if (isOn)
        {
            charge -= Time.deltaTime;

            if (charge <= 0)
            {
                isOn = false;
                StateItem(isOn);
            }
        }
    }


    protected virtual void StateItem(bool _bool) { }

    public void RechargeItem(float _charge)
    {
        charge += _charge;
        if (charge > 100)
        {
            charge = 100;
        }
    }
    public float GetCharge()
    {
        return charge;
    }
    public bool GetIsOn()
    {
        return isOn;
    }
}

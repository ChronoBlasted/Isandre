using System;
using UnityEngine;
using UnityEngine.Events;

public class Alive : MonoBehaviour
{
    public int currentLife;
    public int maxLife;
    public UnityEvent dieEvent;
    public UnityEvent hitEvent;
    public bool Add, Subtract, data;

    public void InitWithData(int data)
    {
        maxLife = data;
        currentLife = maxLife;
    }
    public void InitWithoutData()
    {
        currentLife = maxLife;
    }

    private void Start()
    {
        if (!data)
            InitWithoutData();
    }

    public void Update()
    {
        if (Add)
        {
            AddLife();
            Add = false;
        }
        if (Subtract)
        {
            SubtractLife();
            Subtract = false;
        }
    }
    public void PlayerDie()
    {
        GameManager.Instance.ReloadScene();
    }




    public void ChangeLife(int _i)
    {
        currentLife += _i;

        if (currentLife > maxLife)
        {
            currentLife = maxLife;
            return;
        }

        if (currentLife <= 0)
        {
            dieEvent.Invoke();
            return;
        }
    }
    public int GetLife()
    {
        return currentLife;
    }

    [ContextMenu("AddLife")]
    public void AddLife()
    {
        currentLife += 1;

        if (currentLife > maxLife)
        {
            currentLife = maxLife;
            return;
        }

    }
    [ContextMenu("SubtractLife")]
    public void SubtractLife()
    {
        currentLife += -1;


        if (currentLife <= 0)
        {
            currentLife = 0;

            dieEvent.Invoke();
            return;
        }

        hitEvent.Invoke();
    }

}

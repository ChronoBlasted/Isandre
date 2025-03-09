using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
public class Alive : MonoBehaviour
{
    public int currentLife;
    public int maxLife;
    public UnityEvent dieEvent;
    public UnityEvent hitEvent;
    public bool Add, Subtract, data, IsPlayer;
    public MMF_Player dieFeedbacks;
    bool isDie;

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

        if (IsPlayer)
        {
            UIManager.Instance.GameView.PlayerHealth.Init(currentLife, maxLife);
        }
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
    public void ChangeLife(int _i)
    {
        if (isDie)
            return;

        currentLife += _i;

        if (currentLife > maxLife)
        {
            currentLife = maxLife;
            return;
        }

        if (currentLife <= 0)
        {
            Die();
            return;
        }

        if (IsPlayer)
        {
            UIManager.Instance.GameView.PlayerHealth.SetValueSmooth(currentLife);
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

    public void Die()
    {
        isDie = true;
        dieEvent.Invoke();
        if (dieFeedbacks != null) dieFeedbacks.PlayFeedbacks();

        if (IsPlayer)
        {
            GameManager.Instance.UpdateStateToEnd();
        }
    }

}

using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerChoice
{
    public static event Action<int> OnPlayerChoice;

    public void PlayerChoice(int postType)
    {
        OnPlayerChoice?.Invoke(postType);
    }
}

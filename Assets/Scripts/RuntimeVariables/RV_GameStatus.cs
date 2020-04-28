using UnityEngine;
using Assets.Scripts.Enums;

[CreateAssetMenu(menuName = "Runtime Variables/GameStatus", order = 2)]
public class RV_GameStatus : ScriptableObject
{
    public GameStatus status;
}

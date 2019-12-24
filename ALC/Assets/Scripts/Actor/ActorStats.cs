using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ActorStats", menuName = "Actor Stats", order = 53)]
public class ActorStats: ScriptableObject
{
    [SerializeField] private float m_movementSpeedMultiplier = 1f;
    [SerializeField] private float m_attackSpeedMultiplier = 1f;
    [SerializeField] private float m_damageCausedMultiplier = 1f;
    [SerializeField] private float m_damageTakenMultiplier = 1f;

    public float health { get; set; }

    public float movementSpeed
    {
        get { return m_movementSpeedMultiplier; }
        set { m_movementSpeedMultiplier *= value; }
    }
    public float attackSpeed
    {
        get { return m_attackSpeedMultiplier; }
        set { m_attackSpeedMultiplier *= value; }
    }
    public float damageCaused
    {
        get { return m_damageCausedMultiplier; }
        set { m_damageCausedMultiplier *= value; }
    }
    public float damageTaken
    {
        get { return m_damageTakenMultiplier; }
        set { m_damageTakenMultiplier *= value; }
    }
}
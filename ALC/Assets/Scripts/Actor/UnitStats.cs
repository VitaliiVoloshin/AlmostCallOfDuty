using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats
{

    private float reset = 1f;


    public enum Fraction
    {
        green,
        red
    }
    public Fraction fraction { get; set; }
    private float m_movementSpeed = 1f;
    private float m_attackSpeed = 1f;
    private float m_damageCaused = 1f;
    private float m_damageTaken = 1f;

    public float health { get; set; } = 100;

    public float movementSpeed
    {
        get { return m_movementSpeed; }
        set { m_movementSpeed *= value; }
    }
    public float attackSpeed
    {
        get { return m_attackSpeed; }
        set { m_attackSpeed *= value; }
    }
    public float damageCaused
    {
        get { return m_damageCaused; }
        set { m_damageCaused *= value; }
    }
    public float damageTaken
    {
        get { return m_damageTaken; }
        set { m_damageTaken *= value; }
    }

    public void ResetToDefault(float param)
    {
        param = reset;
    }
}
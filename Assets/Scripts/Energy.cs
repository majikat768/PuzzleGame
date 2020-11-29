using System;
using UnityEngine;

public class Energy
{
    public enum EnergyType
    {
        FIRE,
        WATER,
        DARK,
        LIGHT
    };

    public static Color GetColor(EnergyType type)
    {
        switch(type)
        {
            case EnergyType.FIRE:
                return new Color(1f, 0, 0);
            case EnergyType.WATER:
                return new Color(0f, 0.7f, 1f);
            case EnergyType.DARK:
                return new Color(0.7f, 0f, 1f);
            case EnergyType.LIGHT:
                return new Color(1f, 1f, 0f);
            default:
                return new Color(0f, 0f, 0f);
        }
    }
}

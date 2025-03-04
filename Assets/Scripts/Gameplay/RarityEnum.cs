using UnityEngine;

public enum RarityEnum
{
    Common,
    Rare,
    Epic
}

public static class RarityColor
{
    public static Color Common = new Color(1f, 1f, 1f); // Blanc
    public static Color Rare = new Color(0.4f, 0.6f, 1f); // Bleu pastel
    public static Color Epic = new Color(0.7f, 0.4f, 1f); // Violet pastel
}
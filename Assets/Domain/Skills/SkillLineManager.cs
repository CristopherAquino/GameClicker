using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLineManager : MonoBehaviour
{
    [System.Serializable]
    public class SkillLine
    {
        public int skillIndex; // skill que desbloquea esta línea
        public Image lineImage; // referencia a la línea en UI
    }

    public List<SkillLine> lines = new List<SkillLine>();
    public Color unlockedColor = Color.navyBlue;
    public Color lockedColor = Color.white;

    void Start()
    {
        // Inicializa todas las líneas en color bloqueado
        foreach (var l in lines)
        {
            if (l.lineImage) l.lineImage.color = lockedColor;
        }
    }

    public void ColorLine(int skillIndex)
    {
        foreach (var l in lines)
        {
            if (l.skillIndex == skillIndex && l.lineImage)
            {
                l.lineImage.color = unlockedColor;
            }
        }
    }
}


using UnityEngine;

public class DragSkill : MonoBehaviour
{
    public bool isSelected;
    public Skill skill;
    public Sprite sprite;
    
    public void Selected()
    {
        if (skill.levelSkill != 0)
        {
            isSelected = true;
            GamaManager.gm.isSelectedSkill = true;
            GamaManager.gm.dragSkill = this;
        }
    }

}

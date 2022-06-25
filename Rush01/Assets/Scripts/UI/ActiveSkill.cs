using UnityEngine;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public int number;
    
    private void Update()
    {
	    if (SkillManager.sk.skill[number] == null) return;
	    gameObject.GetComponent<Image>().color = SkillManager.sk.skill[number].isActive ? Color.white : Color.black;
    }
}

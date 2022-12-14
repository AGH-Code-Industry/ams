using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public bool canCast = true;

    public KeyCode[] primaryBinds = { KeyCode.Mouse0, KeyCode.Mouse1 };
    public KeyCode[] secondaryBinds = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};

    public GameObject[] primarySpells;
    public GameObject[] secondarySpells;

    Dictionary<KeyCode, SpellType> primarySpellsDict;
    Dictionary<KeyCode, SpellType> secondarySpellsDict;

    bool primaryCasting = false;

    private void Start()
    {
        for(int i=0; i<primarySpells.Length && i<primaryBinds.Length; i++)
        {
            primarySpellsDict.Add(primaryBinds[i], primarySpells[i].GetComponent<SpellType>());
        }

        for (int i = 0; i < secondarySpells.Length && i < secondaryBinds.Length; i++)
        {
            secondarySpellsDict.Add(secondaryBinds[i], secondarySpells[i].GetComponent<SpellType>());
        }
    }
    private void Update()
    {
        UsePrimarySpell();
    }

    void UsePrimarySpell()
    {
        if (canCast && !primaryCasting)
        {
            foreach (KeyValuePair<KeyCode, SpellType> entry in primarySpellsDict) {
                if (Input.GetKeyDown(entry.Key))
                {
                    primaryCasting = true;
                    primaryCooldown(entry.Value, entry.Key);
                }
            }
        }
    }

    IEnumerator primaryCooldown(SpellType spell, KeyCode key)
    {
        while (primaryCasting)
        {
            yield return new WaitForSeconds(spell.GetCooldown());
            spell.Cast(gameObject.transform);

            if (Input.GetKeyUp(key))
            {
                primaryCasting = false;
                spell.StopCast();
            }
        }
    }
}

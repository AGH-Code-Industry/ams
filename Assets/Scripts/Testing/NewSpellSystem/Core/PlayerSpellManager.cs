using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public bool canCast = true;
    public Transform spellOrigin;

    public KeyCode[] primaryBinds = { KeyCode.Mouse0, KeyCode.Mouse1 };
    public KeyCode[] secondaryBinds = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};

    public GameObject[] primarySpells;
    public GameObject[] secondarySpells;

    Dictionary<KeyCode, SpellType> primarySpellsDict;
    Dictionary<KeyCode, SpellType> secondarySpellsDict;

    bool primaryCasting = false;
    bool secondaryCasting = false;

    private void Start()
    {
        primarySpellsDict = new Dictionary<KeyCode, SpellType>();
        secondarySpellsDict = new Dictionary<KeyCode, SpellType>();
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
        UseSecondarySpell();
    }

    void UsePrimarySpell()
    {
        if (canCast && !primaryCasting)
        {
            foreach (KeyValuePair<KeyCode, SpellType> entry in primarySpellsDict) {
                if (Input.GetKeyDown(entry.Key))
                {
                    primaryCasting = true;
                    StartCoroutine(primaryCooldown(entry.Value, entry.Key));
                }
            }
        }
    }

    IEnumerator primaryCooldown(SpellType spell, KeyCode key)
    {
        while (primaryCasting)
        {
            yield return new WaitForSeconds(spell.GetCooldown());
            spell.Cast(spellOrigin);

            if (!Input.GetKey(key))
            {
                primaryCasting = false;
                spell.StopCast();
            }
        }
    }

    void UseSecondarySpell()
    {
        if (canCast && !secondaryCasting) {
            foreach (KeyValuePair<KeyCode, SpellType> entry in secondarySpellsDict)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    secondaryCasting = true;
                    SecondaryCast(entry.Value);
                }
            }
        }
    }

    void SecondaryCast(SpellType spell) {
        //Add cast time functionality
        spell.Cast(spellOrigin);
        secondaryCasting = false;
        //Add cooldown functionality?
    }
}

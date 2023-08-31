using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPerks : MonoBehaviour
{
    [Header("********** Perks ****************")]
    public float BerserkPerc;                               // Increase DMG based on STR
    public float Colossus;                                  // Increase Weight Cap based on STR
    public float Gladiator;                                 // Increase Armour based on STR
    public float Resilient;                                 // Decrease Stamina / Health Take based on STR

    public float Duelist;                                   // Increase Defense based on Weight
    public float Overwhelm;                                 // Increase Attack based on Weight
    public float Nimble;                                    // Decrease Damage Take based on Weight
    public float Relentless;                                // Increase Stamina and Regen based on Weight
}

/* 
 *  Author : Missnish
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Mage Data", menuName = "Character Data/Warrior")]
public class WarriorData : CharacterData
{
    public WarriorClassType classType;
    public WarriorWpnType wpnType;

}

/* 
 *  Author : Missnish
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Mage Data", menuName = "Character Data/Rogue")]
public class RogueData : CharacterData
{
    public RogueWpnType wpnType;
    public RogueStrategyType strategyType;

}

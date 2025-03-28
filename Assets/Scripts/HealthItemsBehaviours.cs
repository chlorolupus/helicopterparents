using UnityEngine;
using System.Collections.Generic;
namespace HelicopterParents.Goap.Behaviours
{
    public class HealthItemsBehaviours : MonoBehaviour
    {
        public ItemType itemType;
        public List<FighterBehaviour> fighterBehaviours;
        public int amount = 100;
        public bool lootable = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    }
}

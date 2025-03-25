using System;
using System.Collections.Generic;
using UnityEngine;
namespace HelicopterParents.Goap.Behaviours
{
    public class AmmoItemsBehvaiour : MonoBehaviour
    {
        public ItemType itemType;
        public List<FighterBehaviour> fighterBehaviours;
        public int amount = 100;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Fighter")
            {
                FighterBehaviour fighterBehaviour = other.gameObject.GetComponent<FighterBehaviour>();
                fighterBehaviour.isRefillingAmmo = true;
                fighterBehaviours.Add(fighterBehaviour);    
            }

        }

        public void OnTriggerExit(Collider other)
        {
            if (other.tag == "Fighter")
            {
                FighterBehaviour fighterBehaviour = other.gameObject.GetComponent<FighterBehaviour>();
                fighterBehaviour.isRefillingAmmo = false;
                fighterBehaviours.Remove(fighterBehaviour);
            }
        }
    }
}

using CrashKonijn.Agent.Core;
using UnityEngine;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using HelicopterParents.Goap.Behaviours;
using System.Collections.Generic;
using System.Linq;

namespace HelicopterParents.Goap.Sensors
{
    public class FindAmmoMultiSensor : MultiSensorBase
    {
        private List<AmmoItemsBehaviours> ammoItems;

        public FindAmmoMultiSensor()
        {
            this.AddLocalWorldSensor<NeedsAmmo>((agent, references) =>
            {
                // Get a cached reference to the DataBehaviour on the agent
                var data = references.GetCachedComponent<FighterBehaviour>();       

                return data.ammoCount;
            });
            this.AddLocalTargetSensor<ClosestAmmo>((agent,references,target )=>
            {
                var closestAmmo = references.GetCachedComponent<AmmoItemsBehaviours>();
                if (closestAmmo == null)
                    return null;
                if (target is TransformTarget transformTarget)
                    return transformTarget.SetTransform(closestAmmo.transform);
                return new TransformTarget(closestAmmo.transform);
            });
        }
        public override void Created()
        {

        }

        public override void Update()
        {
            this.ammoItems = Object.FindObjectsByType<AmmoItemsBehaviours>(FindObjectsSortMode.None).Where(item => item.lootable == true).ToList();
        }
        private T Closest<T>(IEnumerable<T> list, Vector3 position)
            where T : MonoBehaviour
        {
            T closest = null;
            var closestDistance = float.MaxValue; // Start with the largest possible distance

            foreach (var item in list)
            {
                var distance = Vector3.Distance(item.gameObject.transform.position, position);

                if (!(distance < closestDistance))
                    continue;

                closest = item;
                closestDistance = distance;
            }

            return closest;
        }
    }
}
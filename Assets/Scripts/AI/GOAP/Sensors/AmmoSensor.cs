
using System.Collections.Generic;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.AIGOAPGenerator.Behaviours;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;


namespace CrashKonijn.Goap.AIGOAPGenerator.Sensors
{
    [GoapId("AmmoSensor")]
    public class AmmoSensor : MultiSensorBase
    {
        private AmmoItemsBehvaiour[] ammoCrates;
        // Is called when this script is initialzed

        public AmmoSensor()
        {
            this.AddLocalWorldSensor<AmmoCount>((agent, references) =>
            {
                var data = references.GetCachedComponent<FighterBehaviour>();
                return data.ammoCount;

            });
            
            this.AddLocalTargetSensor<ClosestAmmoCrate>((agent, references, target) =>
            {
                var closestAmmo = this.Closest(this.ammoCrates, agent.Transform.position);
                if (closestAmmo == null)
                    return null;
                if(target is TransformTarget transformTarget)
                    return transformTarget.SetTransform(closestAmmo.transform);
                return new TransformTarget(closestAmmo.transform);
            });
            
        }

        public override void Created() { }

        // Is called every frame that an agent of an `AgentType` that uses this sensor needs it.
        // This can be used to 'cache' data that is used in the `Sense` method.
        // Eg look up all the trees in the scene, and then find the closest one in the Sense method.
        public override void Update()
        {
            this.ammoCrates = Object.FindObjectsByType<AmmoItemsBehvaiour>(FindObjectsSortMode.None);
            
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

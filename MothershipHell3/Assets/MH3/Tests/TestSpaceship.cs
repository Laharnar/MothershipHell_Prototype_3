using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SpaceShipTurretTests {
}

namespace SpaceshipBasicSetupTests {
    public class TestSpaceship {
        GameObject cam;
        GameObject ship1;
        GameObject ship2;

        TurretAPI[] tapis1;
        TurretAPI[] tapis2;

        ShipAPI sapi1;
        ShipAPI sapi2;

        [SetUp]
        public void Setup()
        {
            cam = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera-unitTests"));
            ship1 = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Spaceship-basic1"));
            ship2 = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Spaceship-basic2"));

            ship1.transform.position = new Vector3(0, 6);
            ship2.transform.position = new Vector3(0, -6);

            tapis1 = ship1.GetComponentsInChildren<TurretAPI>();
            tapis2 = ship2.GetComponentsInChildren<TurretAPI>();

            sapi1 = ship1.GetComponentInChildren<ShipAPI>();
            sapi2 = ship2.GetComponentInChildren<ShipAPI>();


        }

        [TearDown]
        public void Teardown() {
            GameObject.Destroy(ship1.gameObject);
            GameObject.Destroy(ship2.gameObject);
            GameObject.Destroy(cam.gameObject);
        }

        [UnityTest]
        public IEnumerator TestAimAnglesOf1Ship()
        {
            yield return new WaitForSeconds(1);
            sapi1.Track(ship2.transform, false);
            sapi2.Track(ship1.transform, false);

            //tapis1[0].TurnTowardsEmptySpace(tapis2[0].transform.position);
            //tapis2[0].TurnTowardsEmptySpace(tapis1[0].transform.position);
            
            yield return new WaitForSeconds(6);
            Debug.Log(ship1.transform.localEulerAngles);
            Debug.Log(sapi1.AimingToLookInDir);
            Debug.Log(sapi1.ShipDegrees);
            Debug.Log(sapi1.ShipDegrees);
            Assert.AreEqual(ship1.transform.localEulerAngles, new Vector3(0, 0, -90));
            

            yield return new WaitForSeconds(4);
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SpaceshipBasicSetupTests {
    public class TestSpaceship {
        GameObject cam;
        GameObject ship1;
        GameObject ship2;

        TurretAPI api1Ship;
        TurretAPI api2Ship;

        [SetUp]
        public void Setup()
        {
            cam = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Camera-unitTests"));
            ship1 = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Spaceship-basic1"));
            ship2 = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Spaceship-basic2"));

            ship1.transform.position = new Vector3(0, 6);
            ship2.transform.position = new Vector3(0, -6);

            api1Ship = ship1.GetComponentInChildren<TurretAPI>();
            api2Ship = ship2.GetComponentInChildren<TurretAPI>();

        }

        [TearDown]
        public void Teardown() {
            GameObject.Destroy(ship1.gameObject);
            GameObject.Destroy(ship2.gameObject);
            GameObject.Destroy(cam.gameObject);
        }

        [UnityTest]
        public IEnumerator Test2ShipsAimingAndDamagingEachOther()
        {
            api1Ship.TurnTowardsEmptySpace(api2Ship.transform.position);
            api2Ship.TurnTowardsEmptySpace(api1Ship.transform.position);
            yield return new WaitForSeconds(6);
            api1Ship.Fire(FireCommand.FireOneReady);
            api2Ship.Fire(FireCommand.FireOneReady);
            yield return new WaitForSeconds(2);
            
            Assert.AreEqual(ship1.GetComponentInChildren<Stats>().MaxHealth - 1, ship1.GetComponent<Stats>().CurHealth);

            Assert.AreEqual(ship2.GetComponentInChildren<Stats>().MaxHealth - 1, ship2.GetComponent<Stats>().CurHealth);
        }
    }
}

namespace TurretBasicSetupTests {

    public class TestTurret {
        GameObject cam;
        GameObject gameGameObject;

        [SetUp]
        public void Setup() {
            cam = MonoBehaviour.Instantiate(
                        Resources.Load<GameObject>("Prefabs/Camera-unitTests"));

            gameGameObject =
                    MonoBehaviour.Instantiate(
                        Resources.Load<GameObject>("Prefabs/Turret-basic"));
            gameGameObject.transform.position = new Vector3();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(gameGameObject);
            Object.Destroy(cam);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestTurretAI_RotationCommands()
        {

            TurretAPI api = gameGameObject.GetComponentInChildren<TurretAPI>();

            api.TurnTowardsEmptySpace(new Vector2(1, 0));
            yield return new WaitForSeconds(8);
            Assert.AreEqual(Quaternion.Euler(0, 0, 0).eulerAngles.z, api.GunRotation(0).eulerAngles.z, 2);

            api.TurnTowardsEmptySpace(new Vector2(-1, 0));
            yield return new WaitForSeconds(8);
            Assert.AreEqual(Quaternion.Euler(0, 0, -180).eulerAngles.z, api.GunRotation(0).eulerAngles.z, 2);

            api.TurnTowardsEmptySpace(new Vector2(10, 10));
            yield return new WaitForSeconds(8);
            Assert.AreEqual(Quaternion.Euler(0, 0, 45).eulerAngles.z, api.GunRotation(0).eulerAngles.z, 2);


            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTurretAI_Shooting()
        {
            gameGameObject.transform.position = new Vector3();
            TurretAPI api = gameGameObject.GetComponentInChildren<TurretAPI>();
            api.Fire(FireCommand.FireOneReady);
            yield return new WaitForSeconds(1);
            Assert.AreEqual(1, api.bulletsFired, "Expected 1 got " + api.bulletsFired);
            yield return new WaitForSeconds(1);

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

    }
}
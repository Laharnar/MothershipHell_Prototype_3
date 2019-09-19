using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SpaceShipTurretTests {
    public class TestAiming {

        GameObject go;
        RotatingTurretTop rtt;

        [UnityTest]
        public IEnumerator TestAimingDirections()
        {
            rtt.TurnToPoint(Vector2.up);
            Assert.AreEqual(Vector3.up, rtt.Direction);

            rtt.TurnToPoint(Vector2.right);
            Assert.AreEqual(Vector3.right, rtt.Direction);

            rtt.TurnToPoint(Vector2.one);
            Assert.AreEqual(new Vector3(1, 1, 0).normalized, rtt.Direction);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestAimingDegreesUp()
        {
            rtt.TurnToPoint(Vector2.up);
            Assert.AreEqual(90, rtt.Degrees);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestAimingDegreesRight()
        {
            rtt.TurnToPoint(Vector2.right);
            Assert.AreEqual(0, rtt.Degrees);

            yield return null;
        }
        [UnityTest]
        public IEnumerator TestAimingDegreesUpRight()
        {
            rtt.TurnToPoint(Vector2.one);
            Assert.AreEqual(45, rtt.Degrees);

            yield return null;
        }

        [SetUp]
        public void Setup()
        {

            go = new GameObject();
            rtt = go.AddComponent<RotatingTurretTop>();
            go.transform.position = Vector2.zero;

        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(go);

        }
    }
}

using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests_RotatingTurretTop {
    public class TestRotationMethodResults {

        RotatingTurretTop rot;
        GameObject go;

        Camera cam;

        // -----1
        [UnityTest]
        public IEnumerator TestTurnToPointBase()
        {
            go.transform.position = new Vector2();
            rot.TurnToPoint(Vector2.up);
            Assert.AreEqual(Vector2.up.normalized, rot.Direction);
            Assert.AreEqual(90, rot.Degrees);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TurnToPointMusOne()
        {
            go.transform.position = new Vector2();
            rot.TurnToPoint(-Vector2.one);
            Assert.AreEqual(-Vector2.one.normalized, rot.Direction);
            Assert.AreEqual(180+45== rot.Degrees || - 90-45==rot.Degrees, true);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TurnToPointMusOneMoved()
        {
            //test if movement ruins anything
            go.transform.position = new Vector2(1, 1);
            rot.TurnToPoint(-Vector2.one*10);
            Assert.AreEqual(-Vector2.one.normalized, rot.Direction);
            Assert.AreEqual(180 + 45 == rot.Degrees || -90 - 45 == rot.Degrees, true);

            yield return null;
        }
        // -----2
        [UnityTest]
        public IEnumerator TestTurnInDirection()
        {
            go.transform.position = new Vector2();
            rot.TurnInDirection(Vector2.up);
            Assert.AreEqual(Vector2.up.normalized, rot.Direction);
            Assert.AreEqual(90, rot.Degrees);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestTurnInDirectionMusOne()
        {
            go.transform.position = new Vector2();
            rot.TurnInDirection(-Vector2.one);
            Assert.AreEqual(-Vector2.one.normalized, rot.Direction);
            Assert.AreEqual(180 + 45 == rot.Degrees || -90 - 45 == rot.Degrees, true);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestTurnInDirectionMusOneMoved()
        {
            //test if movement ruins anything
            go.transform.position = new Vector2(1, 1);
            rot.TurnInDirection(-Vector2.one * 10);
            Assert.AreEqual(-Vector2.one.normalized, rot.Direction);
            Assert.AreEqual(180 + 45 == rot.Degrees || -90 - 45 == rot.Degrees, true);
            yield return null;
        }
        // -----3
        [UnityTest]
        public IEnumerator TestTurnToDegrees()
        {
            go.transform.position = new Vector2();
            rot.TurnToDegrees(90);
            Assert.AreEqual(Vector2.up.normalized, rot.Direction);
            Assert.AreEqual(90, rot.Degrees);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestTurnToDegreesMusOne()
        {
            go.transform.position = new Vector2();
            rot.TurnToDegrees(225);
            Assert.AreEqual(-Vector2.one.normalized, rot.Direction);
            Assert.AreEqual(180 + 45 == rot.Degrees || -90 - 45 == rot.Degrees, true);
            yield return null;
        }
        [UnityTest]
        public IEnumerator TestTurnToDegreesMusOneMoved()
        {
            //test if movement ruins anything
            go.transform.position = new Vector2(1, 1);
            rot.TurnToDegrees(225);
            Assert.AreEqual(-Vector2.one.normalized, rot.Direction);

            Assert.AreEqual(180 + 45 == rot.Degrees || -90 - 45 == rot.Degrees, true);
            yield return null;
        }


        [SetUp]
        public void Setup()
        {
            go = new GameObject();
            rot = go.AddComponent<RotatingTurretTop>();

            cam = new GameObject().AddComponent<Camera>();
            cam.tag = "MainCamera";
        }
        [TearDown]

        public void TearDown()
        {
            Object.Destroy(go);
            Object.Destroy(cam.gameObject);
        }

    }


}

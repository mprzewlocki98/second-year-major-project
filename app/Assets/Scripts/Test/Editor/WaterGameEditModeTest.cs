using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class WaterGameEditModeTest {

    [Test]
    public void AllObjectsPresent() { // to check that all the objects in the scene are there
        EditorSceneManager.OpenScene("Assets/Scenes/15-water-game", OpenSceneMode.Single);  

        GameObject glassCollider, water_dispenser, button, rectangle, glass, water1,
            water2, water3, water4, water5, water6, wellDone, water_B;

        glassCollider = GameObject.Find("glassCollider");
        water_dispenser = GameObject.Find("water_dispenser");
        button = GameObject.Find("button");
        rectangle = GameObject.Find("rectangle");
        glass = GameObject.Find("glass");
        water1 = GameObject.Find("water1");
        water2 = GameObject.Find("water2");
        water3 = GameObject.Find("water3");
        water4 = GameObject.Find("water4");
        water5 = GameObject.Find("water5");
        water6 = GameObject.Find("water6");
        wellDone = GameObject.Find("wellDone");
        water_B = GameObject.Find("water_B");

        Assert.IsNotNull(glassCollider);
        Assert.IsNotNull(water_dispenser);
        Assert.IsNotNull(button);
        Assert.IsNotNull(rectangle);
        Assert.IsNotNull(glass);
        Assert.IsNotNull(water1);
        Assert.IsNotNull(water2);
        Assert.IsNotNull(water3);
        Assert.IsNotNull(water4);
        Assert.IsNotNull(water5);
        Assert.IsNotNull(water6);
        Assert.IsNotNull(wellDone);
        Assert.IsNotNull(water_B);
    }

}

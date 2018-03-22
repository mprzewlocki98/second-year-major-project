using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class ScanGameEditModeTest {

	private GameObject back, cover, textPercentage, buttonContinue, panel,nextSceneScript;

	[SetUp]
	public void Init(){
		EditorSceneManager.OpenScene("Assets/Scenes/13-scan-game.unity", OpenSceneMode.Single);
		back = GameObject.Find("Back");
		cover = GameObject.Find("Cover");
		panel = GameObject.Find ("Panel");
		textPercentage = GameObject.Find("TextPercentage");
		buttonContinue = GameObject.Find("ButtonContinue");
		nextSceneScript = GameObject.Find ("NextSceneScript");
	}

	[Test]
	// Test all components are in the scene

	public void testAllComponentsPresent(){
		Assert.IsNotNull (back);
		Assert.IsNotNull (cover);
		Assert.IsNotNull (panel);
		Assert.IsNotNull (textPercentage);
		Assert.IsNull (buttonContinue);		// continue button is not existed right now
		buttonContinue = panel.transform.Find ("ButtonContinue").gameObject;
		Assert.False (buttonContinue.activeSelf);
		buttonContinue.SetActive (true);	// active ContinueButton
		Assert.IsNotNull (buttonContinue);
		Assert.IsNotNull(nextSceneScript);
	}

	[Test]
	// Test all components at the initial position

	public void testComponentsInitPosition(){
		Assert.AreEqual (-1.2f,back.GetComponent<Transform>().position.x);
		Assert.AreEqual (0.5f,back.GetComponent<Transform>().position.y);
		Assert.AreEqual (-0.5f,cover.GetComponent<Transform>().position.x);
		Assert.AreEqual (0.3f,cover.GetComponent<Transform>().position.y);
		Assert.AreEqual (-14.6f,panel.GetComponent<RectTransform>().anchoredPosition.x);
		Assert.AreEqual (-43.15f, panel.GetComponent<RectTransform> ().anchoredPosition.y);
		Assert.AreEqual  (24.4f,textPercentage.GetComponent<RectTransform>().anchoredPosition.x);
		Assert.AreEqual  (-7.4f, textPercentage.GetComponent<RectTransform> ().anchoredPosition.y);

		buttonContinue = panel.transform.Find ("ButtonContinue").gameObject;
		Assert.AreEqual  (2.7f,buttonContinue.GetComponent<RectTransform>().anchoredPosition.x);
		Assert.AreEqual  (-55.05f, buttonContinue.GetComponent<RectTransform> ().anchoredPosition.y);

	}

	[Test]
	// Test some gameobjects have their scripts

	public void testGameObjectContainsScripts(){
		Assert.IsNotNull(nextSceneScript.GetComponent<GoToNextSceneScan> ());
		Assert.IsNotNull(cover.GetComponent<DrawCanvas> ());
	} 
		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object{
    private static SSDirector _instance;
	public ISceneController currentSceneController { get; set;}
	public bool moving = false;
	public bool Paused { get { return Time.timeScale == 0; } set {Time.timeScale = value?0:1;} } 

	public static SSDirector getInstance(){
		if (_instance == null){
			_instance = new SSDirector();
		}
		return _instance;
	}

	public int getFPS(){
		return Application.targetFrameRate;
	}

	public void setFPS(int fps){
		Application.targetFrameRate = fps;
	}

	public void NextScene(){
		Debug.Log ("Waiting next Scene now...");
		#if UNITY_EDITOR  
		UnityEditor.EditorApplication.isPlaying = false;
		#else  
		Application.Quit();  
		#endif  
	}
}

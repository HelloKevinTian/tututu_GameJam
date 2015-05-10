using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	
	Texture2D Tu1;
	
	float Time1 = 0;

	private GameObject m_player;
	
	// Use this for initialization
	void Start () {
		m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void FixedUpdate () {
		Time1 += Time.deltaTime;
	}
	
	void OnGUI () {
		if (getTime (Time1) == "00:00:30") {
			Debug.Log(getTime(Time1));
			Destroy(m_player);
			Destroy(this.gameObject);
			return;
		}
		GUI.skin.label.fontSize = 40;

		GUI.Label(new Rect(400,450,161,62),getTime(Time1));

		GUIUtility.RotateAroundPivot (6*Time1, new Vector2(103, 200));
		GUI.DrawTexture(new Rect(100,100,6,100),Tu1);//秒针
		GUIUtility.RotateAroundPivot (-6*Time1, new Vector2(103, 200));
		
		GUIUtility.RotateAroundPivot (0.1f*Time1, new Vector2(103, 200));
		GUI.DrawTexture(new Rect(100,120,6,80),Tu1);//分针
		GUIUtility.RotateAroundPivot (-0.1f*Time1, new Vector2(103, 200));
		
		GUIUtility.RotateAroundPivot (0.1f/60*Time1, new Vector2(103, 200));
		GUI.DrawTexture(new Rect(100,140,6,60),Tu1);//时针
		GUIUtility.RotateAroundPivot (-0.1f/60*Time1, new Vector2(103, 200));
	}
	
	string getTime(float time){
		if(time < 0){
			return "00:00:00";
		}
		
		string lastTime = "";
		
		float hour = Mathf.FloorToInt(time/3600%24);
		
		if (hour/10 >=1){
			lastTime += "" + hour;
		}else{
			lastTime += "0" + hour;
		}
		
		float minute = Mathf.FloorToInt(time/60%60);
		
		if (minute/10 >=1){
			lastTime+=":" + minute;
		}else{
			lastTime +=":0" + minute;
		}
		
		float second = Mathf.FloorToInt(time%60);
		
		if (second/10 >=1){
			lastTime+=":" + second;
		}else{
			lastTime +=":0" + second;
		}
		
		return lastTime;
	}
}

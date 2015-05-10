using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class Button : MonoBehaviour {
	Button	button;

	public GameObject enemy;

	public float fireRate = 0.2f;

	private float nextFire = 0;

	private bool beginFlashEnemy = false;

	public GameObject countdown;

	GameObject image;

	void Start () 
	{
		Debug.Log (this.name);
//		countdown = GameObject.FindGameObjectWithTag("CountDown");

		Debug.Log (countdown);

		button = GetComponent<Button> ();

		EventTriggerListener.Get(button.gameObject).onClick =OnButtonClick;
	}
	
	private void OnButtonClick(GameObject go){
		//在这里监听按钮的点击事件
		if(go == button.gameObject){
			Debug.Log ("DoSomeThings");

			beginFlashEnemy = true;

			if (countdown) {
				countdown.SetActive(true);
			}

			image = GameObject.Find("Canvas/Button/Image");

			image.SetActive(false);
		}
	}
	
	void Update() {
		if (beginFlashEnemy && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			Instantiate(enemy, new Vector3(10,-1.7f,0), Quaternion.identity);
		}
	}
}
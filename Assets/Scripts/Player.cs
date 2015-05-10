using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {

    public float m_speed = 3;

	public float bullet_speed = 1;

    public float m_life = 10;

    public GameObject m_bullet;

	// 背景音乐
	public AudioClip m_musicClip;

	public AudioClip m_musicClip1;

	public AudioClip m_musicClip2;
	
	// 声音源
	protected AudioSource m_Audio;

    protected Transform m_transform;

	AudioClip effect;
	
	Transform targetTrans;

	bool is_right = true;

    float m_rocketRate = 0;

	struct aim_data{
		public Transform target;
		public Vector3 offset;
	};

	aim_data aimData;


    //protected AudioSource m_audio;

	SkeletonAnimation skeletonAnimation;

	SkeletonAnimation downSkeletonAnimation;

	// Use this for initialization
	void Start () {
		m_Audio = this.GetComponent<AudioSource>();

        m_transform = this.transform;

		skeletonAnimation = GetComponent<SkeletonAnimation> ();

		SkeletonAnimation[] cos = GetComponentsInChildren<SkeletonAnimation> ();
		foreach (SkeletonAnimation co in cos) {
			if (co != skeletonAnimation) {
				downSkeletonAnimation = co;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//主角移动
		move ();
		
		//主角发射子弹
		shoot ();
	}

	void move(){
		float offsetX = 0;

		if (Input.GetMouseButton (0)) {
			if (Input.mousePosition.x <= Screen.width * 0.5f) {
				if (is_right) {
					m_transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
					is_right = false;
					offsetX += m_speed * Time.deltaTime;
				} else {
					offsetX += m_speed * Time.deltaTime;
				}
				downSkeletonAnimation.state.AddAnimation (0, "move", false, 0);
			} else {
				if (!is_right) {
					m_transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
					is_right = true;
					offsetX -= m_speed * Time.deltaTime;
				} else {
					offsetX += m_speed * Time.deltaTime;
				}
				downSkeletonAnimation.state.AddAnimation (0, "move", false, 0);
			}
		}
		
		m_transform.Translate( new Vector3( offsetX, 0, 0 ) );
	}

	void shoot(){
		m_rocketRate -= Time.deltaTime;
		if ( m_rocketRate <= 0 )
		{
			//子弹发射频率
			m_rocketRate = 0.10f;
			
			GameObject enemy = GameObject.FindGameObjectWithTag("Enemy") as GameObject;
			
			if (enemy != null) {
				aim_data aimData = aim ();

				GameObject bullet = Instantiate( m_bullet, m_transform.position + aimData.offset, m_transform.rotation ) as GameObject;
				//sound
				float val = Random.value;

				if (val < 0.33f) {
					effect = m_musicClip;
				} else if (val >= 0.33f && val < 0.67f) {
					effect = m_musicClip1;
				} else if (val >= 0.67f) {
					effect = m_musicClip2;
				}

				m_Audio.PlayOneShot(effect);

				Rocket rocket = bullet.GetComponent<Rocket>();
				rocket.target = targetTrans;
			} else {
				//Debug.Log("############No Enemy##############");
			}
		}
	}

	aim_data aim(){
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy") as GameObject[];

		float min_distance = 9999;

		foreach (GameObject enemy in enemys) {
			Transform enemyTrans = enemy.transform;
			float distance = Vector3.Distance(enemyTrans.position,m_transform.position);
			if (distance < min_distance) {
				min_distance = distance;
				targetTrans = enemy.transform;
				aimData.target = enemy.transform;
			}
		}

		if (enemys.Length > 0) {

//			if (m_transform.position.x <= targetTrans.position.x) {
//				m_transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
//			} else {
//				m_transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
//			}

			float angle = Mathf.Asin (Mathf.Abs (targetTrans.position.y - m_transform.position.y) / min_distance);

			float step0 = 0;
			float step1 = 5 * Mathf.PI / 180;
			float step2 = 15 * Mathf.PI /180;
			float step3 = 25 * Mathf.PI /180;
			float step4 = 35 * Mathf.PI /180;
			float step5 = 45 * Mathf.PI /180;
			float step6 = 55 * Mathf.PI /180;
			float step7 = 65 * Mathf.PI /180;
			float step8 = 75 * Mathf.PI /180;
			float step9 = 85 * Mathf.PI /180;
			float step10 = Mathf.PI / 2;

//			Debug.Log(targetTrans.position+"   "+m_transform.position+"   "+min_distance);
//			Debug.Log(Mathf.Abs (targetTrans.position.y - m_transform.position.y));
//			Debug.Log("angle :   " + angle*180/Mathf.PI);

			if (angle >= step0 && angle < step1){ //fire_0
				skeletonAnimation.state.AddAnimation (0, "fire_0", false, 0);
				aimData.offset = new Vector3 (0.51f, 0f, 0);
			} else if (angle >= step1 && angle < step2){
				skeletonAnimation.state.AddAnimation (0, "fire_10", false, 0);
				aimData.offset = new Vector3 (1.76f, -0.36f, 0);
			} else if (angle >= step2 && angle < step3){
				skeletonAnimation.state.AddAnimation (0, "fire_20", false, 0);
				aimData.offset = new Vector3 (1.76f, -0.24f, 0);
			} else if (angle >= step3 && angle < step4){
				skeletonAnimation.state.AddAnimation (0, "fire_30", false, 0);
				aimData.offset = new Vector3 (1.71f, -0.12f, 0);
			} else if (angle >= step4 && angle < step5){
				skeletonAnimation.state.AddAnimation (0, "fire_40", false, 0);
				aimData.offset = new Vector3 (1.66f, 0, 0);
			} else if (angle >= step5 && angle < step6){
				skeletonAnimation.state.AddAnimation (0, "fire_50", false, 0);
				aimData.offset = new Vector3 (1.54f, 0.14f, 0);
			} else if (angle >= step6 && angle < step7){
				skeletonAnimation.state.AddAnimation (0, "fire_60", false, 0);
				aimData.offset = new Vector3 (1.51f, 0.24f, 0);
			} else if (angle >= step7 && angle < step8){
				skeletonAnimation.state.AddAnimation (0, "fire_70", false, 0);
				aimData.offset = new Vector3 (1.36f, 0.31f, 0);
			} else if (angle >= step8 && angle < step9){
				skeletonAnimation.state.AddAnimation (0, "fire_80", false, 0);
				aimData.offset = new Vector3 (1.21f, 0.36f, 0);
			} else if (angle >= step9 && angle < step10){
				skeletonAnimation.state.AddAnimation (0, "fire_90", false, 0);
				aimData.offset = new Vector3 (1.03f, 0.37f, 0);
			}
		}
		aimData.offset = new Vector3 (0, 0, 0);
		return aimData;
	}

	//主角被对象other碰撞
    void OnTriggerEnter(Collider other)
    {
//		Debug.Log ("-----------------Who trggered Player: " + other.name);
//        if (other.tag.CompareTo("Enemy") == 0)
//        {
//            m_life -= 1;
//
//            if (m_life <= 0) 
//            {
//				Destroy(this.gameObject);
//
//				Debug.Log("Game Over!");
//            }
//        }
    }


}

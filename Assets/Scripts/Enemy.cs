using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour {
	
    public float m_life = 5;

    protected Transform m_transform;

	SkeletonAnimation skeletonAnimation;

	private Vector3 riseVec = new Vector3 (10, -1.7f, 0);

	private Vector3 setVec = new Vector3(8,-1.7f,0);
	
	public float jumpTime = 1.0f;
	
	private float startTime;
	
	private Vector3 offset = new Vector3(-2,0,0);

	private float attackDistance = 3;

	GameObject gamemgr;

	public GameObject dieEffect;

	// 背景音乐
	public AudioClip m_musicClip;
	
	public AudioClip m_musicClip1;
	
	public AudioClip m_musicClip2;

	public AudioClip m_musicClip3;

	public AudioClip m_musicClip4;

	public AudioClip m_musicClip5;

	public AudioClip m_musicClip6;

	public AudioClip m_musicClip7;

	public AudioClip m_musicClip8;
	
	// 声音源
	protected AudioSource m_Audio;
	
	AudioClip effect;
	
	// Use this for initialization
	void Start () {
		m_Audio = this.GetComponent<AudioSource>();

		skeletonAnimation = GetComponent<SkeletonAnimation>();

        m_transform = this.transform;

		//for jump
		startTime = Time.time;

		gamemgr = GameObject.FindGameObjectWithTag("GameMgr");

		StartCoroutine (nextJump (jumpTime + 1));
	}
	
	// Update is called once per frame
	void Update () {
		jump ();
		if (m_transform.position.x <= -10) {
			Destroy(this.gameObject);
		}
	}

	IEnumerator nextJump (float delay) {
		skeletonAnimation.state.AddAnimation (0, "idle", false, 0);
		yield return new WaitForSeconds (delay);
		
		startTime = Time.time;
		
		riseVec = riseVec + offset;

		setVec = setVec + offset;

		skeletonAnimation.state.AddAnimation (0, "jump", false, 0);

		StartCoroutine (nextJump (jumpTime + 1));
	}
	
	void jump () {
		Vector3 center = (riseVec + setVec) * 0.5F;

		center -= new Vector3(0, 0.01f, 0);

		Vector3 riseRelCenter = riseVec - center;

		Vector3 setRelCenter = setVec - center;

		float fracComplete = (Time.time - startTime) / jumpTime;

//		Debug.Log ("fracComplete:  " + fracComplete);

		transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);

		transform.position += center;
	}

    void OnTriggerEnter (Collider other)
    {
		Debug.Log ("============Who triggered enemy: " + other.tag);
        if (other.tag.CompareTo("Bullet") == 0)
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                m_life -= rocket.m_power;

                if (m_life == 0)
                {
					if (gamemgr) {
						GameManager mgr = gamemgr.GetComponent<GameManager>();
						mgr.AddScore(1);
					}

					if (dieEffect) {
						GameObject go = Instantiate(dieEffect,m_transform.position + new Vector3(0,0.6f,-3),m_transform.rotation) as GameObject;

						Destroy(go,1);
					}

					//sound
					float val = Random.value;
					
					if (val < 0.1f) {
						effect = m_musicClip;
					} else if (val >= 0.1f && val < 0.2f) {
						effect = m_musicClip1;
					} else if (val >= 0.2f && val < 0.3f) {
						effect = m_musicClip2;
					} else if (val >= 0.3f && val < 0.4f) {
						effect = m_musicClip3;
					} else if (val >= 0.4f && val < 0.5f) {
						effect = m_musicClip4;
					} else if (val >= 0.5f && val < 0.6f) {
						effect = m_musicClip5;
					} else if (val >= 0.6f && val < 0.7f) {
						effect = m_musicClip6;
					} else if (val >= 0.7f && val < 0.8f) {
						effect = m_musicClip7;
					} else if (val >= 0.8f) {
						effect = m_musicClip8;
					}

					m_Audio.PlayOneShot(effect);

                    Destroy(this.gameObject,1);
                }
			} else {
				Debug.Log("rocket is null ..............");
			}
        }
    }
}

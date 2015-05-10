using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour {

    public float m_speed = 10;

    public float m_liveTime = 1;

    public float m_power = 1f;

	public Transform target;

	public GameObject effect1;

	public GameObject effect2;

	public GameObject m_explosionFX;

    protected Transform m_transform;

	float x_speed = 0;

	float y_speed = 0;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;

		if (target) {
			float x_offset = Mathf.Abs(target.position.x - m_transform.position.x);
			float y_offset = Mathf.Abs(target.position.y - m_transform.position.y);
			float distance = Vector3.Distance(target.position,m_transform.position);
			x_speed = m_speed * Time.deltaTime;
			y_speed = x_speed * y_offset / x_offset;

			Transform bulletChild = m_transform.FindChild("Rocket");
			if (bulletChild){
				float angle = Mathf.Asin(y_offset / distance) * 180 * Random.Range(0.91f,1.09f) / Mathf.PI;
				bulletChild.localRotation = Quaternion.Euler(new Vector3(angle, 270, 0));
			}

			//load effect
			GameObject effect;
			if (Random.value >= 0.5) {
				effect = effect1;
			} else {
				effect = effect2;
			}
			GameObject effectObj= Instantiate( effect, m_transform.position, m_transform.rotation ) as GameObject;
			Destroy(effectObj,1);
		}
	}
	
	// Update is called once per frame
	void Update () {
        m_liveTime -= Time.deltaTime;

        if (m_liveTime <= 0)
            Destroy(this.gameObject);

		if (target) {
			m_transform.Translate( new Vector3( x_speed, y_speed, 0 ) );
		}
	}

    void OnTriggerEnter(Collider other)
    {
//		Debug.Log ("============Who triggered bullet: " + other.name);
        if (other.tag.CompareTo("Enemy")!=0)
            return;

		GameObject fx = Instantiate(m_explosionFX, other.transform.position, Quaternion.identity) as GameObject;
		Destroy (fx, 1);
        Destroy(this.gameObject);
    }
}

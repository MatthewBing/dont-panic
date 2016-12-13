using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;
	public Texture2D m_guiTexture;
    public bool isPushed;

	// Use this for initialization
	void Start () 
	{
        isPushed = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPushed)
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
            isPushed = false;
		}
	}


}

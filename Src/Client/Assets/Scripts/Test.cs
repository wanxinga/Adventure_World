using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
    {
		Debug.LogError("触发碰撞");
    }

	void OnCollisionEnter(Collision other)
    {
		Debug.LogError("触发碰撞");
	}

	void OnTriggerStay(Collider other)
	{
		Debug.LogError("触发碰撞");
	}

	void OnCollisionStay(Collision other)
	{
		Debug.LogError("触发碰撞");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
	public GameObject wall;
	public MeshRenderer meshRenderer;
	public Texture texture;

	void Start() {
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void ChangeTextur() {
		meshRenderer.material.SetTexture("загружено(2)", texture);
	}
	public void OnTriggerEnter(Collider col) {
		
		if (col.tag == "Player") {
			ChangeTextur();
		}

	}
}

﻿using UnityEngine;
using System.Collections;

public class SelectableCity : SelectableLandmark {

	public string message;

	public Mesh deselectedMesh;
	public Material deselectedMeshMaterial;

	public Mesh selectedMesh;
	public Material selectedMeshMaterial;

	private GameObject itinerary;
	private Vector3 startPos;
	private float minSwipeDistY = 400.0f;

	// Use this for initialization
	void Start () {
		itinerary = GameObject.Find ("ItineraryPlane");
		GetComponent<MeshFilter> ().mesh = deselectedMesh;
		GetComponent<MeshRenderer> ().materials = new Material[]{deselectedMeshMaterial};
		transform.localScale = new Vector3(0.1f,0.1f,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isSelected) {

			if (Input.touchCount != 0) {
				Touch touch = Input.GetTouch (0);
				switch (touch.phase) {
				case TouchPhase.Began:
					startPos = touch.position;
					break;
				case TouchPhase.Ended:
					float swipeDistVertical = 
						(new Vector3 (0, touch.position.y, 0) -
						new Vector3 (0, startPos.y, 0)).magnitude;
					if (swipeDistVertical > minSwipeDistY) {
						itinerary.SendMessage("Add",message);
					}
					break;
				default:
					break;
				}
			}
		}
	}

	public override void OnSelect() {
		GetComponent<MeshFilter> ().mesh = selectedMesh;
		GetComponent<MeshRenderer> ().materials = new Material[]{selectedMeshMaterial};
		transform.localScale = new Vector3(0.5f,0.5f,0.5f);
	}

	public override void OnDeselect() {
		GetComponent<MeshFilter> ().mesh = deselectedMesh;
		GetComponent<MeshRenderer> ().materials = new Material[]{deselectedMeshMaterial};
		transform.localScale = new Vector3(0.1f,0.1f,0.1f);
	}


}

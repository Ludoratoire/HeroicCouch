using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    public List<GameObject> characters;

    public List<GameObject> rooms;

    public CameraController cameraController;

	void Awake () {
        rooms.ForEach(r => r.GetComponent<NavMeshSurface>().BuildNavMesh());
        cameraController.targets = characters;
	}
	
	void Update () {

		if (Input.GetButtonDown("CameraType"))
        {
            cameraController.CycleType();
        }

        if(Input.GetButtonDown("CameraTarget"))
        {
            cameraController.CycleTarget();
        }
	}
}

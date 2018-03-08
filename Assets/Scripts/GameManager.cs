using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public List<GameObject> characters;

    public List<GameObject> rooms;

    public CameraController cameraController;

    GameObject GetRandomRoom()
    {
        return rooms[Random.Range(0, rooms.Count)];
    }

    void Awake () {
        rooms.ForEach(r => r.GetComponent<NavMeshSurface>().BuildNavMesh());
        cameraController.targets = characters;

        characters.ForEach(c =>
        {
            var room = GetRandomRoom();
            var controller = c.GetComponent<CharacterCustomController>();
            controller.AddTarget(room, TargetType.Destination);
            Observable.EveryUpdate()
                .Where(_ => controller.NoMoreTarget)
                .Subscribe(_ => controller.AddTarget(GetRandomRoom(), TargetType.Destination)).AddTo(controller);
        });
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

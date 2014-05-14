﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{	
		public GameObject prefab;
		public int maxCount = 100;
		public int prepareCount = 0;
		[SerializeField]
		private int
				interval = 1;
		private List<GameObject> pooledObjectList = new List<GameObject> ();
		private static GameObject poolAttachedObject = null;
	
		void OnEnable ()
		{
				if (interval > 0)
						StartCoroutine (RemoveObjectCheck ());
		}

		void Start ()
		{
				for (int i=0; i<maxCount; i++) {
						GetInstance ();
				}
				foreach (var item in pooledObjectList) {
						item.SetActive (false);
				}
		}
	
		void OnDisable ()
		{
				StopAllCoroutines ();
		}
	
		public void OnDestroy ()
		{
				if (poolAttachedObject == null)
						return;
		
				if (poolAttachedObject.GetComponents<ObjectPool> ().Length == 1) {
						poolAttachedObject = null;
				}
				foreach (var obj in pooledObjectList) {
						Destroy (obj);
				}
				pooledObjectList.Clear ();
		}
	
		public int Interval {
				get {
						return interval;
				}
				set {
						if (interval != value) {
								interval = value;
				
								StopAllCoroutines ();
								if (interval > 0)
										StartCoroutine (RemoveObjectCheck ());
						}
				}
		}
	
		public GameObject GetInstance ()
		{
				return GetInstance (transform);
		}
	
		public GameObject GetInstance (Transform parent)
		{
				pooledObjectList.RemoveAll ((obj) => obj == null);
		
				foreach (GameObject obj in pooledObjectList) {
						if (obj.activeSelf == false) {
								obj.SetActive (true);
								return obj;	
						}
				}
		
				if (pooledObjectList.Count < maxCount) {
						GameObject obj = (GameObject)GameObject.Instantiate (prefab);
						obj.SetActive (true);
						obj.transform.parent = parent;
						pooledObjectList.Add (obj);
						return obj;
				}
		
				return null;
		}
	
		IEnumerator RemoveObjectCheck ()
		{
				while (true) {
						RemoveObject (prepareCount);
						yield return new WaitForSeconds (interval);
				}
		}
	
		public void RemoveObject (int max)
		{
				if (pooledObjectList.Count > max) {
						int needRemoveCount = pooledObjectList.Count - max;
						foreach (GameObject obj in pooledObjectList.ToArray()) {
								if (needRemoveCount == 0) {
										break;
								}
								if (obj.activeSelf == false) {
										pooledObjectList.Remove (obj);
										Destroy (obj);
										needRemoveCount --;
								}
						}
				}
		}
	
		public static ObjectPool GetObjectPool (GameObject obj)
		{
				if (poolAttachedObject == null) {
						poolAttachedObject = GameObject.Find ("GameController");
						if (poolAttachedObject == null) {
								poolAttachedObject = new GameObject ("GameController");
						}
				}
		
				foreach (var pool in poolAttachedObject.GetComponents<ObjectPool>()) {
						if (pool.prefab == obj) {
								return pool;
						}
				}
		
				foreach (var pool in FindObjectsOfType<ObjectPool>()) {
						if (pool.prefab == obj) {
								return pool;
						}
				}
		
				var newPool = poolAttachedObject.AddComponent<ObjectPool> ();
				newPool.prefab = obj;
				return newPool;
		}
}
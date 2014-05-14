using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

		public GameObject bulletPrefab;
		private ObjectPool bulletPool;

		void Start ()
		{
				InvokeRepeating ("Shot", 2, 0.1f);
				//bulletPool = ObjectPool.GetObjectPool (bulletPrefab);
		}

		void Shot ()
		{
				var bullet = (GameObject)GameObject.Instantiate (bulletPrefab);
				//var bullet = bulletPool.GetInstance ();
				bullet.transform.position = transform.position;
				bullet.transform.rotation = transform.rotation;
		}
}

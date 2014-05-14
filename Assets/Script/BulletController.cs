using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
		public float speed = 100;

		public float time = 0;

		private float activeTime = 0;

		public bool isChangeMaterialColor = false;

		Vector3 direction;

		public void OnEnable ()
		{
				activeTime = Time.timeSinceLevelLoad + time;
		}

		void Start ()
		{
				if (isChangeMaterialColor)
						renderer.material.color = Color.red;

				direction = (transform.up * -1) * speed;
				Destroy (gameObject, time);
		}

		void Update ()
		{
				transform.position += direction * GameController.Instance.deltaTime;
		}
}

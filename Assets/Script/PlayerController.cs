using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

		private SphereCollider _collider;
		private Camera _camera;

		void Start ()
		{
				_collider = GetComponent<SphereCollider> ();
				_collider.enabled = false;

				_camera = Camera.main;
		}


		void Update ()
		{
				if (Input.GetMouseButton (0)) {
					
						var position = _camera.ScreenToWorldPoint (Input.mousePosition);
						transform.position = new Vector3 (position.x, position.y, 0);

				}

				if (Physics.CheckSphere (transform.position, _collider.radius)) {
						Destroy (gameObject);
						GameController.Instance.GameOver ();
				} else if (Physics.CheckSphere (transform.position, 0.3f)) {
						GameController.Instance.score += 1;
				}
		}
}

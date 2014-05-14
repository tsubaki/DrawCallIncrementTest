using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public static GameController Instance {
				get {
						if (instance == null) {
								instance = GameObject.FindObjectOfType<GameController> ();
						}
						return instance;
				}
		}

		void Awake ()
		{
				GameController currentInstance = Instance;
				if (currentInstance != this) {
						Destroy (this);
				}
		}

		public float time = 0;
		public float deltaTime = 0;

		void Update ()
		{
				time = Time.timeSinceLevelLoad;
				deltaTime = Time.deltaTime;
		}

		private static GameController instance;

		public int score = 0;

		public void GameOver ()
		{
				Invoke ("Restart", 3);
		}

		public void Restart ()
		{
				Application.LoadLevel (Application.loadedLevel);
		}

		void OnGUI ()
		{
				GUILayout.Label ("score:" + score.ToString ());
		}
}

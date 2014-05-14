using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{

		void OnGUI ()
		{
				if (GUILayout.Button ("draw call higher", GUILayout.Height (100))) {
						Application.LoadLevel (0);
				}
				if (GUILayout.Button ("draw call lower", GUILayout.Height (100))) {
						Application.LoadLevel (1);
				}
		}
}

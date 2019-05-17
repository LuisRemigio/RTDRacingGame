using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
	[SerializeField] float beamSpeed;
	[SerializeField] float maxDistance;
	[SerializeField] bool hookisFired = false;
	[SerializeField] Vector3 hookOriginalPosition;
	[SerializeField] GameObject gameOjbect;

	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		//if collider is not shot, shoot out collider

        if (!hookisFired)
		{
			hookisFired = true;
			hookOriginalPosition = transform.localPosition;
			//if the collider is still in range of maxdistance, continue moving.
			if (Vector3.Distance(hookOriginalPosition, transform.localPosition) < maxDistance)
			{
				gameObject.transform.position += gameObject.transform.forward * beamSpeed;
			}
			else //if collider goes beyond range, put it back.
			{
				resetHook();
			}
		}
    }

	private void resetHook()
	{
		//make the hook go back to it's original position.
		hookisFired = false;
		//transform.position = transform.localPosition;
		transform.localPosition = hookOriginalPosition;
		
	}

	private void OnTriggerEnter(Collider colliderGameObject)
	{
		if (colliderGameObject.tag == "Track")
		{
			RaycastHover hoverController = gameObject.transform.parent.gameObject.GetComponent<RaycastHover>();
			hoverController.GoToHook(gameObject.transform.position, colliderGameObject);
			resetHook();
		}
	}
}

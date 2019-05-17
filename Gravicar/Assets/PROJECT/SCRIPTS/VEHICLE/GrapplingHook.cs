using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
	[SerializeField] float beamSpeed;
	[SerializeField] float maxDistance;
	[SerializeField] bool hookIsFired = false;
	[SerializeField] Vector3 hookOriginalPosition;
	[SerializeField] GameObject gameOjbect;

    float hookTimer;

	// Start is called before the first frame update
	void Start()
    {
		
    }

	public void FireHook()
	{
        if(!hookIsFired)
        {
            hookOriginalPosition = transform.localPosition;
            hookIsFired = true;
            Debug.Log("Hook Fired");
        }
        else
        {
            Debug.Log("Hook is already fired");
        }

	}

    // Update is called once per frame
    void Update()
    {
		//if collider is not shot, shoot out collider

        if (hookIsFired)
		{            
			
			//if the collider is still in range of maxdistance, continue moving.
			if (Vector3.Distance(hookOriginalPosition, transform.localPosition) < maxDistance)
			{
				gameObject.transform.position += gameObject.transform.forward * beamSpeed * Time.deltaTime;       
            }
			else //if collider goes beyond range, put it back.
			{
				resetHook();
			}
		}

        if (Input.GetKeyDown(KeyCode.R))
        {
            FireHook();
        }

        if(hookTimer < 8)
        {
            hookTimer += Time.deltaTime;
        }
        else
        {
            resetHook();
        }
    }

	private void resetHook()
	{
		//make the hook go back to it's original position.
		hookIsFired = false;
		//transform.position = transform.localPosition;
		transform.localPosition = hookOriginalPosition;
        hookTimer = 0;

    }

	private void OnTriggerStay(Collider colliderGameObject)
	{
		if (colliderGameObject.tag == "Track")
		{
			RaycastHover hoverController = gameObject.transform.parent.gameObject.GetComponent<RaycastHover>();
			hoverController.GoToHook(gameObject.transform.position, colliderGameObject.transform.gameObject);
            Debug.Log("Hook Hit True");
			resetHook();
		}
	}
}

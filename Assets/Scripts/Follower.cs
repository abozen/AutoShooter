using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
	[SerializeField] Transform target;

	[SerializeField] float smoothSpeed = 125f;
	[SerializeField] Vector3 offset;
	[SerializeField] private bool lookAtTarget;


	void Update()
	{
        
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;

		if (lookAtTarget)
			transform.LookAt(target);
	}

}

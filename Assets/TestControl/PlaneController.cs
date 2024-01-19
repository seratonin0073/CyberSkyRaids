using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    private float
        flySpeed = 50f,
        tangazhSpeed = 0.1f;
    private float horizontalMovement;
    private float Amount = 120;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * flySpeed * Time.fixedDeltaTime);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        horizontalMovement += horizontal * Amount * Time.fixedDeltaTime;
        float verticalMovement = Mathf.Lerp(0, 30, Mathf.Abs(vertical)) * Mathf.Sign(vertical);
        float roll = Mathf.Lerp(0, 40, Mathf.Abs(horizontal)) * -Mathf.Sign(horizontal);
        Debug.Log(Mathf.Sign(10));

        transform.localRotation = Quaternion.Euler(Vector3.up * horizontalMovement + Vector3.right * verticalMovement + Vector3.forward * roll);
    }

}

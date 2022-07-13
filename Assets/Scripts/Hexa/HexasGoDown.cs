using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexasGoDown : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float strength;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 offset;
    int cycle;

    void Start()
    {
        StartCoroutine(UpdateHexaHeight());

    }

    IEnumerator UpdateHexaHeight()
    {
        while (true)
        {
            Collider[] hexas = Physics.OverlapSphere(transform.position + offset, radius, layerMask);
            foreach(Collider nearby in hexas)
            {
                float distance = Vector3.Distance(transform.position, nearby.transform.position);

                Vector3 hexaScale = nearby.transform.localScale;
                nearby.transform.localScale = new Vector3(hexaScale.x, hexaScale.y, Mathf.Pow(distance / radius, 1.3f) * strength);
            }
            cycle++;
            Debug.Log("cycle number " + cycle);

            //yield return new WaitForSeconds(.1f);
            yield return new WaitForEndOfFrame();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}

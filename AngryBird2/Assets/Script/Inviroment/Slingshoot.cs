using UnityEngine;
using System.Collections;
// using Assets.Scripts;
using System;

public class Slingshoot : MonoBehaviour
{

    public static Slingshoot instance;
    private void Awake()
    {
        instance = this;
    }
    public LineRenderer SlingshotLineRenderer1;
    public LineRenderer SlingshotLineRenderer2;

    //this linerenderer will draw the projected trajectory of the thrown bird
    public GameObject BirdToThrow;

    [Header("Location Create AngryBird")]
    public Transform L;
    public Transform R;
    public Vector3 Mid_locationAngryBirdStart;

    // Use this for initialization
    void Start()
    {
        if (CameraFollow.instance.BirdToFollow != null)
        {
            BirdToThrow = CameraFollow.instance.BirdToFollow;
        }
        Mid_locationAngryBirdStart = (L.position + R.position) / 2;

        // SlingshotLineRenderer1.sortingLayerName = "LineRenedrer";
        // SlingshotLineRenderer2.sortingLayerName = "LineRenedrer";
        SlingshotLineRenderer1.material = mtel[0];
        SlingshotLineRenderer2.material = mtel[0];
        SlingshotLineRenderer1.SetPosition(0, L.position);
        SlingshotLineRenderer2.SetPosition(0, R.position);
        SlingshotLineRenderer1.SetPosition(1, Mid_locationAngryBirdStart);
        SlingshotLineRenderer2.SetPosition(1, Mid_locationAngryBirdStart);
        SetSlingshotLineRenderersActive(false);
    }

    void Update()
    {
        if (CameraFollow.instance.BirdToFollow != null && GameController.instance.stateSlingshoot == Enums.StateSlingshot.BeforeShoot)
        {
            BirdToThrow = CameraFollow.instance.BirdToFollow;
            DisplaySlingshotLineRenderers();
        }
        if (GameController.instance.stateSlingshoot == Enums.StateSlingshot.Shoot)
        {
            SlingshotLineRenderer1.SetPosition(1, Mid_locationAngryBirdStart);
            SlingshotLineRenderer2.SetPosition(1, Mid_locationAngryBirdStart);
            SlingshotLineRenderer1.material = mtel[0];
            SlingshotLineRenderer2.material = mtel[0];

        }

    }

    public void DisplaySlingshotLineRenderers()
    {
        SlingshotLineRenderer1.SetPosition(1, BirdToThrow.transform.position);
        SlingshotLineRenderer2.SetPosition(1, BirdToThrow.transform.position);
        MaterialsOfLine();

    }

    public void SetSlingshotLineRenderersActive(bool active)
    {
        SlingshotLineRenderer1.enabled = active;
        SlingshotLineRenderer2.enabled = active;

    }
    public Material[] mtel;// mtel[0] = den // mtel[1]= do
    public void MaterialsOfLine()
    {
        if (CameraFollow.instance.BirdToFollow != null)
        {
            if (Vector3.Distance(CameraFollow.instance.BirdToFollow.transform.position, Mid_locationAngryBirdStart) > 1f && Vector3.Distance(CameraFollow.instance.BirdToFollow.transform.position, Mid_locationAngryBirdStart) < 2f)
            {
                SlingshotLineRenderer1.material = mtel[0];
                SlingshotLineRenderer2.material = mtel[0];
            }
            else
            {
                SlingshotLineRenderer1.material = mtel[1];
                SlingshotLineRenderer2.material = mtel[1];
            }
        }
    }

}

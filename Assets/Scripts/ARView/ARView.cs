using System.Collections.Generic;
using UnityEngine;
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.External;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;
using Niantic.ARDK.Utilities.Logging;


public class ARView : MonoBehaviour
{
    
    
    public Camera Camera;

    [EnumFlagAttribute]
    public ARHitTestResultType HitTestType = ARHitTestResultType.ExistingPlane;

    public GameObject PlacementObjectPf;

    private List<GameObject> _placedObjects = new List<GameObject>();
    
    private IARSession _session;
    private bool isAddable = true;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        ARSessionFactory.SessionInitialized += OnAnyARSessionDidInitialize;
    }
    
    
    private void OnAnyARSessionDidInitialize(AnyARSessionInitializedArgs args)
    {
        _session = args.Session;
        _session.Deinitialized += OnSessionDeinitialized;
    }

    private void OnSessionDeinitialized(ARSessionDeinitializedArgs args)
    {
        ClearObjects();
    }

    private void OnDestroy()
    {
        ARSessionFactory.SessionInitialized -= OnAnyARSessionDidInitialize;

       _session = null;

        ClearObjects();
    }

    private void ClearObjects()
    {
        foreach (var placedObject in _placedObjects)
        {
            Destroy(placedObject);
        }

        _placedObjects.Clear();
    }


    // Update is called once per frame
    void Update()
    {
        if (_session == null)
        {
            return;
        }

        if (PlatformAgnosticInput.touchCount <= 0)
        {
            return;
        }

        var touch = PlatformAgnosticInput.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (isAddable)
            {
                TouchBegan(touch);
            }

            
        }
    }


    private void TouchBegan(Touch touch)
    {
        var currentFrame = _session.CurrentFrame;
        if (currentFrame == null)
        {
            return;
        }

        var results = currentFrame.HitTest
        (
            Camera.pixelWidth,
            Camera.pixelHeight,
            touch.position,
            HitTestType
        );

        int count = results.Count;
        Debug.Log("Hit test results: " + count);

        if (count <= 0)
            return;

        // Get the closest result
        var result = results[0];

        var hitPosition = result.WorldTransform.ToPosition();

        _placedObjects.Add(Instantiate(PlacementObjectPf, hitPosition, Quaternion.identity));

        //make isAddable = false;
        isAddable = false;
        
        var anchor = result.Anchor;
        Debug.LogFormat
        (
            "Spawning cube at {0} (anchor: {1})",
            hitPosition.ToString("F4"),
            anchor == null
                ? "none"
                : anchor.AnchorType + " " + anchor.Identifier
        );
    }
    
    
    
    public void BackCLick()
    {
        GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_PreviewProduct);
    }

    public void AddModelClick()
    {
        isAddable = true;
        
        Splash._ShowAndroidToastMessage("You can click on the plane to spawn a furniture");

    }
}

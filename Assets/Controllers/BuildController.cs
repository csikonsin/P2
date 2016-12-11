using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildController : MonoBehaviour {


	public static BuildController Instance;

	Building building;



	RaycastHit hit;
	bool isRayHit = false;
	Ray ray;


	GameObject buildGhostImage;



	// Use this for initialization
	void Start () {
		 building = new Building();

		if(Instance!=null)
		{
			Debug.Log("There should not be more than 1 BuildController instances");
			return;
		}
		Instance = this;

	}

	// Update is called once per frame

	



	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		isRayHit = Physics.Raycast(ray, out hit, 100);
		
		
		if(isRayHit)
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (Input.GetMouseButtonDown(0))
				{

					//Build
					if (building.BuildType == null || building.BuildType == "") return;
					Debug.Log("hit");

					WorldController.Instance.BuildBlock(hit, building.BuildType);
				}

				if (buildGhostImage != null)
				{
					WorldController.Instance.UpdateGhostImage(hit,buildGhostImage);
				}
			}
		}
	}


	public void SetBuildType(string type)
	{
        if (building.BuildType == type)
        {
            building.BuildType = null;
            Destroy(buildGhostImage);
        }
        else
        {
            building.BuildType = type;
            if (buildGhostImage != null)
            {
                Destroy(buildGhostImage);
            }
            buildGhostImage = WorldController.Instance.SetGhostImage(type);
        }
	}

}

public class Building
{
	public string BuildType { get; set; }
}
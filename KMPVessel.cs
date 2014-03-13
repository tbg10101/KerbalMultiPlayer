using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KMP
{
    public class KMPVessel
    {

        //Properties

		public KMPVesselInfo info;

		public String vesselName
		{
			private set;
			get;
		}

		public String ownerName
		{
			private set;
			get;
		}

		public Guid id
		{
			private set;
			get;
		}

		public Orbit orbit
		{
			private set;
			get;
		}

		public Orbit currentOrbit {
			get {
				//Orbit doesn't have a copy constructor...
				Orbit tempOrbit = new Orbit (orbit.inclination, orbit.eccentricity, orbit.semiMajorAxis, orbit.LAN, orbit.argumentOfPeriapsis, orbit.meanAnomalyAtEpoch, orbit.epoch, orbit.referenceBody);
                //Use the orbit driver to update it.
                OrbitDriver od = new OrbitDriver();
                od.orbit = tempOrbit;
                od.UpdateOrbit();
				//Update the orbit
				return tempOrbit;
			}
		}

        //orbitWorld methods are for player triangle positions.
        //We transform them back onto our planet, instead of where kerbin was.
        public Vector3d orbitWorldPosition
        {
            get
            {
                Vector3d bodyPositionChange = Vector3d.zero;
                if (orbitValid)
                {
                    currentOrbit.referenceBody.transform.TransformPoint(orbit.referenceBody.transform.InverseTransformPoint(orbit.pos.xzy));
                }
                return Vector3d.zero;
            }
        }

        public Vector3d orbitWorldVelocity
        {
            get
            {
                if (orbitValid)
                {
                    currentOrbit.referenceBody.transform.TransformDirection(orbit.referenceBody.transform.InverseTransformDirection(orbit.vel.xzy));
                }
                return Vector3d.zero;
            }
        }
        //Actual vessel positions in current time frame
        public Vector3d currentWorldPosition
        {
            get
            {
                if (orbitValid)
                {
                    return currentOrbit.referenceBody.transform.InverseTransformPoint(currentOrbit.pos.xzy);
                }
                return Vector3d.zero;
            }
        }

        public Vector3d currentWorldVelocity
        {
            get
            {
                if (orbitValid)
                {
                    return currentOrbit.referenceBody.transform.TransformDirection(orbit.referenceBody.transform.InverseTransformDirection(orbit.vel.xzy));
                }
                return Vector3d.zero;
            }
        }

        public GameObject gameObj
        {
            private set;
            get;
        }

        public LineRenderer line
        {
            private set;
            get;
        }
		
        public OrbitRenderer orbitRenderer
        {
            private set;
            get;
        }

		public Color activeColor
		{
			private set;
			get;
		}

        public bool orbitValid
        {
            get
            {
                if (orbit == null)
                {
                    return false;
                }
                //Vector3d test_pos = worldPosition;
                //Vector3d test_vel = worldVelocity;
                //if (test_pos == Vector3d.zero || test_vel == Vector3d.zero)
                //{
                //    return false;
                //}
                //return true;
                return true;
            }
        }

        public bool shouldShowOrbit
        {
            get
            {
				if (!orbitValid || situationIsGrounded(info.situation))
					return false;
				else
					return info.state == State.ACTIVE || orbitRenderer.mouseOver;
            }
        }

		public Vessel vesselRef
		{
			set;
            get;
		}

        //Methods

        public KMPVessel(String vessel_name, String owner_name, Guid _id)
        {
			info = new KMPVesselInfo();

			vesselName = vessel_name;
			ownerName = owner_name;
			id = _id;

			//Build the name of the game object
			System.Text.StringBuilder sb = new StringBuilder();
			sb.Append(vesselName);
//			sb.Append(" (");
//			sb.Append(ownerName);
//			sb.Append(')');

			gameObj = new GameObject(sb.ToString());
			gameObj.layer = 9;

			generateActiveColor();

            line = gameObj.AddComponent<LineRenderer>();
            orbitRenderer = gameObj.AddComponent<OrbitRenderer>();
			orbitRenderer.driver = new OrbitDriver();
			
            line.transform.parent = gameObj.transform;
            line.transform.localPosition = Vector3.zero;
            line.transform.localEulerAngles = Vector3.zero;

            line.useWorldSpace = true;
            line.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            line.SetVertexCount(2);
            line.enabled = false;

        }

		public void generateActiveColor()
		{
			//Generate a display color from the owner name
			activeColor = generateActiveColor(ownerName);
		}

		public static Color generateActiveColor(String str)
		{
			int val = 5381;

			foreach (char c in str)
			{
				val = ((val << 5) + val) + c;
			}

			return generateActiveColor(Math.Abs(val));
		}

		public static Color generateActiveColor(int val)
		{
			switch (val % 17)
			{
				case 0:
					return Color.red;

				case 1:
					return new Color(1, 0, 0.5f, 1); //Rosy pink
					
				case 2:
					return new Color(0.6f, 0, 0.5f, 1); //OU Crimson
					
				case 3:
					return new Color(1, 0.5f, 0, 1); //Orange
					
				case 4:
					return Color.yellow;
					
				case 5:
					return new Color(1, 0.84f, 0, 1); //Gold
					
				case 6:
					return Color.green;
					
				case 7:
					return new Color(0, 0.651f, 0.576f, 1); //Persian Green
					
				case 8:
					return new Color(0, 0.651f, 0.576f, 1); //Persian Green
					
				case 9:
					return new Color(0, 0.659f, 0.420f, 1); //Jade
					
				case 10:
					return new Color(0.043f, 0.855f, 0.318f, 1); //Malachite
					
				case 11:
					return Color.cyan;					

				case 12:
					return new Color(0.537f, 0.812f, 0.883f, 1); //Baby blue;

				case 13:
					return new Color(0, 0.529f, 0.741f, 1); //NCS blue
					
				case 14:
					return new Color(0.255f, 0.412f, 0.882f, 1); //Royal Blue
					
				case 15:
					return new Color(0.5f, 0, 1, 1); //Violet
					
				default:
					return Color.magenta;
					
			}
		}

        public void setOrbitalData(Orbit new_orbit) {
            if (new_orbit == null)
            {
                return;
            }
            orbit = new_orbit;
            updatePosition();
        }

        public void updatePosition()
        {
			if (!orbitValid)
				return;

			if (gameObj.transform == null) {
				return;
			}

            gameObj.transform.position = orbitWorldPosition;
			//gameObj.transform.rotation = worldRotation;

            Vector3 scaled_pos = ScaledSpace.LocalToScaledSpace(orbitWorldPosition);

            //Determine the scale of the line so its thickness is constant from the map camera view
			float apparent_size = 0.01f;
			bool pointed = true;
			switch (info.state)
			{
				case State.ACTIVE:
					apparent_size = 0.015f;
					pointed = true;
					break;

				case State.INACTIVE:
					apparent_size = 0.01f;
					pointed = true;
					break;

				case State.DEAD:
					apparent_size = 0.01f;
					pointed = false;
					break;

			}

			float scale = (float)(apparent_size * Vector3.Distance(MapView.MapCamera.transform.position, scaled_pos));

            //Set line vertex positions
					//needs world direction
            Vector3 line_half_dir = Vector3.one * (scale * ScaledSpace.ScaleFactor);

			if (pointed)
			{
				line.SetWidth(scale, 0);
			}
			else
			{
				line.SetWidth(scale, scale);
				line_half_dir *= 0.5f;
			}

            line.SetPosition(0, ScaledSpace.LocalToScaledSpace(orbitWorldPosition - line_half_dir));
            line.SetPosition(1, ScaledSpace.LocalToScaledSpace(orbitWorldPosition + line_half_dir));
        }

        public void updateRenderProperties(bool force_hide = false)
        {
			line.enabled = !force_hide && orbitValid && gameObj != null && MapView.MapIsEnabled;

			if (gameObj != null && !force_hide && shouldShowOrbit)
				orbitRenderer.drawMode = OrbitRenderer.DrawMode.REDRAW_AND_RECALCULATE;
			else
				orbitRenderer.drawMode = OrbitRenderer.DrawMode.OFF;

			//Determine the color
			Color color = activeColor;

			if (orbitRenderer.mouseOver)
				color = Color.white; //Change line color when moused over
			else
			{
				
				switch (info.state)
				{
					case State.ACTIVE:
						color = activeColor;
						break;

					case State.INACTIVE:
						color = activeColor * 0.75f;
						color.a = 1;
						break;

					case State.DEAD:
						color = activeColor * 0.5f;
						break;
				}
				
			}

			line.SetColors(color, color);
			orbitRenderer.orbitColor = color * 0.5f;

			if (force_hide || !orbitValid)
				orbitRenderer.drawIcons = OrbitRenderer.DrawIcons.NONE;
			else if (info.state == State.ACTIVE && shouldShowOrbit)
				orbitRenderer.drawIcons = OrbitRenderer.DrawIcons.OBJ_PE_AP;
			else
				orbitRenderer.drawIcons = OrbitRenderer.DrawIcons.OBJ;


        }

		public static bool situationIsGrounded(Situation situation) {

			switch (situation) {

				case Situation.LANDED:
				case Situation.SPLASHED:
				case Situation.PRELAUNCH:
				case Situation.DESTROYED:
				case Situation.UNKNOWN:
					return true;

				default:
					return false;
			}
		}
		
		
		public static bool situationIsOrbital(Situation situation) {

			switch (situation) {

				case Situation.ASCENDING:
				case Situation.DESCENDING:
				case Situation.ENCOUNTERING:
				case Situation.ESCAPING:
				case Situation.ORBITING:
					return true;

				default:
					return false;
			}
		}
    }
}

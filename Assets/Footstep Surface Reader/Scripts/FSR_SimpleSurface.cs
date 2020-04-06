using UnityEngine;

namespace FSR
{
    public class FSR_SimpleSurface : MonoBehaviour
    {
        [SerializeField]private FSR_Data data;
        [SerializeField]private string surfaceName;



        public string GetSurface()
        {
            bool mismatch = true;

            foreach (FSR_Data.SurfaceType surface in data.surfaces){

                if (surface.name.Equals(surfaceName))
                {
                    mismatch = false;
                }
            }


            if (!mismatch)
            {
                return surfaceName;
            }
            else
            {
                throw new UnityException("looks like you have mismatching surfaces names, make sure all the surfaces components have the same name specified in the FSR data");
            }
        }

    }
}

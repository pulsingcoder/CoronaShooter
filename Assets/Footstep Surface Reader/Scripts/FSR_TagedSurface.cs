using System;
using UnityEngine;


namespace FSR
{
    public class FSR_TagedSurface : MonoBehaviour
    {

        [SerializeField] private FSR_Data data;




        public String GetSurface()
        {

                    try
                    {
                        String[] surfaceName = transform.gameObject.GetComponent<Collider>().GetComponent<Renderer>().material.mainTexture.name.Split('_');

                         bool mismatch = true;
                                                
                        foreach (FSR_Data.SurfaceType surface in data.surfaces)
                        {

                            if (surface.name.Equals(surfaceName[1]))
                            {
                                mismatch = false;
                            }
                        }


                        if (!mismatch)
                        {
                            return surfaceName[1];
                        }
                        else
                        {
                            throw new UnityException("looks like you have mismatching surfaces names, make sure all the surfaces components have the same name specified in the FSR data");
                        }

                    }
                    catch (MissingComponentException)
                    {
                        return "GENERIC";
                    }
                    catch (NullReferenceException)
                    {
                        return "GENERIC";
                    }
                
            

        }
    }
}
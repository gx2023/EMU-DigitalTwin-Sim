
using UnityEngine;
using System.Collections.Generic;

namespace EMU.DT.Simulation
{
    public class DepotController : MonoBehaviour
    {
        [SerializeField] private List&lt;EMUController&gt; emus;
        [SerializeField] private List&lt;Transform&gt; tracks;
        [SerializeField] private List&lt;Transform&gt; platforms;
        
        private static DepotController instance;
        
        public static DepotController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType&lt;DepotController&gt;();
                }
                return instance;
            }
        }
        
        private void Awake()
        {
            if (instance != null &amp;&amp; instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
        }
        
        public EMUController GetEMU(int index)
        {
            if (index &gt;= 0 &amp;&amp; index &lt; emus.Count)
            {
                return emus[index];
            }
            return null;
        }
        
        public Transform GetTrack(int index)
        {
            if (index &gt;= 0 &amp;&amp; index &lt; tracks.Count)
            {
                return tracks[index];
            }
            return null;
        }
        
        public Transform GetPlatform(int index)
        {
            if (index &gt;= 0 &amp;&amp; index &lt; platforms.Count)
            {
                return platforms[index];
            }
            return null;
        }
    }
}


using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMU.DT.Api
{
    public class ApiClient : MonoBehaviour
    {
        [SerializeField] private string baseUrl = "http://localhost:5000";
        
        public static ApiClient Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null &amp;&amp; Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        public async Task&lt;T&gt; GetAsync&lt;T&gt;(string endpoint)
        {
            var url = $"{baseUrl}/{endpoint}";
            using (var request = UnityEngine.Networking.UnityWebRequest.Get(url))
            {
                var operation = request.SendWebRequest();
                
                while (!operation.isDone)
                {
                    await Task.Yield();
                }
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    var json = request.downloadHandler.text;
                    return JsonUtility.FromJson&lt;T&gt;(json);
                }
                else
                {
                    Debug.LogError($"API Error: {request.error}");
                    throw new Exception(request.error);
                }
            }
        }
        
        public async Task&lt;T&gt; PostAsync&lt;T&gt;(string endpoint, object data)
        {
            var url = $"{baseUrl}/{endpoint}";
            var json = JsonUtility.ToJson(data);
            var bodyRaw = Encoding.UTF8.GetBytes(json);
            
            using (var request = UnityEngine.Networking.UnityWebRequest.PostWwwForm(url, json))
            {
                request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
                request.uploadHandler.contentType = "application/json";
                request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                
                var operation = request.SendWebRequest();
                
                while (!operation.isDone)
                {
                    await Task.Yield();
                }
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    var responseJson = request.downloadHandler.text;
                    return JsonUtility.FromJson&lt;T&gt;(responseJson);
                }
                else
                {
                    Debug.LogError($"API Error: {request.error}");
                    throw new Exception(request.error);
                }
            }
        }
    }
}

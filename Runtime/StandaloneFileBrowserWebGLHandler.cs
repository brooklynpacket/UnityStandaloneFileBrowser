#if UNITY_WEBGL

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace JamCity.SF.FileBrowser
{
    internal class StandaloneFileBrowserWebGLHandler : MonoBehaviour
    {
        [DllImport(("__Internal"))]
        private static extern void UploadFile(string gameObjectNamePtr, string methodNamePtr, string filterPtr,
                                              bool multiselect);

        [DllImport("__Internal")]
        private static extern void DownloadFile(string gameObjectNamePtr, string methodNamePtr, string fileName,
                                                byte[] data, int dataSize);

        private Action<string> fileLoadedCallback;
        private Action<IEnumerable<string>> filesLoadedCallback;
        private Action fileSavedCallback;

        public void OpenFileBrowser(string extensions, Action<string> onFileLoaded)
        {
            fileLoadedCallback = onFileLoaded;
            UploadFile(name, nameof(FileDialogResult), extensions, false);
        }

        public void OpenMultiFileBrowser(string extensions, Action<IEnumerable<string>> onFilesLoaded)
        {
            filesLoadedCallback = onFilesLoaded;
            UploadFile(name, nameof(MultiFileDialogResult), extensions, true);
        }

        [UsedImplicitly]
        private void FileDialogResult(string fileUrl)
        {
            StartCoroutine(LoadBlob(fileUrl, fileLoadedCallback));
        }

        [UsedImplicitly]
        private void MultiFileDialogResult(string fileUrls)
        {
            StartCoroutine(LoadBlob(fileUrls.Split(','), filesLoadedCallback));
        }

        private static IEnumerator LoadBlob(string url, Action<string> callback)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string contents = webRequest.downloadHandler.text;
                callback?.Invoke(contents);
            }
        }

        private static IEnumerator LoadBlob(IEnumerable<string> urls, Action<IEnumerable<string>> callback)
        {
            List<string> fileContents = new();
            foreach (string url in urls)
            {
                UnityWebRequest webRequest = UnityWebRequest.Get(url);
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string contents = webRequest.downloadHandler.text;
                    fileContents.Add(contents);
                }
            }

            callback?.Invoke(fileContents);
        }

        public void SaveFileBrowser(byte[] data, string fileName, Action onFileSaved = null)
        {
            fileSavedCallback = onFileSaved;
            DownloadFile(name, nameof(SaveDialogResult), fileName, data, data.Length);
        }

        [UsedImplicitly]
        private void SaveDialogResult()
        {
            fileSavedCallback?.Invoke();
        }
    }
}

#endif

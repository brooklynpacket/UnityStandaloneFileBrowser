#if UNITY_WEBGL

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JamCity.SF.FileBrowser
{
    internal class StandaloneFileBrowserWebGL : IStandaloneFileIO
    {
        private StandaloneFileBrowserWebGLHandler handler;
        public StandaloneFileBrowserWebGL()
        {
            GameObject handlerGameObject = new(nameof(StandaloneFileBrowserWebGLHandler))
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            Object.DontDestroyOnLoad(handlerGameObject);
            handler = handlerGameObject.AddComponent<StandaloneFileBrowserWebGLHandler>();
        }

        public void OpenFileAsync(Action<string> contentsCallback, params ExtensionFilter[] extensions)
        {
            handler.OpenFileBrowser(GetExtensions(extensions), contentsCallback);
        }

        public void OpenFilesAsync(Action<IEnumerable<string>> contentsCallback, params ExtensionFilter[] extensions)
        {
            handler.OpenMultiFileBrowser(GetExtensions(extensions), contentsCallback);
        }

        public void SaveFileAsync(string defaultPath, string contents)
        {
            handler.SaveFileBrowser(Encoding.UTF8.GetBytes(contents), defaultPath);
        }

        private static string GetExtensions(IEnumerable<ExtensionFilter> extensionFilters)
        {
            List<string> extensions = new();

            foreach (ExtensionFilter extensionFilter in extensionFilters)
            {
                extensions.AddRange(extensionFilter.Extensions);
            }

            return string.Join(",", extensions);
        }
    }
}

#endif

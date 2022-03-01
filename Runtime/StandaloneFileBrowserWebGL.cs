#if UNITY_WEBGL

using System;
using UnityEditor;
using UnityEngine;

namespace JamCity.SF.FileBrowser
{
    public class StandaloneFileBrowserWebGL : IStandaloneFileBrowser
    {
        public string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            Debug.LogError("Cannot use synchronous file browser operations in WebGL!");
            return Array.Empty<string>();
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect,
                                       Action<string[]> cb)
        {
            cb.Invoke(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            Debug.LogError("Cannot use synchronous file browser operations in WebGL!");
            return Array.Empty<string>();
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFolderPanel(title, directory, multiselect));
        }

        public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            // string ext = extensions != null ? extensions[0].Extensions[0] : string.Empty;
            // string name = string.IsNullOrEmpty(ext) ? defaultName : defaultName + "." + ext;
            Debug.LogError("Cannot use synchronous file browser operations in WebGL!");
            return string.Empty;
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions,
                                       Action<string> cb)
        {
            cb.Invoke(SaveFilePanel(title, directory, defaultName, extensions));
        }

        // EditorUtility.OpenFilePanelWithFilters extension filter format
        private static string[] GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            string[] filters = new string[extensions.Length * 2];
            for (int i = 0 ; i < extensions.Length ; i++)
            {
                int at = i * 2;
                filters[at] = extensions[i].Name;
                filters[at + 1] = string.Join(",", extensions[i].Extensions);
            }

            return filters;
        }
    }
}

#endif

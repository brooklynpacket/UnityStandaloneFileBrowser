#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;

namespace JamCity.SF.FileBrowser
{
    internal class StandaloneFileBrowserEditor : StandaloneFileBrowser
    {
        public override string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions,
                                               bool multiselect)
        {
            string path;

            if (extensions == null)
            {
                path = EditorUtility.OpenFilePanel(title, directory, string.Empty);
            }
            else
            {
                path = EditorUtility.OpenFilePanelWithFilters(title, directory,
                    GetFilterFromFileExtensionList(extensions));
            }

            return string.IsNullOrEmpty(path) ? Array.Empty<string>() : new[] { path };
        }

        public override void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions,
                                                bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public override string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            string path = EditorUtility.OpenFolderPanel(title, directory, string.Empty);
            return string.IsNullOrEmpty(path) ? Array.Empty<string>() : new[] { path };
        }

        public override void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFolderPanel(title, directory, multiselect));
        }

        public override string SaveFilePanel(string title, string directory, string defaultName,
                                             ExtensionFilter[] extensions)
        {
            string ext = extensions != null ? extensions[0].Extensions[0] : string.Empty;
            string name = string.IsNullOrEmpty(ext) ? defaultName : $"{defaultName}{ext}";
            return EditorUtility.SaveFilePanel(title, directory, name, ext);
        }

        public override void SaveFilePanelAsync(string title, string directory, string defaultName,
                                                ExtensionFilter[] extensions, Action<string> cb)
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

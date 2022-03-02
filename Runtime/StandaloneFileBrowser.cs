using System;
using System.Collections.Generic;
using System.IO;

namespace JamCity.SF.FileBrowser
{
    internal abstract class StandaloneFileBrowser : IStandaloneFileBrowser
    {
        public void OpenFileAsync(Action<string> contentsCallback, params ExtensionFilter[] extensions)
        {
            OpenFilePanelAsync("Open File", string.Empty, extensions, false, paths =>
            {
                foreach (string path in paths)
                {
                    contentsCallback(File.ReadAllText(path));
                }
            });
        }

        public void OpenFilesAsync(Action<IEnumerable<string>> contentsCallback, params ExtensionFilter[] extensions)
        {
            OpenFilePanelAsync("Open Files", string.Empty, extensions, true, paths =>
            {
                List<string> contents = new();
                foreach (string path in paths)
                {
                    contents.Add(File.ReadAllText(path));
                }

                contentsCallback(contents);
            });
        }

        public void SaveFileAsync(string defaultPath, string contents)
        {
            string directory = Path.GetDirectoryName(defaultPath);
            string name = Path.GetFileNameWithoutExtension(defaultPath);
            string ext = Path.GetExtension(defaultPath);
            SaveFilePanelAsync("Save File", directory, name, new ExtensionFilter[] { ext },
                saveFilePath => File.WriteAllText(saveFilePath, contents));
        }

        public abstract string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions,
                                               bool multiselect);

        public abstract string[] OpenFolderPanel(string title, string directory, bool multiselect);

        public abstract string SaveFilePanel(string title, string directory, string defaultName,
                                             ExtensionFilter[] extensions);

        public abstract void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions,
                                                bool multiselect, Action<string[]> cb);

        public abstract void OpenFolderPanelAsync(string title, string directory, bool multiselect,
                                                  Action<string[]> cb);

        public abstract void SaveFilePanelAsync(string title, string directory, string defaultName,
                                                ExtensionFilter[] extensions, Action<string> cb);
    }
}

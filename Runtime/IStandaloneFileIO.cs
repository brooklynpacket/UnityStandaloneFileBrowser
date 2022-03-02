using System;
using System.Collections.Generic;

namespace JamCity.SF.FileBrowser
{
    public interface IStandaloneFileIO
    {
        void OpenFileAsync(Action<string> contentsCallback, params ExtensionFilter[] extensions);
        void OpenFilesAsync(Action<IEnumerable<string>> contentsCallback, params ExtensionFilter[] extensions);
        void SaveFileAsync(string defaultPath, string contents);
    }
}

namespace JamCity.SF.FileBrowser
{
    public static class StandaloneFileBrowserHelper
    {
        public static IStandaloneFileIO GetFileIO()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return new StandaloneFileBrowserWebGL();
#else
            return GetFileBrowser();
#endif
        }

        public static IStandaloneFileBrowser GetFileBrowser()
        {
#if UNITY_EDITOR
            return new StandaloneFileBrowserEditor();
#elif UNITY_STANDALONE_OSX
            return  new StandaloneFileBrowserMac();
#elif UNITY_STANDALONE_WIN
            return  new StandaloneFileBrowserWindows();
#elif UNITY_STANDALONE_LINUX
            return  new StandaloneFileBrowserLinux();
#else
            return null;
#endif
        }
    }
}

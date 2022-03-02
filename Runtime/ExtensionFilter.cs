namespace JamCity.SF.FileBrowser
{
    public struct ExtensionFilter
    {
        public readonly string Name;
        public readonly string[] Extensions;

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            Name = filterName;
            Extensions = filterExtensions;
        }

        public static implicit operator ExtensionFilter(string extension)
        {
            string withDot = extension.StartsWith(".") ? extension : $".{extension}";
            return new ExtensionFilter(string.Empty, withDot);
        }
    }
}

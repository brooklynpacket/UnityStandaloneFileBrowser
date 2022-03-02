# SF Tools | Unity File Browser

A simple wrapper for native file dialogs on Windows/Mac/Linux/Web.

- Works in editor and runtime.
- Open file/folder, save file dialogs supported.
- Multiple file selection.
- File extension filter.
- Mono/IL2CPP backends supported.
- Linux support by [Ricardo Rodrigues](https://github.com/RicardoEPRodrigues).
- Basic WebGL support.

Example usage:

```csharp
// Open file
var paths = StandaloneFileBrowserHelper.GetFileBrowser().OpenFilePanel("Open File", "", "", false);

// Open file async
StandaloneFileBrowserHelper.GetFileBrowser().OpenFilePanelAsync("Open File", "", "", false, (string[] paths) => {  });

// Open file with filter
var extensions = new [] {
    new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
    new ExtensionFilter("Sound Files", "mp3", "wav" ),
    new ExtensionFilter("All Files", "*" ),
};
var paths = StandaloneFileBrowserHelper.GetFileBrowser().OpenFilePanel("Open File", "", extensions, true);

// Save file
var path = StandaloneFileBrowserHelper.GetFileBrowser().SaveFilePanel("Save File", "", "", "");

// Save file async
StandaloneFileBrowserHelper.GetFileBrowser().SaveFilePanelAsync("Save File", "", "", "", (string path) => {  });

// Save file with filter
var extensionList = new [] {
    new ExtensionFilter("Binary", "bin"),
    new ExtensionFilter("Text", "txt"),
};
var path = StandaloneFileBrowserHelper.GetFileBrowser().SaveFilePanel("Save File", "", "MySaveFile", extensionList);

// Open file directly
StandaloneFileBrowserHelper.GetFileIO.OpenFileAsync(contents => { });

// Save file directly
string contents = "{}";
StandaloneFileBrowserHelper.GetFileIO.SaveFileAsync("path/to/file.json", contents);
```

Mac Screenshot
![Alt text](/Documentation/sfb_mac.jpg?raw=true "Mac")

Windows Screenshot
![Alt text](/Documentation/sfb_win.jpg?raw=true "Win")

Linux Screenshot
![Alt text](/Documentation/sfb_linux.jpg?raw=true "Win")

Notes:
- Windows
    * Requires .NET 2.0 api compatibility level 
    * Async dialog opening not implemented, ..Async methods simply calls regular sync methods.
    * Plugin import settings should be like this;
    
    ![Alt text](/Documentation/win_import_1.jpg?raw=true "Plugin Import Ookii") ![Alt text](/Documentation/win_import_2.jpg?raw=true "Plugin Import System.Forms")
    
- Mac
    * Sync calls are throws an exception at development build after native panel loses and gains focus. Use async calls to avoid this.

WebGL:
 - Basic upload/download file support.
 - File filter support.

# About the Project

A WPF based desktop app for viewing photos that are uploaded on photos hosting website ```Flickr.com```. 
Users can view and search for photos from within the app.

Following features are provided by the Application,

* Search for your favorite images using the search bar.
* Select an image and zoom-in/zoom-out. 
* Navigate to different page results.
* Navigate to previous search result. 


## Running the Project

### Prerequisite 

* Visual Studio 2017 or later
* Microsoft Edge WebView2 Runtime
* Reactjs version 18.2.0 or later
* Node.js version 18.12.1 or later

### Building the Project 

Please follow below steps to build the project

1. Clone the project to your local directory:
   ```
   https://github.com/sowrab1994/PhotoViewer.git
   ```
2. CD into the directory ```PhotoViewer\src```
3. Run following command in command prompt
   ```
   msbuild PhotoViewer.sln /property:Configuration=Release
   ```
4. Application ```PhotoViewe.exe``` will be built in the directory
   ```
   PhotoViewer\src\PhotoViewer\bin\Release\
   ```

## Technical Details

### High Level Design

![image](https://user-images.githubusercontent.com/8791528/208348048-afc7f875-426c-458e-8e09-366755c98a9d.png)

![image](https://user-images.githubusercontent.com/8791528/208348206-67e1b216-d413-4c98-bae1-5cc67332470d.png)

### Low Level Design

![image](https://user-images.githubusercontent.com/8791528/208348235-1ca8300f-831f-4f1e-be5a-401981dc6dd0.png)


## What's Next

* CICD Pipeline in Git to generate the build.
* Localization of texts used in the Application.
* Adding Icons for the Application.
* Making the application DPI aware.
* Caching of API results.


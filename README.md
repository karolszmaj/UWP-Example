# Flickr Search - Universal App Platform Example
Here you can find example implementation of flickr search. Solution was configured for Windows SDK Build 14393.
### Requirements
1. Windows 10 build 14393
2. Visual Studo 2015 with update 3
3. Nuget Version >= 3.4.4

### Architecture - MVVM
1. FlickrClient - Universal Windows
Contains all Views and ViewModels. In addition this assembly is required to setup IoC Container and load&install all available assemblies.
2. FlickrClient.Framework - Portable (allows unit testing)
Contains common interfaces for wrapping Container and exposes interface for assembly installation
3. FlicrSearch.Logic - Portable (allows unit testing)
Has all necessary logic for executing requests to Flickr API. All business logic is put into queries (CQRS pattern). Data Access Layer returns all data using reactive extensions IObservable<T>.

### Considerations and app performance optimizations
##### Page Transitions & Animations
Solution allows developer to use XAML Composition API. This app example not covers usage of this API.
##### Display photos on Pivot Control
For purpose of this app second screen should allow user to swipe between pictures and display headers. Problem of Pivot control is that it doesn't support virtualization. Rendering 300+ elements on Pivot control is almost unreal. Render time will to much time that's why I've implemented other solution for this problem. 
Combining FlipView(for photos) + ListView(for headers) allows us to use virtualization support. Render time is much better and keeps memory in good condition. 
[![](https://cldup.com/VAV8JzwxYd.PNG)]


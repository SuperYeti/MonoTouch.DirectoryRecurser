
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.IO;

namespace DirectoryRecurser
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// If you have defined a view, add it here:
			// window.AddSubview (navigationController.View);
			
			window.MakeKeyAndVisible ();
			
			window.AddSubview (navigation.View);
			
			ShowDirectoryTree("/Users/wmoxley/Documents/", false);
			
			return true;
		}
		
		private void ShowDirectoryTree(string basePath, bool pushing)
		{
			RootElement root = new RootElement(basePath)
			{
				BuildListing(basePath)
			};
			
			var dv = new DialogViewController (root, pushing);
			
			navigation.PushViewController (dv, true);
			
		}
		
		private Section BuildListing(string basePath)
		{
			Section sect = new Section();
			
			foreach(string dir in Directory.GetDirectories(basePath))
			{
				string strDir = dir;
				MultilineElement element = new MultilineElement (strDir);
				element.Tapped += delegate { ShowDirectoryTree(dir, true); };
			
				sect.Add(element);
			}
			
			foreach(string fil in Directory.GetFiles(basePath))
			{
				string strFil = fil;
				MultilineElement element = new MultilineElement (basePath);
				element.Tapped += delegate { Utilities.UnsuccessfulMessage("File: " + strFil + " tapped"); };
			
				sect.Add(element);
				
			}
			
			return sect;
			
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
			
		}
		
	}
	
}

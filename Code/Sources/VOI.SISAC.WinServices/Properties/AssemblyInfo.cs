using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("VOI.SISAC.WinServices")]
[assembly: AssemblyDescription("Carga Automática de Itinerario")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Volaris")]
[assembly: AssemblyProduct("VOI.SISAC.WinServices")]
[assembly: AssemblyCopyright("Copyright © Volaris 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("473116f5-57c8-45f7-ad87-5f3464298097")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//Se agrego la configuracion de Log4Net
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

using NuGet.Frameworks;
using System.Collections.Generic;
using System.Linq;

namespace packageValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            string packagePath = @"C:\git\runtime3\artifacts\packages\Debug\Shipping\System.Diagnostics.EventLog.6.0.0-dev.nupkg";
            
            Package package = new Package(packagePath);

            ValidateCompileTimeRuntimeTimeTfms(package);
            GetTfmRidPairsToTest(package);
        }
        
        // Validate that there is a compatible runtime asset for each compile time asset.
        // Valiate the assembly version compatibilty.
        // Also Validate the api compat between the compatiable compile and runtime assemblies.
        public static void ValidateCompileTimeRuntimeTimeTfms(Package package)
        {
            List<NuGetFramework> runtimeTfms = package.packageAssets.Where(t => t.assetType == AssetType.LibAsset).Select(t => t.tfm).ToList();
            foreach (var item in package.packageAssets.Where(t => t.assetType == AssetType.RefAsset))
            {
                NuGetFramework compileTimeTfm = item.tfm;
                NuGetFramework compatibleFramework = new FrameworkReducer().GetNearest(compileTimeTfm, runtimeTfms);                
            }
        }

        // Validate that the api surface is same across all runtime assets.
        public static void ValidateApiCompatAcrossAllRuntimeAssemblies(Package package)
        {

        }

        // Validates that all the tfms supported in the previous version are supported in this version as well.
        // validate that api surface is compatible as well between the compatible dlls.
        public static void ValidateTargetFrameworksFromPreviousPackage(Package package, Package oldPackage)
        {

        }

        // Issue warnings if we are dropping any dependencies from previous versions
        public static VoidValidateDependencyGroupsFromPreviousPackage(Package package, Package oldPackage)
        {

        }

        // Validate that there are no unneccessary and unwanted package references for each tfms.
        public static ValidatePackageReferencesForTfms(Package package)
        {

        }

        public static List<(NuGetFramework, string)> GetTfmRidPairsToTest(Package package)
        {
            return package.packageAssets.Select(t => (t.tfm, t.rid)).ToList();
        }

        // Verigy closure and Verify types are out of scope for the first draft

    }
}

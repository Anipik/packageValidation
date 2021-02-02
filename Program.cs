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
            var Assets = package.packageAssets.Where(t => t.rid != null).ToList();
            var tfms = Assets.Select(t => t.tfm).Distinct();
            foreach (var tfm in tfms)
            {
                var versionlessAsset = package.packageAssets.Where(t => t.tfm == tfm && t.assetType = AssetType.LibAsset);
                var assemblyPath = package.GetAssemblyPath(versionlessAsset);

                foreach (var item in Assets)
                {
                    var compareAssemblyPath = package.GetAssemblyPath(item);
                    ApiCompatManager.RunApiCompat(assemblyPath, item);
                }
            }
        }

        // Validates that all the tfms supported in the previous version are supported in this version as well.
        // validate that api surface is compatible as well between the compatible dlls.
        public static void ValidateTargetFrameworksFromPreviousPackage(Package package, Package oldPackage)
        {
            var oldTfmRidPairs = GetTfmRidPairsToTest(oldPackage);
            var runtimeTfmRidPairs = package.packageAssets.Where(t => t.assetType != AssetType.RefAsset).Select(t => (t.tfm, t.rid)).ToList();

            foreach (var item in oldTfmRidPairspackage)
            {
                // pass the rid as well
                NuGetFramework compileTimeTfm = item.tfm;
                NuGetFramework compatibleFramework = new FrameworkReducer().GetNearest(compileTimeTfm, runtimeTfmRidPairs.Select(t => t.tfm));
            }
        }

        public static void ValidateApiSurfaceFromPreviousPackage(Package package, Package oldPackage)
        {

        }

        // Issue warnings if we are dropping any dependencies from previous versions
        public static Void ValidateDependencyGroupsFromPreviousPackage(Package package, Package oldPackage)
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

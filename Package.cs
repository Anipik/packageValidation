using NuGet.Packaging;
using System;
using System.Linq;
using NuGet.Frameworks;
using System.Collections.Generic;
using NuGet.Packaging.Core;

namespace packageValidation
{
    public class Package
    {
        public List<Asset> packageAssets = new List<Asset>();
        public string Title;
        public Dictionary<NuGetFramework, List<PackageDependency>> packageDependencies = new Dictionary<NuGetFramework, List<PackageDependency>>();

        public Package(string packagePath)
        {
            var nupkgReader = new PackageArchiveReader(packagePath);
            var nuspecReader = nupkgReader.NuspecReader;
            var Title = nuspecReader.GetTitle();
            var dependencyGroups = nuspecReader.GetDependencyGroups();
            
            foreach (var item in dependencyGroups)
            {
                packageDependencies.Add(item.TargetFramework, item.Packages.ToList());
            }

            var files = nupkgReader.GetFiles().ToList().Where(t => t.EndsWith(".dll")).Where(t => t.Contains(Title + ".dll"));
            foreach (var file in files)
            {
                Console.WriteLine(file);
                packageAssets.Add(ExtractAssetFromFile(file));
            }
        }

        public static Asset ExtractAssetFromFile(string filePath)
        {
            Asset asset = null;
            if (filePath.StartsWith("ref"))
            {
                var stringParts = filePath.Split(@"/");
                asset = new Asset(NuGetFramework.Parse(stringParts[1]), null, filePath, AssetType.RefAsset);
            }
            else if (filePath.StartsWith("lib"))
            {
                var stringParts = filePath.Split(@"/");
                asset = new Asset(NuGetFramework.Parse(stringParts[1]), null, filePath, AssetType.LibAsset);

            }
            else if (filePath.StartsWith("runtimes"))
            {
                var stringParts = filePath.Split(@"/");
                asset = new Asset(NuGetFramework.Parse(stringParts[3]), stringParts[1], filePath, AssetType.RuntimeAsset);
            }
            return asset;
        }

        // extract the package to a temp folder or 
        public string GetAssemblyPath(Asset asset)
        {
            return null;
        }
    }
}

using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace packageValidation
{
    public class Asset
    {
        public NuGetFramework tfm;
        public string rid;
        public string packagePath;
        public AssetType assetType;

        public Asset(NuGetFramework targetFramework, string runtimeIdentifier, string packagePath1, AssetType AssetType)
        {
            tfm = targetFramework;
            rid = runtimeIdentifier;
            packagePath = packagePath1;
            assetType = AssetType;
        }
    }

}

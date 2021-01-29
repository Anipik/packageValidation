# Validation for Multi-Targeted Packages

 There are several well-established patterns for building cross-platform packages but our guidance for building cross-platform packages is still very rudimentary.
 This feature provides validation scenarios which a developer should worry about while building a cross platform packages.

# Validation across different runtimes and Target Frameworks
- Api surface area is same across all the runtimes for a TargetFramework.
- Compatible frameworks in the package have compatible surface area.

# Validation between previous release
- Validate all the TargetFrameworks supported in the previous release are supported in the new release. (unless they are out of support)
- Validate that all common TargetFrameworks have compatible surface area eg no deletions

# Validation between compile and runtime assets
- Validate that there is compatible runtime asset for every compile time asset.
- Validate that the surface area between such assets is the same.
- Validate that assembly versions match 

# Validation for dependencies
- Warn on unnecessary package references. A package reference that is never used directly by assembly. A package reference that gets removed by conflict resolution. PackageReference which is exposed to compile but only used at runtime.
- Warn on missing package references. Package references exposed in one framework to consumers but absent in a compatible framework. 
- Warn on dropping package references across different versions.

# Verify Closure and Verify Types
TBD


# Implementation Details
The validation Package will be a collections of tasks and targets.

The validation target will run after the pack from a csproj and will run the basic package validation by default which will consists of everything except validation with previous release. The input to such target will be the package path. This part can also be shipped as a dotnet tool or as part of .net sdk.

To enable the validation with the previous release the user will need to add the 
```
<ItemGroup>
  <PackageDownload Include="PackageName" Version="PackageVersion"> 
</ItemGroup>
```
Restore will be responsible for downloading the package. This information will also be provided to the validation target to get the path to previous version package and run the required validation.
 

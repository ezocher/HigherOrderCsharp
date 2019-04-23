# Chapter 1 Notes

## .NET Core versus .NET Framework
* Sections 1.1 through 1.3 are built on .NET Core 2.1
* Sections 1.4 through 1.6 are built on .NET Framework 4.7.2 because they need to use Shell32 COM interop to access file system shortcuts/links. COM interop is not yet available in .NET Core 2.1 
## Section 1.6 - Functional versus object-oriented programming
There is no code in section 1.6, but the book describes an analogous object-oriented approach to implementing dir_walk in C++ using an abstract base class and and two virtual methods.

I have followed this description and built a directory walker in C# in the Chapter1_6 project using an abstract class and abstract methods.

### Two abstract classes: DirectoryWalker and DirectoryWalkerAccumulator

As I re-implemented the samples in section 1.5 I noticed the traversal order difference in the two main implementations of dir_walk.

In the original dir_walk on page 17, the per-file-or-directory function is called *before* each directory is opened and processed. In the later versions on pages 21 and 24 the per-directory function is called *after* each directory is opened and processed so that the per-directory function can process the accumulated results from the files it contains and from its sub-directories.

These two different traversal orders are useful in different situations, so I implemented two different abstract classes as described below.

### DirectoryWalker
In order to implement a "natural order" listing of directories and files (in the PrintAll class in Chaper1_6_DW.cs) I re-created the original dir_walk in the abstract class DirectoryWalker. This class has a single abstract method FileOrDirectory(string) that has no return value.

This class is the simplest way to do things like display or log something for all files and directories as in the PrintAll or Dangles classes (or for some subset of them, like I do in the PrintFilesFilteredByExtension, which I use to list all photos).

The sample classes that inherit from DirectoryWalker are in Chapter1_6_DW.cs.

### DirectoryWalkerAccumulator
I re-created the later version of dir_walk (dir_walk_cb on page 21) in the abstract class DirectoryWalkerAccumulator. I gave it this name because it accumulates results and passes them up to parent directories.

This class is used for the samples that need to accumulate results and process them per directory (such as DirSize) or accumulate all results.

The sample classes that inherit from DirectoryWalkerAccumulator are in Chapter1_6_DWAccumulator.cs.

## Background information for section 1.??
Some resources for background information about topics in Chapter 1

Memoization:
- https://spin.atomicobject.com/2011/10/27/generic-memoization-in-c/
- https://www.aleksandar.io/post/memoization/

Aspect-oriented programming:
- https://www.dotnetcurry.com/patterns-practices/1305/aspect-oriented-programming-aop-csharp-using-solid
- https://www.postsharp.net/blog/post/Anders-Hejlsberg-Dead-Body
- https://msdn.microsoft.com/en-us/magazine/dn574804.aspx?f=255&MSPPError=-2147217396

Dependency injection:
- ??




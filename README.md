# ezocher / HigherOrderCsharp
![Book cover: Higher Order Perl by Mark Jason Dominus](http://hop.perl.plover.com/cover-med.jpg)

[Mark Jason Dominus](https://blog.plover.com/) authored a classic book about functional programming and other advanced techniques in Perl 5 called [Higher Order Perl: Transforming Programs with Programs](https://hop.perl.plover.com/). I bought the book when it came out in 2005 and recently opened it back up after many years away from Perl. The book is [still in print](https://www.amazon.com/Higher-Order-Perl-Transforming-Programs/dp/1558607013/) and Mark has graciously posted the full book for [free download here](https://hop.perl.plover.com/).

This project is a C# implementation of the programs in the book that I'm doing to improve and update my C# skills and knowledge. I think I'll need to learn many features of C# 6 and 7 and maybe Roslyn to duplicate all of the code in the book.

The book follows the classic form of code interspersed with explanations and I'm re-writing all the code in C# (line for line where possible) and adding any necessary C# explanations in comments. 

If you want to follow along you can download the book for free from [this page](https://hop.perl.plover.com/). Mark's explanations of the techniques and their benefits are very language-independent and timeless, so read the book, and when you come to some Perl code then look at this project for the C# version.

## Project Structure
* There is one folder per book chapter containing one VS solution file per chapter, e.g. the Chapter1 folder contains Chapter1.sln
* Each chapter folder contains several folders, with from one to three book sections per folder and one .csproj for each group of sections, e.g. the Chapter1_1-1_3 folder contains Chapter1_1-1_3.csproj
* There is one class in one file for each book section, e.g. Chapter1_4.cs contains class Chapter1_4
* There are Demo_xxx() methods in each class to demonstrate the code from each section
* The Program.cs file calls all of the Demo_xxx() methods for the sections in its .csproj

## Status
- [x] Chapter 1, Sections 1.1 - 1.3
- [x] Section 1.4
- [ ] Section 1.5
- [ ] Section 1.7
- [ ] Section 1.8
- [ ] Chapter 2, Section 2.1

# Code challenge
The purpose of this solution is to merge the product catalog from Company A and Company B and consolidate into one superset.

# Getting Started
The project is a written in .NET 8 and C# using Visual Studio 2022 and consist of two projects
1. ProductProcessing - It is a Console application consist of Input folder having barcodes, Catalog and Supplier from Company A and B
2. ProductProcessingTest - It is a test application using Xunit framework

# Installing
1. navigate to ProductProcessing 
cd ProductProcessing

2. Restore Nuget package:
dotnet restore

# Executing Program
1. Build solution
dotnet build

2. Run the application
dotnet run

The application will generate the output csv in Output folder.

![image](https://github.com/farjaf/ProductProcessing/assets/12959993/77de5185-5755-45e7-8102-c4e8bfb07afb)

# Running Tests
To run unit test, navigate to ProductProcessingTest and run 
dotnet test

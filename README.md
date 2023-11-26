## Build
* First if you want to build the project you have to install .NET 8 SDK, you can download it from:
https://dotnet.microsoft.com/en-us/download/dotnet/8.0
* To build project enter PWProject directory and run command:
```
build.bat
```

## Run 
* To run project run .exe file, you can also run thread pool version by typing:
``` 
\PWProject\bin\Debug\net8.0\PWProject.exe -- parallel 
```
for thread pool version

* To run project on simple threads type
```
\PWProject\bin\Debug\net8.0\PWProject.exe -- threads
```

* You can also run a project with '-- fast' which will run version on conccurent structes
```
\PWProject\bin\Debug\net8.0\PWProject.exe -- fast
``` 



## Run Test Generator
```
\TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- FileName.txt MIN_N MAX_N MIN_M MAX_M MAX_COST
```

## Testing
* To run test you can type:
```
test.bat
```
Which will create test and run single thread, thread pool, threads and fast version for diffrent threads number and save runtimes in *.txt files (It takes around 3 hours to complete).
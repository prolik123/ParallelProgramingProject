## Build
* To build project enter PWProject directory and run command:
```
build.bat
```

## Run 
* To run project run .exe file, you can also run parallel version by typing:
``` 
PWProject.exe -- parallel 
```
for thread pool version

* To run project on simple threads type
```
PWProject.exe -- threads
```

## Run Test Generator
```
TestsGenerator.exe -- FileName.txt MIN_N MAX_N MIN_M MAX_M MAX_COST
```

## Testing
* To run test you can type:
```
test.bat
```
Which will create test and run single thread and parallel version and save runtimes in *.txt files
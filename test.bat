TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- sparseSmall.txt 100000 100000 100000 100000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- sparseMed.txt 1000000 1000000 1000000 1000000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- sparseBig.txt 10000000 10000000 10000000 10000000 1000000000
rem TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- sparseVeryBig.txt 100000000 100000000 100000000 100000000 1000000000

PWProject\bin\Debug\net8.0\PWProject.exe < sparseSmall.txt 2> sparseSmallTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <sparseSmall.txt 2> sparseSmallTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <sparseSmall.txt 2> sparseSmallTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < sparseMed.txt 2> sparseMedTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <sparseMed.txt 2> sparseMedTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <sparseMed.txt 2> sparseMedTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < sparseBig.txt 2> sparseBigTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <sparseBig.txt 2> sparseBigTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <sparseBig.txt 2> sparseBigTimeMultiThread.txt

rem PWProject\bin\Debug\net8.0\PWProject.exe < sparseVeryBig.txt 2> sparseVeryBigTimeSingleThread.txt
rem PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <sparseVeryBig.txt 2> sparseVeryBigTimeMultiThread.txt

TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- 10VSmall.txt 10000 10000 100000 100000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- 10VMed.txt 100000 100000 1000000 1000000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- 10VBig.txt 1000000 1000000 10000000 10000000 1000000000

PWProject\bin\Debug\net8.0\PWProject.exe < 10VSmall.txt 2> 10VSmallTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <10VSmall.txt 2> 10VSmallTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <10VSmall.txt 2> 10VSmallTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < 10VMed.txt 2> 10VMedTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <10VMed.txt 2> 10VMedTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <10VMed.txt 2> 10VMedTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < 10VBig.txt 2> 10VBigTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <10VBig.txt 2> 10VBigTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <10VBig.txt 2> 10VBigTimeMultiThread.txt

rem E ~ V*sqrt(V)
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- midDenseSmall.txt 1000 1000 30000 30000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- midDenseMed.txt 10000 10000 1000000 1000000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- midDenseBig.txt 100000 100000 30000000 30000000 1000000000

PWProject\bin\Debug\net8.0\PWProject.exe < midDenseSmall.txt 2> midDenseSmallTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <midDenseSmall.txt 2> midDenseSmallTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <midDenseSmall.txt 2> midDenseSmallTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < midDenseMed.txt 2> midDenseMedTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <midDenseMed.txt 2> midDenseMedTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <midDenseMed.txt 2> midDenseMedTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < midDenseBig.txt 2> midDenseBigTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <midDenseBig.txt 2> midDenseBigTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <midDenseBig.txt 2> midDenseBigTimeMultiThread.txt

TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- denseSmall.txt 1000 1000 450000 450000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- denseMed.txt 5000 5000 1200000 1200000 1000000000
TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe -- denseBig.txt 10000 10000 45000000 45000000 1000000000

rem E ~ \choose{n}{2}
PWProject\bin\Debug\net8.0\PWProject.exe < denseSmall.txt 2> denseSmallTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel <denseSmall.txt 2> denseSmallTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <denseSmall.txt 2> denseSmallTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < denseMed.txt 2> denseMedTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel < denseMed.txt 2> denseMedTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <denseMed.txt 2> denseMedTimeMultiThread.txt

PWProject\bin\Debug\net8.0\PWProject.exe < denseBig.txt 2> denseBigTimeSingleThread.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- parallel < denseBig.txt 2> denseBigTimeThreadPool.txt
PWProject\bin\Debug\net8.0\PWProject.exe -- threads <denseBig.txt 2> denseBigTimeMultiThread.txt
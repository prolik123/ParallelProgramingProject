# Parallel Programming Project

This repository contains a high-performance **Parallel Graph Processing** project implemented in **C# (.NET 8)**. The primary goal of this project is to benchmark and compare different parallelization strategies for solving computational problems on weighted graphs (e.g., Shortest Path or MST algorithms).

The project demonstrates the performance trade-offs between sequential execution and various parallel threading models, analyzing speedup and resource utilization.

## 📄 Documentation

For a deep dive into the theoretical background, algorithmic details, and performance analysis, please refer to the **[Project Paper](paper.pdf)** included in this repository.

## 🚀 Features

* **Multiple Implementations**:
    * **Sequential**: Baseline single-threaded implementation.
    * **Threads**: Parallel implementation using manually managed `System.Threading.Thread`.
    * **ThreadPool**: Parallel implementation utilizing the `.NET ThreadPool` for efficient task management.
    * **Fast (Concurrent)**: Optimized parallel version using **concurrent data structures** (e.g., `ConcurrentQueue`, `ConcurrentBag`) for maximum throughput.
* **Test Generator**: Automated generation of random weighted graphs with configurable parameters (Nodes, Edges, Cost).
* **Benchmarking Suite**: Batch scripts to automate testing across various dataset sizes.
* **Visualization**: Scripts and outputs to plot performance metrics.

## 📂 Repository Structure

* **`PWProject`**: Main application source code (C#).
* **`TestsGenerator`**: Utility to generate random graph test cases.
* **`plots`**: Python/Matplotlib scripts for visualizing benchmark results.
* **`PNG`**: Generated plots and performance graphs.
* **`results`**: Raw text output from benchmark runs.
* **`paper.pdf`**: Detailed project report.

## 🛠️ Prerequisites

* **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**: Required to build and run the C# projects.
* **Python** (Optional): Required if you wish to run the plotting scripts in the `plots` folder.

## 🔨 Building the Project

This project includes batch scripts for easy building on Windows.

1.  Open a terminal in the repository root.
2.  Run the build script:
    ```cmd
    build.bat
    ```
    *To clean the build artifacts before building, you can run `CleanBuild.bat`.*

## 💻 Usage

### 1. Generating Tests
Before running the benchmark, generate a test graph using the `TestsGenerator`.

**Syntax:**
```cmd
TestsGenerator.exe FileName.txt MIN_N MAX_N MIN_M MAX_M MAX_COST
````

  * `N`: Number of Nodes
  * `M`: Number of Edges
  * `COST`: Max Edge Weight

**Example:**

```cmd
\TestsGenerator\bin\Debug\net8.0\TestsGenerator.exe my_test_graph.txt 100 1000 500 5000 100
```

### 2\. Running the Algorithm

You can run different versions of the algorithm using command-line flags.

  * **ThreadPool Version:**
    ```cmd
    \PWProject\bin\Debug\net8.0\PWProject.exe -- parallel
    ```
  * **Manual Threads Version:**
    ```cmd
    \PWProject\bin\Debug\net8.0\PWProject.exe -- threads
    ```
  * **Optimized Concurrent Version:**
    ```cmd
    \PWProject\bin\Debug\net8.0\PWProject.exe -- fast
    ```

### 3\. Automated Testing & Benchmarking

To run a full suite of tests and save the runtimes for analysis:

```cmd
test.bat
```

*Note: This process generates multiple tests and runs all variations. It may take several hours to complete depending on the configured parameters.*

## 📊 Performance Results

The project analyzes execution time across different graph sizes. Visualizations of these results can be found in the **`PNG`** directory.

> 
> *(Check the `PNG` folder for the latest generated performance plots)*

## 👤 Author

  * **prolik123** - [GitHub Profile](https://github.com/prolik123)

-----

*This project is part of a Parallel Programming course assignment.*

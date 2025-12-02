
namespace PWProject.DataStructures;

public record Edge<T>(T First, T Second) : Pair<T, T>(First, Second);
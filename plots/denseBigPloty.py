import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [s]')
plt.title('Performace [|V| = 1e4, |E| = 4,5e7]')
plt.plot([1, 2, 4, 8, 16], [100, 100, 100, 100, 100], color='red')
plt.plot([1, 2, 4, 8, 16], [110, 69, 45, 26, 33], color='blue')
plt.plot([1, 2, 4, 8, 16], [107, 69, 45, 27, 32], color='green')
plt.plot([1, 2, 4, 8, 16], [107, 61, 48, 30, 30], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
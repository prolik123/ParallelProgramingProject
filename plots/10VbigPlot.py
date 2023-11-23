import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [s]')
plt.title('Performace [|V| = 1e6, |E| = 1e7]')
plt.plot([1, 2, 4, 8, 16], [74, 74, 74, 74, 74], color='red')
plt.plot([1, 2, 4, 8, 16], [131, 62, 36, 25, 26], color='blue')
plt.plot([1, 2, 4, 8, 16], [129, 61, 35, 23, 22], color='green')
plt.plot([1, 2, 4, 8, 16], [75, 39, 25, 20, 19], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
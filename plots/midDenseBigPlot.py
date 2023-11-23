import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [s]')
plt.title('Performace [|V| = 1e5, |E| = 3e7]')
plt.plot([1, 2, 4, 8, 16], [120, 120, 120, 120, 120], color='red')
plt.plot([1, 2, 4, 8, 16], [175, 73, 47, 29, 28], color='blue')
plt.plot([1, 2, 4, 8, 16], [173, 75, 48, 32, 26], color='green')
plt.plot([1, 2, 4, 8, 16], [159, 72, 35, 26, 20], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
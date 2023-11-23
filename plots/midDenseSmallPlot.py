import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e3, |E| = 3e4]')
plt.plot([1, 2, 4, 8, 16], [16, 16, 16, 16, 16], color='red')
plt.plot([1, 2, 4, 8, 16], [26, 13, 11, 8, 10], color='blue')
plt.plot([1, 2, 4, 8, 16], [30, 14, 9, 8, 7], color='green')
plt.plot([1, 2, 4, 8, 16], [50, 14, 15, 9, 7], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
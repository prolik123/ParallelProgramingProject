import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e6, |E| = 1e6]')
plt.plot([1, 2, 4, 8, 16], [5500, 5500, 5500, 5500, 5500], color='red')
plt.plot([1, 2, 4, 8, 16], [12000, 11000, 6000, 4400, 4600], color='blue')
plt.plot([1, 2, 4, 8, 16], [10000, 8200, 5400, 4000, 4300], color='green')
plt.plot([1, 2, 4, 8, 16], [7600, 6100, 4300, 3600, 3200], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e5, |E| = 1e6]')
plt.plot([1, 2, 4, 8, 16], [2900, 2900, 2900, 2900, 2900], color='red')
plt.plot([1, 2, 4, 8, 16], [3500, 2500, 2200, 1900, 1870], color='blue')
plt.plot([1, 2, 4, 8, 16], [3600, 2400, 2000, 1550, 1750], color='green')
plt.plot([1, 2, 4, 8, 16], [2600, 1900, 1350, 1575, 1625], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
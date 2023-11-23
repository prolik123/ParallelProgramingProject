import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e4, |E| = 1e5]')
plt.plot([1, 2, 4, 8, 16], [150, 150, 150, 150, 150], color='red')
plt.plot([1, 2, 4, 8, 16], [170, 110, 110, 80, 90], color='blue')
plt.plot([1, 2, 4, 8, 16], [190, 100, 75, 70, 70], color='green')
plt.plot([1, 2, 4, 8, 16], [210, 110, 75, 65, 90], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()
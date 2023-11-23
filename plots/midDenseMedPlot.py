import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 1e4, |E| = 1e6]')
plt.plot([1, 2, 4, 8, 16], [1450, 1450, 1450, 1450, 1450], color='red')
plt.plot([1, 2, 4, 8, 16], [1800, 1050, 850, 650, 650], color='blue')
plt.plot([1, 2, 4, 8, 16], [1700, 1150, 750, 590, 560], color='green')
plt.plot([1, 2, 4, 8, 16], [1600, 970, 710, 560, 500], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()